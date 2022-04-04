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

    public Task DeleteCarAsync(Guid carId);

    public Task<CarDataViewModel> AllCarsForUserAsync(string userId, int page, int itemsPerPage);

    public Task<bool> ValidateUserOwnsCarAsync(string userId, Guid carId);

    public Task<bool> ValidateCarExistsAsync(Guid carId);

    public Task<EditCarViewModel> GetCarDataAsync(Guid carId);

    public Task UpdateCarDataAsync(EditCarViewModel input);

    public Task<ICollection<CarInfoViewModel>> GetLastAddedCarsAsync();
}
