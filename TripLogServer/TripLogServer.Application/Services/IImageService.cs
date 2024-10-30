using Microsoft.AspNetCore.Http;

namespace TripLogServer.Application.Services;
public interface IImageService
{
    public Task SaveImageAsync(string imagePath, string folder, IFormFile fileData);
    public Task DeleteImageAsync(string imagePath, string folder);
}
