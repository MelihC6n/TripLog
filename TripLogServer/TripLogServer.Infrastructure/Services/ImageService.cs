using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using TripLogServer.Application.Services;

namespace TripLogServer.Infrastructure.Services;
internal sealed class ImageService : IImageService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ImageService(IWebHostEnvironment hostEnvironment)
    {
        _webHostEnvironment = hostEnvironment;
    }
    public async Task SaveImageAsync(string imagePath, string folder, IFormFile fileData)
    {
        var root = _webHostEnvironment.WebRootPath;
        using (var stream = System.IO.File.Create(root + "/" + folder + "/" + imagePath))
        {
            fileData.CopyTo(stream);
        }
    }

    public async Task DeleteImageAsync(string imagePath, string folder)
    {
        var root = _webHostEnvironment.WebRootPath;
        var filePath = System.IO.Path.Combine(root, folder, imagePath);
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
        }
    }
}
