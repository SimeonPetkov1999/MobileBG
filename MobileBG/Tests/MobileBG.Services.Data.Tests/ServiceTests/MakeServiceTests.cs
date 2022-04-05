namespace MobileBG.Services.Data.Tests.ServiceTests;

using MobileBG.Web.ViewModels.Makes;

public class MakeServiceTests
{
    private Mock<IRepository<MakeEntity>> makeRepoMock;
    private MakeService makeService;

    public MakeServiceTests()
    {
        AutoMapper.Configure();
    }

    [Fact]
    public async void CreateMakeAsyncShouldCreateMake()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var entity = new MakeEntity { Name = "TestNameForMake" };

        this.makeRepoMock.Setup(x => x.AddAsync(entity));

        var result = await this.makeService.CreateMakeAsync("Test");

        Assert.IsType<Guid>(result);
    }

    [Fact]
    public async void CreateModelForMakeAsyncShouldReturnTrue()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var id = Guid.NewGuid();
        var makes = new List<MakeEntity>()
        {
           new() { Id = id, Name = "TestNameForMake", Models = new List<ModelEntity>() { new() { Name = "TestModel" } } },
           new() { Id = Guid.NewGuid(), Name = "TestNameForMake" },
           new() { Id = Guid.NewGuid(), Name = "TestNameForMake" },
        }.AsQueryable();

        var mockMakes = makes.BuildMock();

        this.makeRepoMock.Setup(x => x.All()).Returns(mockMakes);

        var result = await this.makeService.CreateModelForMakeAsync(id, "Test");

        Assert.True(result);
    }

    [Fact]
    public async void CreateModelForMakeAsyncShouldReturnFalseIfMakeisNull()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var id = Guid.NewGuid();
        var makes = new List<MakeEntity>()
        {
           new() { Id = id, Name = "TestNameForMake", Models = new List<ModelEntity>() { new() { Name = "Test" } } },
           new() { Id = Guid.NewGuid(), Name = "TestNameForMake" },
           new() { Id = Guid.NewGuid(), Name = "TestNameForMake" },
        }.AsQueryable();

        var mockMakes = makes.BuildMock();

        this.makeRepoMock.Setup(x => x.All()).Returns(mockMakes);

        var result = await this.makeService.CreateModelForMakeAsync(id, "Test");

        Assert.False(result);
    }

    [Fact]
    public async void DeleteMakeAsyncShouldReturnTrueIfDeleted()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var id = Guid.NewGuid();
        var makes = new List<MakeEntity>()
        {
           new() { Id = id, Name = "TestNameForMake" },
           new() { Id = Guid.NewGuid(), Name = "TestNameForMake" },
           new() { Id = Guid.NewGuid(), Name = "TestNameForMake" },
        }.AsQueryable();

        var mockMakes = makes.BuildMock();

        this.makeRepoMock.Setup(x => x.All()).Returns(mockMakes);

        var result = await this.makeService.DeleteMakeAsync(id);

        Assert.True(result);
    }

    [Fact]
    public async void DeleteMakeAsyncShouldReturnFalseIfNotDeleted()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var id = Guid.NewGuid();
        var makes = new List<MakeEntity>()
        {
           new() { Id = id, Name = "TestNameForMake", Models = new List<ModelEntity>() { new() { Name = "Test" } } },
           new() { Id = Guid.NewGuid(), Name = "TestNameForMake" },
           new() { Id = Guid.NewGuid(), Name = "TestNameForMake" },
        }.AsQueryable();

        var mockMakes = makes.BuildMock();

        this.makeRepoMock.Setup(x => x.All()).Returns(mockMakes);

        var result = await this.makeService.DeleteMakeAsync(id);

        Assert.False(result);
    }

    [Fact]
    public async void EditMakeAsyncShouldEditMake()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var id = Guid.NewGuid();
        var model = new EditMakeViewModel()
        {
            Id = id,
            Name = "NewName",
        };

        var makes = new List<MakeEntity>()
        {
           new() { Id = id, Name = "TestNameForMake", Models = new List<ModelEntity>() { new() { Name = "Test" } } },
           new() { Id = Guid.NewGuid(), Name = "TestNameForMake" },
           new() { Id = Guid.NewGuid(), Name = "TestNameForMake" },
        }.AsQueryable();

        var mockMakes = makes.BuildMock();

        this.makeRepoMock.Setup(x => x.All()).Returns(mockMakes);

        await this.makeService.EditMakeAsync(model);

        Assert.True(true);
    }

    [Theory]
    [InlineData("Test")]
    [InlineData(null)]
    public async void GetAllMakesAsyncShouldReturnAllMakes(string value)
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var id = Guid.NewGuid();

        var makes = new List<MakeEntity>()
        {
           new() { Id = id, Name = "TestNameForMake", Models = new List<ModelEntity>() { new() { Name = "Test" } } },
           new() { Id = Guid.NewGuid(), Name = "TestNameForMake" },
           new() { Id = Guid.NewGuid(), Name = "TestNameForMake" },
        }.AsQueryable();

        var mockMakes = makes.BuildMock();

        this.makeRepoMock.Setup(x => x.AllAsNoTracking()).Returns(mockMakes);

        var result = await this.makeService.GetAllMakesAsync(value);

        Assert.IsType<List<MakeInfoViewModel>>(result);
    }

    [Fact]
    public async void GetMakeDetialsAsyncShouldReturnOneMakeWithDetails()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var id = Guid.NewGuid();

        var makes = new List<MakeEntity>()
        {
           new() { Id = id, Name = "TestNameForMake", Models = new List<ModelEntity>() { new() { Name = "Test" } } },
           new() { Id = Guid.NewGuid(), Name = "TestNameForMake" },
           new() { Id = Guid.NewGuid(), Name = "TestNameForMake" },
        }.AsQueryable();

        var mockMakes = makes.BuildMock();

        this.makeRepoMock.Setup(x => x.AllAsNoTracking()).Returns(mockMakes);

        var result = await this.makeService.GetMakeDetialsAsync(id);

        Assert.IsType<MakeDetailsViewModel>(result);
    }

    private void InitializeRepos()
    {
        this.makeRepoMock = new Mock<IRepository<MakeEntity>>();
        this.makeService = new MakeService(this.makeRepoMock.Object);
    }
}
