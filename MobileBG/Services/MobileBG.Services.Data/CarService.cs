namespace MobileBG.Services.Data;

public class CarService : ICarService
{
    private readonly IRepository<MakeEntity> makeRepo;
    private readonly IRepository<PetrolTypeEntity> petrolTypeRepo;
    private readonly IRepository<CityEntity> cityTypeRepo;
    private readonly IRepository<CarEntity> carRepo;
    private readonly ICloudinaryService cloudinaryService;

    public CarService(
        IRepository<MakeEntity> makeRepo,
        IRepository<PetrolTypeEntity> petrolTypeRepo,
        IRepository<CityEntity> cityTypeRepo,
        IRepository<CarEntity> carRepo,
        ICloudinaryService cloudinaryService)
    {
        this.makeRepo = makeRepo;
        this.petrolTypeRepo = petrolTypeRepo;
        this.cityTypeRepo = cityTypeRepo;
        this.carRepo = carRepo;
        this.cloudinaryService = cloudinaryService;
    }

    public async Task<Guid> CreateCarAsync(CreateCarInputViewModel model, string userId)
    {
        var entity = new CarEntity()
        {
            Id = Guid.NewGuid(),
            MakeId = model.MakeId,
            ModelId = model.ModelId,
            CityId = model.CityId,
            PetrolTypeId = model.PetrolTypeId,
            HorsePower = model.HorsePower,
            Price = model.Price,
            Description = model.Description,
            UserId = userId,
        };

        var links = new List<string>();
        foreach (var imageFile in model.Images)
        {
            var link = await this.cloudinaryService.UploadAsync(imageFile, entity.Id);
            links.Add(link);
        }

        foreach (var link in links)
        {
            entity.Images.Add(new ImageEntity() { ImageUrl = link });
        }

        await this.carRepo.AddAsync(entity);
        await this.carRepo.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<ICollection<DropdownDataViewModel>> GetAllCitiesAsync()
    {
        var cities = await this.cityTypeRepo
            .AllAsNoTracking()
            .To<DropdownDataViewModel>()
            .ToListAsync();

        return cities;
    }

    public async Task<ICollection<DropdownDataViewModel>> GetAllMakesAsync()
    {
        var makes = await this.makeRepo
            .AllAsNoTracking()
            .To<DropdownDataViewModel>()
            .ToListAsync();

        return makes;
    }

    public async Task<ICollection<DropdownDataViewModel>> GetAllPetrolTypesAsync()
    {
        var types = await this.petrolTypeRepo
            .AllAsNoTracking()
            .To<DropdownDataViewModel>()
            .ToListAsync();

        return types;
    }
}
