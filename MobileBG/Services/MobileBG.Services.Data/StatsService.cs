namespace MobileBG.Services.Data;
public class StatsService : IStatsService
{
    private readonly IRepository<CarEntity> carsRepo;

    public StatsService(IRepository<CarEntity> carsRepo)
    {
        this.carsRepo = carsRepo;
    }

    public async Task<int> GetCountOfCarsForUserAsync(string userId)
        => await this.carsRepo
            .AllAsNoTracking()
            .Where(x => x.UserId == userId)
            .CountAsync();
}
