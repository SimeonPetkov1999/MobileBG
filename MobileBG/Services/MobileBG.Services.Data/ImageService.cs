namespace MobileBG.Services.Data;

public class ImageService : IImageService
{
    private readonly IRepository<ImageEntity> imageRepo;
    private readonly ICloudinaryService cloudinaryService;

    public ImageService(
        IRepository<ImageEntity> imageRepo,
        ICloudinaryService cloudinaryService)
    {
        this.imageRepo = imageRepo;
        this.cloudinaryService = cloudinaryService;
    }

    public async Task DeleteImageAsync(string url)
    {
        var image = await this.imageRepo
            .All()
            .FirstOrDefaultAsync(x => x.ImageUrl == url);

        this.imageRepo.Delete(image);
        await this.imageRepo.SaveChangesAsync();

        await this.cloudinaryService.DeleteAsync(url);
    }
}
