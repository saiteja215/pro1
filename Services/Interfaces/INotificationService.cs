namespace EcommerceBackend.Services.Interfaces
{
    public interface INotificationService
    {
        Task SendOrderNotificationAsync(int userId, string message);
        Task SendBroadcastAsync(string message);
    }
}
