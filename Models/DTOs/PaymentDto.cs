namespace EcommerceBackend.Models.DTOs
{
    public class PaymentDto
    {
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
    }
}
