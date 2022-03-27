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
    public async Task<IActionResult> Details(Guid id)
    {
        var model = await this.carService.SingleCarAsync(id);

        return this.View(model);
    }
}
