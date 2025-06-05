using EcommerceBackend.Data;
using EcommerceBackend.Models.DTOs;
using EcommerceBackend.Models;
using EcommerceBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Services.Implementations
{
    // ProductService.cs
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageStorageService _imageService;

        public ProductService(ApplicationDbContext context, IImageStorageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            return await _context.Products
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock,
                    ImageUrl = p.ImageUrl
                }).ToListAsync();
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                ImageUrl = product.ImageUrl
            };
        }

        public async Task<ProductDto> CreateAsync(ProductCreateDto dto)
        {
            var imageUrl = await _imageService.UploadImageAsync(dto.Image);

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                ImageUrl = imageUrl,
                CreatedAt = DateTime.UtcNow
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(product.Id);
        }

        public async Task<ProductDto> UpdateAsync(ProductUpdateDto dto)
        {
            var product = await _context.Products.FindAsync(dto.Id);
            if (product == null) return null;

            if (dto.Image != null)
            {
                await _imageService.DeleteImageAsync(product.ImageUrl);
                product.ImageUrl = await _imageService.UploadImageAsync(dto.Image);
            }

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Stock = dto.Stock;
            product.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return await GetByIdAsync(product.Id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
