namespace MobileBG.Services.Data.Contracts;

public interface ICarService
{
    public Task<Guid> CreateCarAsync(
        CreateCarInputViewModel model,
        string userId);

    public Task<SingleCarViewModel> SingleCarAsync(Guid carId);
}
