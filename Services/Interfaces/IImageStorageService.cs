namespace EcommerceBackend.Services.Interfaces
{
    public interface IImageStorageService
    {
        Task<string> UploadImageAsync(IFormFile image);
        Task<bool> DeleteImageAsync(string imageUrl);
    }
}
