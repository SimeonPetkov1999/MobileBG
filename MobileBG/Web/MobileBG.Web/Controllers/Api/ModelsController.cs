namespace MobileBG.Web.Controllers.Api;

using Microsoft.AspNetCore.Mvc;
using MobileBG.Services.Data.Contracts;
using MobileBG.Web.ViewModels.Cars;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
