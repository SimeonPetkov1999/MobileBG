namespace MobileBG.Web.Controllers;
using MobileBG.Common;
using OpenHtmlToPdf;

public class CarController : BaseController
{
    private readonly ICarService carService;
    private readonly IDropDownDataService dropDownDataService;
    private readonly IStatsService statsService;

    public CarController(
        ICarService carService,
        IDropDownDataService dropDownDataService,
        IStatsService statsService)
    {
        this.carService = carService;
        this.dropDownDataService = dropDownDataService;
        this.statsService = statsService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Create()
    {
        var makes = await this.dropDownDataService.GetAllMakesAsync();
        var petrolTypes = await this.dropDownDataService.GetAllPetrolTypesAsync();
        var cities = await this.dropDownDataService.GetAllCitiesAsync();

        var model = new CreateCarInputViewModel()
        {
            Makes = makes,
            PetrolTypes = petrolTypes,
            Cities = cities,
        };

        return this.View(model);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAsync(CreateCarInputViewModel input)
    {
        if (this.ModelState.IsValid == false)
        {
            input.Makes = await this.dropDownDataService.GetAllMakesAsync();
            input.PetrolTypes = await this.dropDownDataService.GetAllPetrolTypesAsync();
            input.Cities = await this.dropDownDataService.GetAllCitiesAsync();
            return this.View(input);
        }

        var userId = this.User.GetId();
        await this.carService.CreateCarAsync(input, userId);

        this.TempData["Success"] = "You successfuly created a car. You will recieve an email when your car is approved.";

        return this.RedirectToAction(nameof(this.Mine), 1);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Details(Guid id)
    {
        var carIsValid = await this.carService.ValidateCarExistsAsync(id);
        if (carIsValid == false)
        {
            return this.NotFound();
        }

        var model = await this.carService.SingleCarAsync(id);
        model.UsersCars = await this.statsService.GetCountOfCarsForUserAsync(model.UserId);

        return this.View(model);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Mine(int id = 1)
    {
        if (id < 1)
        {
            return this.NotFound();
        }

        var userId = this.User.GetId();

        var model = await this.carService.AllCarsForUserAsync(userId, id, GlobalConstants.CarsPerPage);

        var viewModel = new SearchCarViewModel()
        {
            ItemsPerPage = GlobalConstants.CarsPerPage,
            PageNumber = id,
            ItemsCount = model.Count,
            Cars = model.Cars,
        };

        return this.View(viewModel);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Delete(Guid Id)
    {
        var userId = this.User.GetId();
        var userIsOwner = await this.carService.ValidateUserOwnsCarAsync(userId, Id);

        if (userIsOwner || this.User.IsInRole(GlobalConstants.AdministratorRoleName))
        {
            await this.carService.DeleteCarAsync(Id);
            this.TempData["Success"] = "You succesfully deleted the car!";
        }

        return this.RedirectToAction(nameof(this.Mine), 1);
    }

    [HttpGet]
    public async Task<IActionResult> All(SearchCarViewModel input, int id = 1)
    {
        if (id < 1)
        {
            return this.NotFound();
        }

        var model = await this.carService.AllApprovedCarsAsync(input, id, GlobalConstants.CarsPerPage);

        var viewModel = new SearchCarViewModel()
        {
            Makes = await this.dropDownDataService.GetAllMakesAsync(),
            Cities = await this.dropDownDataService.GetAllCitiesAsync(),
            PetrolTypes = await this.dropDownDataService.GetAllPetrolTypesAsync(),
            Cars = model.Cars,
            ItemsCount = model.Count,
            ItemsPerPage = GlobalConstants.CarsPerPage,
            PageNumber = id,
        };

        return this.View(viewModel);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Edit(Guid Id)
    {
        var carIsValid = await this.carService.ValidateCarExistsAsync(Id);
        var userOwnsCar = await this.carService.ValidateUserOwnsCarAsync(this.User.GetId(), Id);

        if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName) && (carIsValid == false || userOwnsCar == false))
        {
            return this.NotFound();
        }

        var model = await this.carService.GetCarDataAsync(Id);
        model.Cities = await this.dropDownDataService.GetAllCitiesAsync();
        model.Makes = await this.dropDownDataService.GetAllMakesAsync();
        model.PetrolTypes = await this.dropDownDataService.GetAllPetrolTypesAsync();

        return this.View(model);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Edit(EditCarViewModel input)
    {
        if (this.ModelState.IsValid)
        {
            await this.carService.UpdateCarDataAsync(input);
            this.TempData["Success"] = "You succesfully updated the car! Wait for approval.";
            return this.RedirectToAction(nameof(this.Mine), 1);
        }

        var model = await this.carService.GetCarDataAsync(input.Id);

        model.Cities = await this.dropDownDataService.GetAllCitiesAsync();
        model.Makes = await this.dropDownDataService.GetAllMakesAsync();
        model.PetrolTypes = await this.dropDownDataService.GetAllPetrolTypesAsync();

        return this.View(model);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> ForUser(string Id, [FromQuery] int page)
    {
        if (page <= 0)
        {
            return this.NotFound();
        }

        var model = await this.carService.AllCarsForUserAsync(Id, page, GlobalConstants.CarsPerPage);

        var viewModel = new SearchCarViewModel()
        {
            ItemsPerPage = GlobalConstants.CarsPerPage,
            PageNumber = page,
            ItemsCount = model.Count,
            Cars = model.Cars,
        };

        return this.View(viewModel);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Pdf(Guid Id)
    {
        var carIsValid = await this.carService.ValidateCarExistsAsync(Id);
        var userOwnsCar = await this.carService.ValidateUserOwnsCarAsync(this.User.GetId(), Id);

        if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName) && (carIsValid == false || userOwnsCar == false))
        {
            return this.NotFound();
        }

        var car = await this.carService.SingleCarAsync(Id);

        var viewHtml = await this.RenderViewAsync("Pdf", car);

        var pdf = OpenHtmlToPdf.Pdf
            .From(viewHtml)
            .OfSize(PaperSize.A4)
            .Landscape()
            .Content();

        return this.File(pdf, "application/octet-stream", $"{car.Make}_{car.Model}.pdf");
    }
}
