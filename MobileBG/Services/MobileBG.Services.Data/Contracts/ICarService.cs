namespace MobileBG.Services.Data.Contracts;

public interface ICarService
{
    public Task<Guid> CreateCarAsync(
        CreateCarInputViewModel model,
        string userId);

    public Task<SingleCarViewModel> SingleCarAsync(Guid carId);

    public Task<CarDataViewModel> AllApprovedCarsAsync(SearchCarViewModel input, int page, int itemsPerPage);

    public Task<CarDataViewModel> AllUnapprovedCarsAsync(int page, int itemsPerPage);

    public Task ApproveCarAsync(Guid carId);
}
