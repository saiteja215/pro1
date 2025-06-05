namespace EcommerceBackend.Helpers
{
    public class EmailHelper
    {
        public async Task<bool> SendEmailAsync(string to, string subject, string body)
        {
            // Simulated email sending
            await Task.Delay(500);
            Console.WriteLine($"Email sent to {to}: {subject}");
            return true;
        }
    }
}
