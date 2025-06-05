namespace EcommerceBackend.Models.Payment
{
    public class PaymentRequest
    {
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "INR";
        public string PaymentMethod { get; set; } // Card, UPI, etc.
    }
}
