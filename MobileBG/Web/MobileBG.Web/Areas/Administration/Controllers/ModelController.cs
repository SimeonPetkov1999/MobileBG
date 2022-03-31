namespace MobileBG.Web.Areas.Administration.Controllers;
public class ModelController : AdministrationController
{
    private readonly IMakeService makeService;

    public ModelController(IMakeService makeService)
    {
        this.makeService = makeService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(Guid makeId, string modelName)
    {
        var isCreated = await this.makeService.CreateModelForMakeAsync(makeId, modelName);

        if (!isCreated)
        {
            this.TempData["Danger"] = "There is already model with this name!";
            return this.RedirectToAction("Details", "Make", new { Id = makeId });
        }

        return this.RedirectToAction("Details", "Make", new { Id = makeId });
    }
}
