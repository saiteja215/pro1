using EcommerceBackend.Models.DTOs;
using EcommerceBackend.Models.Payment;

namespace EcommerceBackend.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResponseDto> ProcessPaymentAsync(PaymentDto dto);
        Task<bool> HandleWebhookAsync(PaymentWebhook webhook);
    }
}
