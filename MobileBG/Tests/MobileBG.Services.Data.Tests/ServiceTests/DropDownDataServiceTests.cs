namespace MobileBG.Services.Data.Tests.ServiceTests;

using MobileBG.Services.Data.Contracts;
using MobileBG.Web.ViewModels.Cars;

public class DropDownDataServiceTests
{
    private Mock<IRepository<MakeEntity>> makeRepo;
    private Mock<IRepository<PetrolTypeEntity>> petrolTypeRepo;
    private Mock<IRepository<CityEntity>> cityTypeRepo;

    private IDropDownDataService dropDownDataService;

    public DropDownDataServiceTests()
    {
        AutoMapper.Configure();
    }

    [Fact]
    public async void GetAllCitiesAsyncShouldReturnAllCities()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var cities = new List<CityEntity>()
        {
            new() { Id = Guid.NewGuid(), Name = "TestCity" },
            new() { Id = Guid.NewGuid(), Name = "TestCity1" },
            new() { Id = Guid.NewGuid(), Name = "TestCity2" },
        }.AsQueryable();

        var mockCities = cities.BuildMock();

        this.cityTypeRepo.Setup(x => x.AllAsNoTracking()).Returns(mockCities);
        var result = await this.dropDownDataService.GetAllCitiesAsync();

        Assert.Equal(3, result.Count);
    }

    [Fact]
    public async void GetAllMakesAsyncShouldReturnAllMakes()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var makes = new List<MakeEntity>()
        {
            new() { Id = Guid.NewGuid(), Name = "TestMake" },
            new() { Id = Guid.NewGuid(), Name = "TestMake1" },
            new() { Id = Guid.NewGuid(), Name = "TestMake2" },
        }.AsQueryable();

        var mockMakes = makes.BuildMock();

        this.makeRepo.Setup(x => x.AllAsNoTracking()).Returns(mockMakes);
        var result = await this.dropDownDataService.GetAllMakesAsync();

        Assert.Equal(3, result.Count);
    }

    [Fact]
    public async void GetAllPetrolTypesAsyncShouldReturnAllPetrolTypes()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var types = new List<PetrolTypeEntity>()
        {
            new() { Id = Guid.NewGuid(), Name = "PetrolType" },
            new() { Id = Guid.NewGuid(), Name = "PetrolType1" },
            new() { Id = Guid.NewGuid(), Name = "PetrolType2" },
        }.AsQueryable();

        var typesMock = types.BuildMock();

        this.petrolTypeRepo.Setup(x => x.AllAsNoTracking()).Returns(typesMock);
        var result = await this.dropDownDataService.GetAllPetrolTypesAsync();

        Assert.Equal(3, result.Count);
    }

    private void InitializeRepos()
    {
        this.makeRepo = new Mock<IRepository<MakeEntity>>();
        this.petrolTypeRepo = new Mock<IRepository<PetrolTypeEntity>>();
        this.cityTypeRepo = new Mock<IRepository<CityEntity>>();
        this.dropDownDataService = new DropDownDataService(this.makeRepo.Object, this.petrolTypeRepo.Object, this.cityTypeRepo.Object);
    }
}
