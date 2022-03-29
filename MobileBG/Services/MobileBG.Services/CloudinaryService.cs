namespace MobileBG.Services;

public class CloudinaryService : ICloudinaryService
{
    private Cloudinary cloudinary;

    public CloudinaryService(IConfiguration config)
    {
        var cloud = Common.Encoding.Base64Decode(config.GetSection("Cloudinary:Cloud").Value);
        var apiKey = Common.Encoding.Base64Decode(config.GetSection("Cloudinary:ApiKey").Value);
        var apiSecret = Common.Encoding.Base64Decode(config.GetSection("Cloudinary:ApiSecret").Value);

        var account = new Account(cloud, apiKey, apiSecret);
        this.cloudinary = new Cloudinary(account);
    }

    public async Task<string> UploadAsync(IFormFile file, Guid carId)
    {
        var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(Guid.NewGuid().ToString(), stream),
            Folder = "MobileBG",
            PublicId = Guid.NewGuid().ToString(),
            Tags = carId.ToString(),
        };

        var res = await this.cloudinary.UploadAsync(uploadParams);
        return res.Url.ToString();
    }

    public async Task<string> DeleteAsync(string url)
    {
        var id = url.Split('/').Last().Split('.').First();
        var publicId = $"MobileBG/{id}";
        var res = await this.cloudinary.DeleteResourcesAsync(publicId);

        return res.ToString();
    }

    public async Task<string> DeleteAllAsync(Guid carId)
    {
        var res = await this.cloudinary.DeleteResourcesByTagAsync(carId.ToString());
        return res.ToString();
    }
}
