namespace MobileBG.Services.Data.Tests.ServiceTests;

using Microsoft.AspNetCore.Identity;
using MobileBG.Services.Data.Contracts;

public class StatsServiceTests
{
    private Mock<IRepository<CarEntity>> carsRepoMock;
    private Mock<IRepository<MakeEntity>> makeRepoMock;
    private Mock<UserManager<ApplicationUser>> userManagerMock;
    private IStatsService statsService;

    public StatsServiceTests()
    {
        AutoMapper.Configure();
    }

    [Fact]
    public async void GetCountOfUsersAsync()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var users = new List<ApplicationUser>()
        {
            new() { Id = Guid.NewGuid().ToString() },
            new() { Id = Guid.NewGuid().ToString() },
            new() { Id = Guid.NewGuid().ToString() },
        }.AsQueryable();

        var usersMock = users.BuildMock();

        this.userManagerMock.Setup(x => x.Users).Returns(usersMock);

        var result = await this.statsService.GetCountOfUsers();
        Assert.Equal(3, result);
    }

    [Fact]
    public async void GetCountOfCarsForUserAsyncShouldReturnCount()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var userId = Guid.NewGuid().ToString();
        var cars = new List<CarEntity>()
        {
            new() { Id = Guid.NewGuid(), UserId = userId },
            new() { Id = Guid.NewGuid(), UserId = userId },
            new() { Id = Guid.NewGuid(), UserId = userId },
        }.AsQueryable();

        var carsMock = cars.BuildMock();

        this.carsRepoMock.Setup(x => x.AllAsNoTracking()).Returns(carsMock);

        var result = await this.statsService.GetCountOfCarsForUserAsync(userId);
        Assert.Equal(3, result);
    }

    [Fact]
    public async void GetCountOfCarsShouldReturnCount()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var userId = Guid.NewGuid().ToString();
        var cars = new List<CarEntity>()
        {
            new() { Id = Guid.NewGuid() },
            new() { Id = Guid.NewGuid() },
            new() { Id = Guid.NewGuid() },
        }.AsQueryable();

        var carsMock = cars.BuildMock();

        this.carsRepoMock.Setup(x => x.AllAsNoTracking()).Returns(carsMock);

        var result = await this.statsService.GetCountOfCars();
        Assert.Equal(3, result);
    }

    [Fact]
    public async void GetCountOfMakes()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var userId = Guid.NewGuid().ToString();
        var makes = new List<MakeEntity>()
        {
            new() { Id = Guid.NewGuid() },
            new() { Id = Guid.NewGuid() },
            new() { Id = Guid.NewGuid() },
        }.AsQueryable();

        var makesMock = makes.BuildMock();

        this.makeRepoMock.Setup(x => x.AllAsNoTracking()).Returns(makesMock);

        var result = await this.statsService.GetCountOfMakes();
        Assert.Equal(3, result);
    }

    private void InitializeRepos()
    {
        this.carsRepoMock = new Mock<IRepository<CarEntity>>();
        this.makeRepoMock = new Mock<IRepository<MakeEntity>>();

        var store = new Mock<IUserStore<ApplicationUser>>();
        this.userManagerMock = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

        this.statsService = new StatsService(this.carsRepoMock.Object, this.makeRepoMock.Object, this.userManagerMock.Object);
    }
}
