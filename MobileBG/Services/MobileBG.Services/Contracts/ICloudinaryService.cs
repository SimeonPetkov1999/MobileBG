namespace MobileBG.Services.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

public interface ICloudinaryService
{
    public Task<string> UploadAsync(IFormFile file, Guid carId);

    public string Delete(string url);

    public string DeleteAll(Guid carId);
}
