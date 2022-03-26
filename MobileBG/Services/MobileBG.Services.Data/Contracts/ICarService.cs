namespace MobileBG.Services.Data.Contracts;

public interface ICarService
{
    public Task<ICollection<DropdownDataViewModel>> GetAllMakesAsync();

    public Task<ICollection<DropdownDataViewModel>> GetAllPetrolTypesAsync();

    public Task<ICollection<DropdownDataViewModel>> GetAllCitiesAsync();

    public Task<Guid> CreateCarAsync(
        CreateCarInputViewModel model,
        string userId);
}
