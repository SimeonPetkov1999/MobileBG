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

        return this.RedirectToAction(nameof(this.All));
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid Id)
    {
        var model = await this.makeService.GetMakeDetialsAsync(Id);

        return this.View(model);
    }
}
