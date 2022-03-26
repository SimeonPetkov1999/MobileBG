namespace MobileBG.Services.Contracts;

public interface ICloudinaryService
{
    public Task<string> UploadAsync(IFormFile file, Guid carId);

    public string Delete(string url);

    public string DeleteAll(Guid carId);
}
