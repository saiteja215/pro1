using EcommerceBackend.Models.DTOs;
using EcommerceBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet]
        public async Task<ActionResult<CartDto>> GetCart()
        {
            var cart = await _cartService.GetCartByUserIdAsync(UserId);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        public class AddItemDto
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }

        [HttpPost("add")]
        public async Task<ActionResult<CartDto>> Add(AddItemDto dto)
        {
            var cart = await _cartService.AddToCartAsync(UserId, dto.ProductId, dto.Quantity);
            return Ok(cart);
        }

        [HttpDelete("remove/{productId}")]
        public async Task<IActionResult> Remove(int productId)
        {
            var result = await _cartService.RemoveFromCartAsync(UserId, productId);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPost("clear")]
        public async Task<IActionResult> Clear()
        {
            var result = await _cartService.ClearCartAsync(UserId);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
