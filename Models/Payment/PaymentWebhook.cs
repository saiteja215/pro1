using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Models.Payment
{
    public class PaymentWebhook
    {
        [Key]
        public string EventId { get; set; }
        public string PaymentGateway { get; set; }
        public string Payload { get; set; }
        public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;
    }
}
