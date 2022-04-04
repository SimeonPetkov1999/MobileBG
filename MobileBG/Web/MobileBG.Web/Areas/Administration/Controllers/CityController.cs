namespace MobileBG.Web.Areas.Administration.Controllers;

using MobileBG.Web.ViewModels.Cities;

public class CityController : AdministrationController
{
    private readonly ICityService cityService;

    public CityController(ICityService cityService)
    {
        this.cityService = cityService;
    }

    [HttpGet]
    public async Task<IActionResult> All(string keyWord)
    {
        var cities = await this.cityService.GetAllCitiesAsync(keyWord);

        var viewModel = new AllCitiesViewModel()
        {
            Cities = cities,
            KeyWord = keyWord,
        };

        return this.View(viewModel);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return this.View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCityInputModel inputModel)
    {
        if (!this.ModelState.IsValid)
        {
            return this.View(inputModel);
        }

        await this.cityService.CreateCityAsync(inputModel.Name);
        this.TempData["Success"] = $"City with name {inputModel.Name} created";
        return this.RedirectToAction(nameof(this.All));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid Id)
    {
        var isDeleted = await this.cityService.DeleteCityAsync(Id);
        if (!isDeleted)
        {
            this.TempData["Danger"] = "You can't delete this city because there are cars assosiated with it!";
            return this.RedirectToAction(nameof(this.All));
        }

        this.TempData["Success"] = $"City is deleted";
        return this.RedirectToAction(nameof(this.All));
    }
}
