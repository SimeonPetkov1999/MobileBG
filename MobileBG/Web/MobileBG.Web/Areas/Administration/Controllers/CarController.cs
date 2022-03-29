﻿namespace MobileBG.Web.Areas.Administration.Controllers;
public class CarController : AdministrationController
{
    private readonly ICarService carService;

    public CarController(ICarService carService)
    {
        this.carService = carService;
    }

    public async Task<IActionResult> Unapproved(int id = 1)
    {
        if (id < 1)
        {
            return this.NotFound();
        }

        var itemsPerPage = 5;

        var model = await this.carService.AllUnapprovedCarsAsync(id, itemsPerPage);

        var viewModel = new SearchCarViewModel()
        {
            ItemsPerPage = itemsPerPage,
            PageNumber = id,
            ItemsCount = model.Count,
            Cars = model.Cars,
        };

        return this.View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Approve(Guid Id)
    {
        await this.carService.ApproveCarAsync(Id);
        this.TempData["Success"] = "Car is approved!";
        return this.RedirectToAction(nameof(this.Unapproved), 1);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid Id)
    {
        await this.carService.DeleteCarAsync(Id);
        this.TempData["Danger"] = "Car is deleted";
        return this.RedirectToAction(nameof(this.Unapproved), 1);
    }
}