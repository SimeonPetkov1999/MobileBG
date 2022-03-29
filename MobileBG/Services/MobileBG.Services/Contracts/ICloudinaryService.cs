namespace MobileBG.Services.Contracts;

public interface ICloudinaryService
{
    public Task<string> UploadAsync(IFormFile file, Guid carId);

    public Task<string> DeleteAsync(string url);

    public Task<string> DeleteAllAsync(Guid carId);
}
