using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Restaurant.Application.Services.FileServices;

public class FileService : IFileService
{
    private readonly string _basePath;
    public FileService(IConfiguration configuration)
    {
        _basePath = configuration["FileStorage:BasePath"] ?? "wwwroot/files";
    }

    public async Task<bool> DeleteFile(string fileName, string subFolder)
    {
        var filePath = Path.Combine(_basePath, subFolder, fileName);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);

            return await Task.FromResult(true);
        }

        return await Task.FromResult(false);
    }

    public async Task<string> GetFileUrl(string fileName, string subFolder)
    {
        return await Task.FromResult(Path.Combine($"/files/{subFolder}", fileName).Replace("\\", "/"));
    }

    public async Task<string> SaveFileAsync(IFormFile file, string subFolder)
    {
        // Validate file
        if (file == null || file.Length == 0)
            throw new ArgumentException("Invalid file.");

        // Generate unique file name
        var extension = Path.GetExtension(file.FileName);
        var uniqueFileName = $"{Guid.NewGuid()}{extension}";

        // Combine path
        var folderPath = Path.Combine(_basePath, subFolder);

        if (!Directory.Exists(folderPath)) // Ensure directory exists
        {
            Directory.CreateDirectory(folderPath);
        }

        var filePath = Path.Combine(folderPath, uniqueFileName);

        // Save file
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return uniqueFileName; // Return unique name to store in the database
    }
}
