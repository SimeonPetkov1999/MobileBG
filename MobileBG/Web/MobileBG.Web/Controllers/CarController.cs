namespace MobileBG.Web.Controllers;
using MobileBG.Common;

public class CarController : BaseController
{
    private readonly ICarService carService;
    private readonly IDropDownDataService dropDownDataService;

    public CarController(
        ICarService carService,
        IDropDownDataService dropDownDataService)
    {
        this.carService = carService;
        this.dropDownDataService = dropDownDataService;
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

        this.TempData["Success"] = "You successfuly created a car. Wait for approval";

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

        var model = await this.carService.AllCarsForUserAsync(userId, id, GlobalConstants.ItemsPerPage);

        var viewModel = new SearchCarViewModel()
        {
            ItemsPerPage = GlobalConstants.ItemsPerPage,
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

        if (userIsOwner)
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

        var model = await this.carService.AllApprovedCarsAsync(input, id, GlobalConstants.ItemsPerPage);

        var viewModel = new SearchCarViewModel()
        {
            Makes = await this.dropDownDataService.GetAllMakesAsync(),
            Cities = await this.dropDownDataService.GetAllCitiesAsync(),
            PetrolTypes = await this.dropDownDataService.GetAllPetrolTypesAsync(),
            Cars = model.Cars,
            ItemsCount = model.Count,
            ItemsPerPage = GlobalConstants.ItemsPerPage,
            PageNumber = id,
        };

        return this.View(viewModel);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Edit(Guid Id)
    {
        var carIsValid = await this.carService.ValidateCarExistsAsync(Id);

        if (carIsValid == false)
        {
            this.NotFound();
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

        var model = await this.carService.AllCarsForUserAsync(Id, page, GlobalConstants.ItemsPerPage);

        var viewModel = new SearchCarViewModel()
        {
            ItemsPerPage = GlobalConstants.ItemsPerPage,
            PageNumber = page,
            ItemsCount = model.Count,
            Cars = model.Cars,
        };

        return this.View(viewModel);
    }
}
