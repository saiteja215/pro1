using EcommerceBackend.Data;
using EcommerceBackend.Models.DTOs;
using EcommerceBackend.Models;
using EcommerceBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

public class CartService : ICartService
{
    private readonly ApplicationDbContext _context;

    public CartService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CartDto> GetCartByUserIdAsync(int userId)
    {
        var cart = await _context.Carts.Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null) return null;

        return new CartDto
        {
            Id = cart.Id,
            UserId = cart.UserId,
            Items = cart.CartItems.Select(ci => new CartItemDto
            {
                ProductId = ci.ProductId,
                ProductName = ci.Product.Name,
                Quantity = ci.Quantity,
                Price = ci.Product.Price
            }).ToList()
        };
    }

    public async Task<CartDto> AddToCartAsync(int userId, int productId, int quantity)
    {
        var cart = await _context.Carts.Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null)
        {
            cart = new Cart { UserId = userId, CartItems = new List<CartItem>() };
            _context.Carts.Add(cart);
        }

        var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            cart.CartItems.Add(new CartItem { ProductId = productId, Quantity = quantity });
        }

        await _context.SaveChangesAsync();
        return await GetCartByUserIdAsync(userId);
    }

    public async Task<bool> RemoveFromCartAsync(int userId, int productId)
    {
        var cart = await _context.Carts.Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        var item = cart?.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
        if (item == null) return false;

        _context.CartItems.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ClearCartAsync(int userId)
    {
        var cart = await _context.Carts.Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId);
        if (cart == null || !cart.CartItems.Any()) return false;

        _context.CartItems.RemoveRange(cart.CartItems);
        await _context.SaveChangesAsync();
        return true;
    }
}
