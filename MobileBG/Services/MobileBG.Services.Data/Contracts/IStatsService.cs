namespace MobileBG.Services.Data.Contracts;

public interface IStatsService
{
    public Task<int> GetCountOfCarsForUserAsync(string userId);
}

