using EcommerceBackend.Models.DTOs;

namespace EcommerceBackend.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(int id);
        Task<ProductDto> CreateAsync(ProductCreateDto dto);
        Task<ProductDto> UpdateAsync(ProductUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
