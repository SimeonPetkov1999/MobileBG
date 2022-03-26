namespace MobileBG.Services.Data.Contracts;

public interface IDropDownDataService
{
    public Task<ICollection<DropdownDataViewModel>> GetAllMakesAsync();

    public Task<ICollection<DropdownDataViewModel>> GetAllPetrolTypesAsync();

    public Task<ICollection<DropdownDataViewModel>> GetAllCitiesAsync();
}

