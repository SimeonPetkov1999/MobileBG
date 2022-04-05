namespace MobileBG.Services.Data.Tests.ServiceTests;

public class ModelServiceTests
{
    private Mock<IRepository<ModelEntity>> modelRepoMock;
    private ModelService modelService;

    public ModelServiceTests()
    {
        AutoMapper.Configure();
    }

    [Fact]
    public async void GetModelsForMakeAsyncShouldReturnModels()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var makeId = Guid.NewGuid();
        var makes = new List<ModelEntity>()
        {
            new() { Id = Guid.NewGuid(), Name = "TestModel", MakeId = makeId },
            new() { Id = Guid.NewGuid(), Name = "TestModel", MakeId = makeId },
            new() { Id = Guid.NewGuid(), Name = "TestModel", MakeId = makeId },
        }.AsQueryable();

        var mockMakes = makes.BuildMock();

        this.modelRepoMock.Setup(x => x.AllAsNoTracking()).Returns(mockMakes);
        var result = await this.modelService.GetModelsForMakeAsync(makeId);

        Assert.Equal(3, result.Count);
    }

    [Fact]
    public async void DeleteModelAsyncShouldReturnTrueIfDeleted()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var id = Guid.NewGuid();
        var makes = new List<ModelEntity>()
        {
            new() { Id = id, Name = "TestModel", },
        }.AsQueryable();

        var mockMakes = makes.BuildMock();

        this.modelRepoMock.Setup(x => x.All()).Returns(mockMakes);
        var result = await this.modelService.DeleteModelAsync(id);

        Assert.True(result.IsDeleted);
    }

    [Fact]
    public async void DeleteModelAsyncShouldReturnFalseIfNotDeleted()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var id = Guid.NewGuid();
        var makes = new List<ModelEntity>()
        {
            new() { Id = id, Name = "TestModel", Cars = new List<CarEntity>() { new() { MakeId = Guid.NewGuid() } } },
        }.AsQueryable();

        var mockMakes = makes.BuildMock();

        this.modelRepoMock.Setup(x => x.All()).Returns(mockMakes);
        var result = await this.modelService.DeleteModelAsync(id);

        Assert.False(result.IsDeleted);
    }

    private void InitializeRepos()
    {
        this.modelRepoMock = new Mock<IRepository<ModelEntity>>();
        this.modelService = new ModelService(this.modelRepoMock.Object);
    }
}
