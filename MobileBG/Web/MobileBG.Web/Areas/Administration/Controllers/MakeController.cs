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
}
