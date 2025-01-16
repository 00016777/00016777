using Microsoft.AspNetCore.Http;

namespace Restaurant.Application.Services.FileServices;

public interface IFileService
{
    Task<string> SaveFileAsync(IFormFile file, string subFolder);
    Task<string> GetFileUrl(string fileName, string subFolder);
    Task<bool> DeleteFile(string fileName, string subFolder);
}
