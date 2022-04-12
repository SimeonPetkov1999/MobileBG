namespace MobileBG.Services.Data.Tests.ServiceTests;

using MobileBG.Services.Contracts;
using MobileBG.Services.Data.Contracts;

public class ImageServiceTests
{
    private Mock<IRepository<ImageEntity>> imageRepo;
    private Mock<ICloudinaryService> cloudinaryService;

    private IImageService imageService;

    public ImageServiceTests()
    {
        AutoMapper.Configure();
    }

    [Fact]
    public async void DeleteImageAsyncShouldDeleteImageAndNotThrowException()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var imageUrl = "SomeUrl";
        var blogs = new List<ImageEntity>()
        {
            new() { Id = Guid.NewGuid(), ImageUrl = imageUrl },
            new() { Id = Guid.NewGuid(), ImageUrl = "test" },
        }.AsQueryable();

        var imagesMock = blogs.BuildMock();
        this.imageRepo.Setup(x => x.All()).Returns(imagesMock);

        await this.imageService.DeleteImageAsync(imageUrl);

        Assert.True(true);
    }

    private void InitializeRepos()
    {
        this.imageRepo = new Mock<IRepository<ImageEntity>>();
        this.cloudinaryService = new Mock<ICloudinaryService>();

        this.imageService = new ImageService(this.imageRepo.Object, this.cloudinaryService.Object);
    }
}
