namespace MobileBG.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobileBG.Services.Data.Contracts;
using MobileBG.Web.Infrastructure;
using MobileBG.Web.ViewModels.Cars;
using System;
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
        var petrolTypes = await this.carService.GetAllPetrolTypesAsync();
        var cities = await this.carService.GetAllCitiesAsync();

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
            input.Makes = await this.carService.GetAllMakesAsync();
            input.PetrolTypes = await this.carService.GetAllPetrolTypesAsync();
            input.Cities = await this.carService.GetAllCitiesAsync();
            return this.View(input);
        }

        var userId = this.User.GetId();
        await this.carService.CreateCarAsync(input, userId);

        return this.Content("Added");
    }
}
