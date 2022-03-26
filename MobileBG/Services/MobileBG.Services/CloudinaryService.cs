namespace MobileBG.Services;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MobileBG.Services.Contracts;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public string Delete(string url)
    {
        var id = url.Split('/').Last().Split('.').First();
        var publicId = $"MobileBG/{id}";
        var res = this.cloudinary.DeleteResources(publicId);

        return res.ToString();
    }

    public string DeleteAll(Guid carId)
    {
        var res = this.cloudinary.DeleteResourcesByTag(carId.ToString());
        return res.ToString();
    }
}
