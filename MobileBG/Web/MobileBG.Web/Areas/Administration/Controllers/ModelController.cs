namespace MobileBG.Web.Areas.Administration.Controllers;
public class ModelController : AdministrationController
{
    private readonly IMakeService makeService;
    private readonly IModelService modelService;

    public ModelController(
        IMakeService makeService,
        IModelService modelService)
    {
        this.makeService = makeService;
        this.modelService = modelService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(Guid makeId, string modelName)
    {
        var isCreated = await this.makeService.CreateModelForMakeAsync(makeId, modelName);

        if (!isCreated)
        {
            this.TempData["Danger"] = "Модел с това име вече съществува";
            return this.RedirectToAction("Details", "Make", new { Id = makeId });
        }

        this.TempData["Success"] = $"{modelName} е добавена!";
        return this.RedirectToAction("Details", "Make", new { Id = makeId });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAsync(Guid Id)
    {
        var result = await this.modelService.DeleteModelAsync(Id);

        if (!result.IsDeleted)
        {
            this.TempData["Danger"] = "Не можете да изтриете този модел, защото има коли асоциирани с нея";
            return this.RedirectToAction("Details", "Make", new { Id = result.MakeId });
        }

        this.TempData["Success"] = "Изтрито";
        return this.RedirectToAction("Details", "Make", new { Id = result.MakeId });
    }
}
