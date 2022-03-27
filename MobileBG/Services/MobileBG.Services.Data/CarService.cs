namespace MobileBG.Services.Data;

public class CarService : ICarService
{
    private readonly IRepository<CarEntity> carRepo;
    private readonly ICloudinaryService cloudinaryService;

    public CarService(
        IRepository<CarEntity> carRepo,
        ICloudinaryService cloudinaryService)
    {
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
            YearMade = model.YearMade,
            Km = model.Km,
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

    public async Task<SingleCarViewModel> SingleCarAsync(Guid carId)
    {
        var car = await this.carRepo
             .AllAsNoTracking()
             .Include(x => x.Make)
             .Include(x => x.Model)
             .Include(x => x.PetrolType)
             .Include(x => x.User)
             .Where(x => x.Id == carId)
             .To<SingleCarViewModel>()
             .FirstOrDefaultAsync();

        return car;
    }

    public async Task<ICollection<CarInfoViewModel>> AllCarsAsync(SearchCarViewModel input, int page, int itemsPerPage)
    {
        var query = this.carRepo.AllAsNoTracking();

        query = this.ApplyFilters(input, query);

        var cars = await query
                         .Include(x => x.Make)
                         .Include(x => x.Model)
                         .Skip((page - 1) * itemsPerPage)
                         .Take(itemsPerPage)
                         .To<CarInfoViewModel>()
                         .ToListAsync();

        return cars;
    }

    public async Task<int> CarsCountAsync(SearchCarViewModel input)
    {
        var query = this.carRepo.AllAsNoTracking();

        query = this.ApplyFilters(input, query);
        return await query.CountAsync();
    }

    private IQueryable<CarEntity> ApplyFilters(SearchCarViewModel input, IQueryable<CarEntity> query)
    {
        if (input.MakeId != null)
        {
            query = query.Where(x => x.MakeId == input.MakeId);
        }

        if (input.ModelId != null)
        {
            query = query.Where(x => x.ModelId == input.ModelId);
        }

        return query;
    }
}
