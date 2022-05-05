namespace MobileBG.Web.Areas.Administration.Controllers;
using MobileBG.Web.ViewModels.Makes;

public class MakeController : AdministrationController
{
    private readonly IMakeService makeService;

    public MakeController(IMakeService makeService)
    {
        this.makeService = makeService;
    }

    [HttpGet]
    public async Task<IActionResult> All(string keyWord)
    {
        var makes = await this.makeService.GetAllMakesAsync(keyWord);

        var model = new AllMakesViewModel() { KeyWord = keyWord, Makes = makes };
        return this.View(model);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return this.View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(MakeInputModel input)
    {
        if (!this.ModelState.IsValid)
        {
            return this.View(input);
        }

        await this.makeService.CreateMakeAsync(input.Name);

        this.TempData["Success"] = $"Успешно създадохте марка с име {input.Name}";

        return this.RedirectToAction(nameof(this.All));
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid Id)
    {
        var model = await this.makeService.GetMakeDetialsAsync(Id);

        return this.View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid Id)
    {
        var model = await this.makeService.GetMakeDetialsAsync(Id);

        var viewModel = new EditMakeViewModel()
        {
            Id = Id,
            Name = model.Name,
        };

        return this.View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditMakeViewModel model)
    {
        if (!this.ModelState.IsValid)
        {
            this.TempData["Danger"] = $"Марка с име {model.Name} вече съществува!";
            return this.RedirectToAction(nameof(this.Edit), new { Id = model.Id });
        }

        await this.makeService.EditMakeAsync(model);

        this.TempData["Success"] = $"Името е успешно сменено на {model.Name}";

        return this.RedirectToAction(nameof(this.Edit), new { Id = model.Id });
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid Id)
    {
        var isDeleted = await this.makeService.DeleteMakeAsync(Id);
        if (!isDeleted)
        {
            this.TempData["Danger"] = "Не можете да изтриете тази марка, защото има модели асоциирани с нея";
            return this.RedirectToAction(nameof(this.All), new { Id = Id });
        }

        this.TempData["Success"] = "Марката е изтрита!";
        return this.RedirectToAction(nameof(this.All), new { Id = Id });
    }
}
