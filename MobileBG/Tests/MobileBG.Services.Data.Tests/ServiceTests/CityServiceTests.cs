namespace MobileBG.Services.Data.Tests.ServiceTests;

public class CityServiceTests
{
    private readonly Mock<IRepository<CityEntity>> cityRepoMock;
    private readonly CityService cityServiceMock;

    public CityServiceTests()
    {
        this.cityRepoMock = new Mock<IRepository<CityEntity>>();
        this.cityServiceMock = new CityService(this.cityRepoMock.Object);

        AutoMapper.Configure();
    }

    [Fact]
    public async void GetAllCitiesAsyncShouldReturnListOfCities()
    {
        var cities = new List<CityEntity>()
        {
            new() { Id = Guid.NewGuid(), Name = "TestCity" },
            new() { Id = Guid.NewGuid(), Name = "TestCity1" },
            new() { Id = Guid.NewGuid(), Name = "TestCity2" },
        }.AsQueryable();

        var mockCities = cities.BuildMock();

        this.cityRepoMock.Setup(x => x.AllAsNoTracking()).Returns(mockCities);
        var result = await this.cityServiceMock.GetAllCitiesAsync(null);

        Assert.Equal(3, result.Count);
    }

    [Fact]
    public async void GetAllCitiesAsyncWithGiveKeyWordShouldReturnListOfCities()
    {
        var cities = new List<CityEntity>()
        {
            new() { Id = Guid.NewGuid(), Name = "TestCity" },
            new() { Id = Guid.NewGuid(), Name = "TestCity1" },
            new() { Id = Guid.NewGuid(), Name = "TestCity2" },
        }.AsQueryable();

        var mockCities = cities.BuildMock();

        this.cityRepoMock.Setup(x => x.AllAsNoTracking()).Returns(mockCities);
        var result = await this.cityServiceMock.GetAllCitiesAsync("Test");

        Assert.Equal(3, result.Count);
    }

    [Fact]
    public async void CreateCityAsyncSHouldCreateCity()
    {
        var city = new CityEntity() { Name = "Test" };
        this.cityRepoMock.Setup(x => x.AddAsync(city));
        var result = await this.cityServiceMock.CreateCityAsync("Test");

        Assert.IsType<Guid>(result);
    }

    [Fact]
    public async void DeleteCityAsyncShouldReturnTrue()
    {
        var id = Guid.NewGuid();
        var cities = new List<CityEntity>()
        {
            new() { Id = id, Name = "TestCity" },
            new() { Id = Guid.NewGuid(), Name = "TestCity1" },
            new() { Id = Guid.NewGuid(), Name = "TestCity2" },
        }.AsQueryable();

        var mockCities = cities.BuildMock();

        this.cityRepoMock.Setup(x => x.All()).Returns(mockCities);

        var result = await this.cityServiceMock.DeleteCityAsync(id);

        Assert.True(result);
    }

    [Fact]
    public async void DeleteCityAsyncShouldReturnFalseIfThereAreCarsFromThisCity()
    {
        var id = Guid.NewGuid();
        var cities = new List<CityEntity>()
        {
            new() { Id = id, Name = "TestCity", Cars = new List<CarEntity>() { new() } },
            new() { Id = Guid.NewGuid(), Name = "TestCity1" },
            new() { Id = Guid.NewGuid(), Name = "TestCity2" },
        }.AsQueryable();

        var mockCities = cities.BuildMock();

        this.cityRepoMock.Setup(x => x.All()).Returns(mockCities);

        var result = await this.cityServiceMock.DeleteCityAsync(id);

        Assert.False(result);
    }
}