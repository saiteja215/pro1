namespace EcommerceBackend.Helpers
{
    public static class PaymentHelper
    {
        public static bool IsValidPaymentMethod(string method)
        {
            var validMethods = new[] { "Card", "UPI", "NetBanking" };
            return validMethods.Contains(method);
        }
    }
}
