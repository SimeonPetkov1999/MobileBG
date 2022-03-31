namespace MobileBG.Services.Data;

public class DropDownDataService : IDropDownDataService
{
    private readonly IRepository<MakeEntity> makeRepo;
    private readonly IRepository<PetrolTypeEntity> petrolTypeRepo;
    private readonly IRepository<CityEntity> cityTypeRepo;

    public DropDownDataService(
        IRepository<MakeEntity> makeRepo,
        IRepository<PetrolTypeEntity> petrolTypeRepo,
        IRepository<CityEntity> cityTypeRepo)
    {
        this.makeRepo = makeRepo;
        this.petrolTypeRepo = petrolTypeRepo;
        this.cityTypeRepo = cityTypeRepo;
    }

    public async Task<ICollection<DropdownDataViewModel>> GetAllCitiesAsync()
    {
        var cities = await this.cityTypeRepo
            .AllAsNoTracking()
            .To<DropdownDataViewModel>()
            .OrderBy(x => x.Name)
            .ToListAsync();

        return cities;
    }

    public async Task<ICollection<DropdownDataViewModel>> GetAllMakesAsync()
    {
        var makes = await this.makeRepo
            .AllAsNoTracking()
            .To<DropdownDataViewModel>()
            .OrderBy(x => x.Name)
            .ToListAsync();

        return makes;
    }

    public async Task<ICollection<DropdownDataViewModel>> GetAllPetrolTypesAsync()
    {
        var types = await this.petrolTypeRepo
            .AllAsNoTracking()
            .To<DropdownDataViewModel>()
            .OrderBy(x => x.Name)
            .ToListAsync();

        return types;
    }
}

