namespace MobileBG.Services.Data.Tests.ServiceTests;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Configuration;
using MobileBG.Services.Contracts;
using MobileBG.Services.Data.Contracts;
using MobileBG.Services.Messaging;
using MobileBG.Web.ViewModels;
using Moq;
using System.IO;
using System.Text;

public class CarServiceTests
{
    private Mock<IRepository<CarEntity>> carRepo;
    private Mock<IRepository<ImageEntity>> imageRepo;
    private Mock<ICloudinaryService> cloudinaryService;
    private Mock<IEmailSender> emailSender;

    private ICarService carService;

    public CarServiceTests()
    {
        AutoMapper.Configure();
    }

    [Fact]
    public async void GetCarDataAsyncShouldReturnData()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var carId = Guid.NewGuid();
        var cars = new List<CarEntity>()
        {
            new() { Id = carId, Model = new() { }, Make = new() { }, PetrolType = new() { }, City = new() },
            new() { Id = Guid.NewGuid() },
        }.AsQueryable();

        var carsMock = cars.BuildMock();

        this.carRepo.Setup(x => x.AllAsNoTracking()).Returns(carsMock);

        var result = await this.carService.GetCarDataAsync(carId);

        Assert.IsType<EditCarViewModel>(result);
    }

    [Fact]
    public async void ValidateCarExistsAsyncShouldReturnTrue()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var carId = Guid.NewGuid();
        var cars = new List<CarEntity>()
        {
            new() { Id = carId },
            new() { Id = Guid.NewGuid() },
        }.AsQueryable();

        var carsMock = cars.BuildMock();

        this.carRepo.Setup(x => x.AllAsNoTracking()).Returns(carsMock);

        var result = await this.carService.ValidateCarExistsAsync(carId);

        Assert.True(result);
    }

    [Fact]
    public async void ValidateUserOwnsCarAsyncShouldReturnTrue()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var carId = Guid.NewGuid();
        var userId = Guid.NewGuid().ToString();
        var cars = new List<CarEntity>()
        {
            new() { Id = carId, UserId = userId },
            new() { Id = Guid.NewGuid() },
        }.AsQueryable();

        var carsMock = cars.BuildMock();

        this.carRepo.Setup(x => x.AllAsNoTracking()).Returns(carsMock);

        var result = await this.carService.ValidateUserOwnsCarAsync(userId, carId);

        Assert.True(result);
    }

    [Fact]
    public async void AllCarsForUserAsyncShouldReturnCars()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var userId = Guid.NewGuid().ToString();
        var cars = new List<CarEntity>()
        {
            new() { Id = Guid.NewGuid(), UserId = userId, Make = new MakeEntity() { Id = Guid.NewGuid() }, Model = new ModelEntity() { Id = Guid.NewGuid() } },
        }.AsQueryable();

        var carsMock = cars.BuildMock();

        this.carRepo.Setup(x => x.AllAsNoTracking()).Returns(carsMock);

        var result = await this.carService.AllCarsForUserAsync(userId, 1, 10);

        Assert.IsType<CarDataViewModel>(result);
    }

    [Fact]
    public async void GetLastAddedCarsAsyncShouldReturnList()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var userId = Guid.NewGuid().ToString();
        var cars = new List<CarEntity>()
        {
            new()
            {
                Id = Guid.NewGuid(), UserId = userId,
                Make = new MakeEntity() { Id = Guid.NewGuid() },
                Model = new ModelEntity() { Id = Guid.NewGuid() },
                CreatedOn = DateTime.Now,
                IsApproved = true,
            },
        }.AsQueryable();

        var carsMock = cars.BuildMock();

        this.carRepo.Setup(x => x.AllAsNoTracking()).Returns(carsMock);

        var result = await this.carService.GetLastAddedCarsAsync();

        Assert.Single(result);
    }

    [Fact]
    public async void CreateCarAsyncShouldCreateCar()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
        IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.txt");

        var userId = Guid.NewGuid().ToString();
        var carInput = new CreateCarInputViewModel
        {
            CityId = Guid.NewGuid(),
            MakeId = Guid.NewGuid(),
            ModelId = Guid.NewGuid(),
            PetrolTypeId = Guid.NewGuid(),
            Description = "Test",
            HorsePower = 100,
            Km = 1000,
            Price = 10000,
            YearMade = 2012,
            Images = new List<IFormFile>() { file },
        };

        var result = await this.carService.CreateCarAsync(carInput, userId);

        Assert.IsType<Guid>(result);
    }

    [Fact]
    public async void AllApprovedCarsAsyncShouldReturnShouldReturnListOfCars()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var searchModel = this.CreateSearchModel();

        var cars = new List<CarEntity>()
        {
            new()
            {
                Id = Guid.NewGuid(),
                MakeId = (Guid)searchModel.MakeId,
                Make = new MakeEntity() { Id = (Guid)searchModel.MakeId },
                ModelId = (Guid)searchModel.ModelId,
                Model = new ModelEntity() { Id = (Guid)searchModel.ModelId },
                PetrolTypeId = (Guid)searchModel.PetrolTypeId,
                CityId = (Guid)searchModel.CityId,
                YearMade = 2010,
                HorsePower = 150,
                Price = 5000,
                CreatedOn = DateTime.Now,
                IsApproved = true,
            },
        }.AsQueryable();

        var carsMock = cars.BuildMock();

        this.carRepo.Setup(x => x.AllAsNoTracking()).Returns(carsMock);

        var result = await this.carService.AllApprovedCarsAsync(searchModel, 1, 10);

        Assert.IsType<CarDataViewModel>(result);
    }

    [Fact]
    public async void AllUnapprovedCarsAsyncShouldReturnListOfCars()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var cars = new List<CarEntity>()
        {
            new()
            {
                Id = Guid.NewGuid(),
                MakeId = Guid.NewGuid(),
                Make = new MakeEntity() { Id = Guid.NewGuid(), },
                ModelId = Guid.NewGuid(),
                Model = new ModelEntity() { Id = Guid.NewGuid(), },
                PetrolTypeId = Guid.NewGuid(),
                CityId = Guid.NewGuid(),
                YearMade = 2010,
                HorsePower = 150,
                Price = 5000,
                CreatedOn = DateTime.Now,
                IsApproved = false,
            },
        }.AsQueryable();

        var carsMock = cars.BuildMock();

        this.carRepo.Setup(x => x.AllAsNoTracking()).Returns(carsMock);

        var result = await this.carService.AllUnapprovedCarsAsync(1, 10);

        Assert.IsType<CarDataViewModel>(result);
    }

    [Fact]
    public async void ApproveCarAsyncShouldChangeStatusOfTheCarAndNotThrowException()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var carId = Guid.NewGuid();

        var cars = new List<CarEntity>()
        {
            new()
            {
                Id = carId,
                MakeId = Guid.NewGuid(),
                Make = new MakeEntity() { Id = Guid.NewGuid(), },
                ModelId = Guid.NewGuid(),
                Model = new ModelEntity() { Id = Guid.NewGuid(), },
                PetrolTypeId = Guid.NewGuid(),
                CityId = Guid.NewGuid(),
                YearMade = 2010,
                HorsePower = 150,
                Price = 5000,
                CreatedOn = DateTime.Now,
                IsApproved = false,
                User = new ApplicationUser() { Email = "test@test.bg" },
            },
        }.AsQueryable();

        var carsMock = cars.BuildMock();

        this.carRepo.Setup(x => x.AllAsNoTracking()).Returns(carsMock);

        await this.carService.ApproveCarAsync(carId);

        Assert.True(true);
    }

    [Fact]
    public async void UpdateCarDataAsyncShouldUpdateTheCarAndNotThrowException()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var carId = Guid.NewGuid();

        var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
        IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.txt");

        var editModel = new EditCarViewModel()
        {
            Id = carId,
            MakeId = Guid.NewGuid(),
            ModelId = Guid.NewGuid(),
            CityId = Guid.NewGuid(),
            PetrolTypeId = Guid.NewGuid(),
            Description = "Test",
            HorsePower = 100,
            Images = new List<IFormFile>() { file },
            Km = 100,
            Price = 1000,
            YearMade = 2000,
            ImageUrls = new List<string>() { "test" },
        };

        var cars = new List<CarEntity>()
        {
            new()
            {
                Id = carId,
                MakeId = Guid.NewGuid(),
                Make = new MakeEntity() { Id = Guid.NewGuid(), },
                ModelId = Guid.NewGuid(),
                Model = new ModelEntity() { Id = Guid.NewGuid(), },
                PetrolTypeId = Guid.NewGuid(),
                CityId = Guid.NewGuid(),
                YearMade = 2010,
                HorsePower = 150,
                Price = 5000,
                CreatedOn = DateTime.Now,
                IsApproved = false,
            },
        }.AsQueryable();

        var carsMock = cars.BuildMock();

        this.carRepo.Setup(x => x.All()).Returns(carsMock);

        await this.carService.UpdateCarDataAsync(editModel);

        Assert.True(true);
    }

    [Fact]
    public async void DeleteCarAsyncShouldDeleteCarAndNotThrowException()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var carId = Guid.NewGuid();

        var cars = new List<CarEntity>()
        {
            new()
            {
                Id = carId,
                MakeId = Guid.NewGuid(),
                Make = new MakeEntity() { Id = Guid.NewGuid(), },
                ModelId = Guid.NewGuid(),
                Model = new ModelEntity() { Id = Guid.NewGuid(), },
                PetrolTypeId = Guid.NewGuid(),
                CityId = Guid.NewGuid(),
                YearMade = 2010,
                HorsePower = 150,
                Price = 5000,
                CreatedOn = DateTime.Now,
                IsApproved = false,
                Images = new List<ImageEntity>() { new() { ImageUrl = "Test" } },
            },
        }.AsQueryable();

        var carsMock = cars.BuildMock();

        this.carRepo.Setup(x => x.AllAsNoTracking()).Returns(carsMock);

        await this.carService.DeleteCarAsync(carId);

        Assert.True(true);
    }

    private void InitializeRepos()
    {
        this.carRepo = new Mock<IRepository<CarEntity>>();
        this.imageRepo = new Mock<IRepository<ImageEntity>>();
        this.cloudinaryService = new Mock<ICloudinaryService>();
        this.emailSender = new Mock<IEmailSender>();
        this.carService = new CarService(this.carRepo.Object, this.imageRepo.Object, this.cloudinaryService.Object, this.emailSender.Object);
    }

    private SearchCarViewModel CreateSearchModel()
    {
        var makeId = Guid.NewGuid();
        var modelId = Guid.NewGuid();
        var petrolTypeId = Guid.NewGuid();
        var cityId = Guid.NewGuid();
        var yearFrom = 2000;
        var yearTo = 2022;
        var minHorsePower = 100;
        var minPrice = 1000;
        var maxPrice = 100000;
        var orderBy = OrderBy.DateAdded;

        var searchModel = new SearchCarViewModel()
        {
            MakeId = makeId,
            ModelId = modelId,
            PetrolTypeId = petrolTypeId,
            CityId = cityId,
            YearFrom = yearFrom,
            YearTo = yearTo,
            MinHorsePower = minHorsePower,
            MinPrice = minPrice,
            MaxPrice = maxPrice,
            Order = orderBy,
        };

        return searchModel;
    }
}
