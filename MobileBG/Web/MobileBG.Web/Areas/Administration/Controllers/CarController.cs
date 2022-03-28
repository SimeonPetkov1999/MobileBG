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

        var cars = await this.carService.AllUnapprovedCarsAsync(id, itemsPerPage);
        var model = new SearchCarViewModel()
        {
            ItemsPerPage = itemsPerPage,
            PageNumber = id,
            ItemsCount = cars.Count,
            Cars = cars,
        };

        return this.View(model);
    }
}
