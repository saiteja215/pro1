using EcommerceBackend.Data;
using EcommerceBackend.Models.DTOs;
using EcommerceBackend.Models.Payment;
using EcommerceBackend.Services.Interfaces;

namespace EcommerceBackend.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _context;

        public PaymentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaymentResponseDto> ProcessPaymentAsync(PaymentDto dto)
        {
            // Simulate successful payment
            return await Task.FromResult(new PaymentResponseDto
            {
                TransactionId = Guid.NewGuid().ToString(),
                IsSuccess = true,
                Message = "Payment processed successfully"
            });
        }

        public async Task<bool> HandleWebhookAsync(PaymentWebhook webhook)
        {
            _context.PaymentWebhooks.Add(webhook);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
