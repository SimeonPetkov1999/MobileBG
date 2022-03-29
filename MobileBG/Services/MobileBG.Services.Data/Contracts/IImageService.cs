namespace MobileBG.Services.Data.Contracts;

public interface IImageService
{
    public Task DeleteImageAsync(string url);
}
