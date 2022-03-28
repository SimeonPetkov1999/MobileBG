namespace MobileBG.Services.Data.Contracts;

public interface ICarService
{
    public Task<Guid> CreateCarAsync(
        CreateCarInputViewModel model,
        string userId);

    public Task<SingleCarViewModel> SingleCarAsync(Guid carId);

    public Task<ICollection<CarInfoViewModel>> AllApprovedCarsAsync(SearchCarViewModel input, int page, int itemsPerPage);

    public Task<ICollection<CarInfoViewModel>> AllUnapprovedCarsAsync(int page, int itemsPerPage);

    public Task<int> CarsCountAsync(SearchCarViewModel input);
}
