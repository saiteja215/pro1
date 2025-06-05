using EcommerceBackend.Models.DTOs;

namespace EcommerceBackend.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDto> GetByIdAsync(int id);
        Task<OrderDto> CreateOrderAsync(int userId);
        Task<bool> UpdateStatusAsync(int orderId, string status);
        Task<IEnumerable<OrderDto>> GetUserOrdersAsync(int userId);
    }
}
