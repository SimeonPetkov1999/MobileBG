namespace MobileBG.Services.Data;

using Microsoft.AspNetCore.Identity;

public class StatsService : IStatsService
{
    private readonly IRepository<CarEntity> carsRepo;
    private readonly IRepository<MakeEntity> makeRepo;
    private readonly UserManager<ApplicationUser> userManager;

    public StatsService(
        IRepository<CarEntity> carsRepo,
        IRepository<MakeEntity> makeRepo,
        UserManager<ApplicationUser> userManager)
    {
        this.carsRepo = carsRepo;
        this.makeRepo = makeRepo;
        this.userManager = userManager;
    }

    public async Task<int> GetCountOfCarsForUserAsync(string userId)
    => await this.carsRepo
        .AllAsNoTracking()
        .Where(x => x.UserId == userId)
        .CountAsync();

    public async Task<int> GetCountOfCars()
    => await this.carsRepo
        .AllAsNoTracking()
        .CountAsync();

    public async Task<int> GetCountOfMakes()
    => await this.makeRepo
        .AllAsNoTracking()
        .CountAsync();

    public async Task<int> GetCountOfUsers()
    => await this.userManager
        .Users
        .CountAsync();
}
