namespace MobileBG.Services.Data;

using MobileBG.Services.Messaging;
using MobileBG.Web.ViewModels;
using System.Collections.Generic;

public class CarService : ICarService
{
    private readonly IRepository<CarEntity> carRepo;
    private readonly IRepository<ImageEntity> imageRepo;
    private readonly ICloudinaryService cloudinaryService;
    private readonly IEmailSender emailSender;

    public CarService(
        IRepository<CarEntity> carRepo,
        IRepository<ImageEntity> imageRepo,
        ICloudinaryService cloudinaryService,
        IEmailSender emailSender)
    {
        this.carRepo = carRepo;
        this.imageRepo = imageRepo;
        this.cloudinaryService = cloudinaryService;
        this.emailSender = emailSender;
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

        var cars = await this.GetCarDataAsync(page, itemsPerPage, query);

        return new CarDataViewModel() { Cars = cars, Count = count };
    }

    public async Task ApproveCarAsync(Guid carId)
    {
        var car = this.carRepo
            .AllAsNoTracking()
            .Include(x => x.User)
            .Include(x => x.Make)
            .Include(x => x.Model)
            .FirstOrDefault(x => x.Id == carId);

        if (car != null)
        {
            car.IsApproved = true;
            this.carRepo.Update(car);
            await this.emailSender.SendEmailAsync("mobile-bg@abv.bg", "MobileBG", car.User.Email, "You car is approved", $"Congratulations, your {car.Make.Name} {car.Model.Name} is approved!");
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
             .Where(x => x.UserId == userId)
             .Where(x => x.IsApproved == true);

        var count = query.Count();

        var cars = await this.GetCarDataAsync(page, itemsPerPage, query);

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

    public async Task<EditCarViewModel> GetCarDataAsync(Guid carId)
    {
        var car = await this.carRepo
            .AllAsNoTracking()
            .Include(x => x.City)
            .Include(x => x.Make)
            .Include(x => x.Model)
            .Include(x => x.PetrolType)
            .Include(x => x.Images)
            .Where(x => x.Id == carId)
            .To<EditCarViewModel>()
            .FirstOrDefaultAsync();

        return car;
    }

    public async Task UpdateCarDataAsync(EditCarViewModel input)
    {
        var car = this.carRepo
            .All()
            .FirstOrDefault(x => x.Id == input.Id);

        car.MakeId = input.MakeId;
        car.ModelId = input.ModelId;
        car.CityId = input.CityId;
        car.PetrolTypeId = input.PetrolTypeId;
        car.YearMade = input.YearMade;
        car.Price = input.Price;
        car.Description = input.Description;
        car.HorsePower = input.HorsePower;
        car.Km = input.Km;

        if (input.Images != null)
        {
            var links = new List<string>();
            foreach (var imageFile in input.Images)
            {
                var link = await this.cloudinaryService.UploadAsync(imageFile, car.Id);
                links.Add(link);
            }

            foreach (var link in links)
            {
                car.Images.Add(new ImageEntity() { ImageUrl = link });
            }
        }

        car.IsApproved = false;
        this.carRepo.Update(car);
        await this.carRepo.SaveChangesAsync();
    }

    public async Task<bool> ValidateCarExistsAsync(Guid carId)
        => await this.carRepo
            .AllAsNoTracking()
            .AnyAsync(x => x.Id == carId);

    public async Task<ICollection<CarInfoViewModel>> GetLastAddedCarsAsync()
        => await this.carRepo.AllAsNoTracking()
         .Include(x => x.Make)
         .Include(x => x.Model)
         .Where(x => x.IsApproved == true)
         .OrderByDescending(x => x.CreatedOn)
         .Take(7)
         .To<CarInfoViewModel>()
         .ToListAsync();

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
            OrderBy.DateAdded => query.OrderByDescending(x => x.CreatedOn),
            OrderBy.PriceAsc => query.OrderBy(x => x.Price),
            OrderBy.PriceDesc => query.OrderByDescending(x => x.Price),
            OrderBy.Newest => query.OrderByDescending(x => x.YearMade),
            OrderBy.Oldest => query.OrderBy(x => x.YearMade),
            _ => query.OrderByDescending(x => x.CreatedOn),
        };
        return query;
    }

    private async Task<List<CarInfoViewModel>> GetCarDataAsync(int page, int itemsPerPage, IQueryable<CarEntity> query)
        => await query
             .Include(x => x.Make)
             .Include(x => x.Model)
             .OrderBy(x => x.CreatedOn)
             .Skip((page - 1) * itemsPerPage)
             .Take(itemsPerPage)
             .To<CarInfoViewModel>()
             .ToListAsync();
}
