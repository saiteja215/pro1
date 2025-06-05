namespace EcommerceBackend.Models.DTOs
{
    public class PaymentResponseDto
    {
        public string TransactionId { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
