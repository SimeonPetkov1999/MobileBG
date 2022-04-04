namespace MobileBG.Services.Data.Contracts;

public interface IStatsService
{
    public Task<int> GetCountOfCarsForUserAsync(string userId);

    public Task<int> GetCountOfCars();

    public Task<int> GetCountOfMakes();

    public Task<int> GetCountOfUsers();
}

