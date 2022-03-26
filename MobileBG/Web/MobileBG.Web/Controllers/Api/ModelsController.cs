namespace MobileBG.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class ModelsController : ControllerBase
{
    private readonly IModelService modelService;

    public ModelsController(IModelService modelService)
    {
        this.modelService = modelService;
    }

    [HttpGet("ForMake")]
    public async Task<ICollection<DropdownDataViewModel>> GetModelsForBrand(Guid makeId)
    {
        return await this.modelService.GetModelsForMakeAsync(makeId);
    }
}
