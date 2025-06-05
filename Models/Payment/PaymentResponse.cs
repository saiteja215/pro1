namespace EcommerceBackend.Models.Payment
{
    public class PaymentResponse
    {
        public string TransactionId { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
