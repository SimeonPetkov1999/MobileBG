namespace MobileBG.Web.Areas.Administration.Controllers;
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

        var itemsPerPage = 2;

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
        this.TempData["Success"] = "You succesfully approved the car!";
        return this.RedirectToAction(nameof(this.Unapproved), 1);
    }
}
