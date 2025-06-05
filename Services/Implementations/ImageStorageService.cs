using EcommerceBackend.Services.Interfaces;

namespace EcommerceBackend.Services.Implementations
{
    public class ImageStorageService : IImageStorageService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _contextAccessor;

        public ImageStorageService(IWebHostEnvironment env, IHttpContextAccessor contextAccessor)
        {
            _env = env;
            _contextAccessor = contextAccessor;
        }

        public async Task<string> UploadImageAsync(IFormFile image)
        {
            if (image == null || image.Length == 0)
                return null;

            var uploadsFolder = Path.Combine(_env.WebRootPath, "images");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            var baseUrl = _contextAccessor.HttpContext.Request.Scheme + "://" + _contextAccessor.HttpContext.Request.Host;
            return baseUrl + "/images" + fileName;
        }

        public async Task<bool> DeleteImageAsync(string imageUrl)
        {
            var fileName = Path.GetFileName(new Uri(imageUrl).LocalPath);
            var path = Path.Combine(_env.WebRootPath, "images", fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }

}
