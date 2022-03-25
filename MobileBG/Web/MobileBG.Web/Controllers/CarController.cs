namespace MobileBG.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using MobileBG.Services.Data.Contracts;
using MobileBG.Web.ViewModels.Cars;
using System.Threading.Tasks;

public class CarController : BaseController
{
    private readonly ICarService carService;

    public CarController(ICarService carService)
    {
        this.carService = carService;
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var makes = await this.carService.GetAllMakesAsync();

        var model = new CreateCarInputViewModel();
        model.Makes = makes;
        return this.View(model);
    }

    [HttpPost]
    public IActionResult Create(CreateCarInputViewModel input)
    {
        return this.View();
    }
}
