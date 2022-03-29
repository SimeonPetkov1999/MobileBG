namespace MobileBG.Web.Controllers;

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

        return this.Content("Added");
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Details(Guid id)
    {
        var model = await this.carService.SingleCarAsync(id);

        return this.View(model);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Mine(int id = 1)
    {
        var userId = this.User.GetId();
        var itemsPerPage = 5;

        var model = await this.carService.AllCarsForUserAsync(userId, id, itemsPerPage);

        var viewModel = new SearchCarViewModel()
        {
            ItemsPerPage = itemsPerPage,
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
        var isValid = await this.carService.ValidateUserOwnsCarAsync(userId, Id);

        if (isValid)
        {
            await this.carService.DeleteCarAsync(Id);
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

        var itemsPerPage = 5;

        var model = await this.carService.AllApprovedCarsAsync(input, id, itemsPerPage);

        var viewModel = new SearchCarViewModel()
        {
            Makes = await this.dropDownDataService.GetAllMakesAsync(),
            Cities = await this.dropDownDataService.GetAllCitiesAsync(),
            PetrolTypes = await this.dropDownDataService.GetAllPetrolTypesAsync(),
            Cars = model.Cars,
            ItemsCount = model.Count,
            ItemsPerPage = itemsPerPage,
            PageNumber = id,
        };

        return this.View(viewModel);
    }
}
