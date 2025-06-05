    namespace EcommerceBackend.Helpers
{
    public static class ImageHelper
    {
        public static bool IsValidImage(IFormFile file)
        {
            var allowedTypes = new[] { "image/jpeg", "image/png", "image/webp" };
            return file != null && file.Length > 0 && allowedTypes.Contains(file.ContentType);
        }
    }
}
