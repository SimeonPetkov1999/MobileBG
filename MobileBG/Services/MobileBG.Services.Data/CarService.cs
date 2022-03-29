namespace MobileBG.Services.Data;

using MobileBG.Web.ViewModels;

public class CarService : ICarService
{
    private readonly IRepository<CarEntity> carRepo;
    private readonly IRepository<ImageEntity> imageRepo;
    private readonly ICloudinaryService cloudinaryService;

    public CarService(
        IRepository<CarEntity> carRepo,
        IRepository<ImageEntity> imageRepo,
        ICloudinaryService cloudinaryService)
    {
        this.carRepo = carRepo;
        this.imageRepo = imageRepo;
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

    public async Task<CarDataViewModel> AllApprovedCarsAsync(SearchCarViewModel input, int page, int itemsPerPage)
    {
        var query = this.carRepo.AllAsNoTracking().Where(x => x.IsApproved == true);

        query = this.ApplyFilters(input, query);

        var count = query.Count();
        var cars = await query
             .Include(x => x.Make)
             .Include(x => x.Model)
             .Skip((page - 1) * itemsPerPage)
             .Take(itemsPerPage)
             .To<CarInfoViewModel>()
             .ToListAsync();

        return new CarDataViewModel() { Cars = cars, Count = count };
    }

    public async Task<CarDataViewModel> AllUnapprovedCarsAsync(int page, int itemsPerPage)
    {
        var query = this.carRepo
             .AllAsNoTracking()
             .Where(x => x.IsApproved == false);

        var count = await query.CountAsync();

        var cars = await this.GetCarData(page, itemsPerPage, query);

        return new CarDataViewModel() { Cars = cars, Count = count };
    }

    public async Task ApproveCarAsync(Guid carId)
    {
        var car = this.carRepo
            .AllAsNoTracking()
            .FirstOrDefault(x => x.Id == carId);

        if (car != null)
        {
            car.IsApproved = true;
            this.carRepo.Update(car);
            await this.carRepo.SaveChangesAsync();
        }
    }

    public async Task DeleteCarAsync(Guid carId)
    {
        var car = this.carRepo
            .AllAsNoTracking()
            .Include(x => x.Images)
            .FirstOrDefault(x => x.Id == carId);

        if (car != null)
        {
            foreach (var image in car.Images)
            {
                this.imageRepo.Delete(image);
            }

            await this.imageRepo.SaveChangesAsync();
            await this.cloudinaryService.DeleteAllAsync(car.Id);

            this.carRepo.Delete(car);
            await this.carRepo.SaveChangesAsync();
        }
    }

    public async Task<CarDataViewModel> AllCarsForUserAsync(string userId, int page, int itemsPerPage)
    {
        var query = this.carRepo
             .AllAsNoTracking()
             .Where(x => x.UserId == userId);

        var count = query.Count();

        var cars = await this.GetCarData(page, itemsPerPage, query);

        var model = new CarDataViewModel() { Cars = cars, Count = count };

        return model;
    }

    public async Task<bool> ValidateUserOwnsCarAsync(string userId, Guid carId)
    {
        var car = await this.carRepo
            .AllAsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == carId);

        return car.UserId == userId;
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

        if (input.CityId != null)
        {
            query = query.Where(x => x.CityId == input.CityId);
        }

        if (input.PetrolTypeId != null)
        {
            query = query.Where(x => x.PetrolTypeId == input.PetrolTypeId);
        }

        if (input.YearFrom != null)
        {
            query = query.Where(x => x.YearMade >= input.YearFrom);
        }

        if (input.YearTo != null)
        {
            query = query.Where(x => x.YearMade <= input.YearTo);
        }

        if (input.MaxPrice != null)
        {
            query = query.Where(x => x.Price <= input.MaxPrice);
        }

        if (input.MinPrice != null)
        {
            query = query.Where(x => x.Price >= input.MinPrice);
        }

        if (input.MinHorsePower != null)
        {
            query = query.Where(x => x.HorsePower >= input.MinHorsePower);
        }

        query = input.Order switch
        {
            OrderBy.DateAdded => query.OrderBy(x => x.CreatedOn),
            OrderBy.PriceAsc => query.OrderBy(x => x.Price),
            OrderBy.PriceDesc => query.OrderByDescending(x => x.Price),
            OrderBy.Newest => query.OrderByDescending(x => x.YearMade),
            OrderBy.Oldest => query.OrderBy(x => x.YearMade),
            _ => query.OrderBy(x => x.CreatedOn),
        };
        return query;
    }

    private async Task<List<CarInfoViewModel>> GetCarData(int page, int itemsPerPage, IQueryable<CarEntity> query)
        => await query
             .Include(x => x.Make)
             .Include(x => x.Model)
             .OrderBy(x => x.CreatedOn)
             .Skip((page - 1) * itemsPerPage)
             .Take(itemsPerPage)
             .To<CarInfoViewModel>()
             .ToListAsync();
}
