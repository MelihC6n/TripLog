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
}
