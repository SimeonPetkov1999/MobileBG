namespace MobileBG.Web.Controllers;

using System.Diagnostics;
using MobileBG.Web.ViewModels;

public class HomeController : BaseController
{
    private readonly IStatsService statsService;
    private readonly ICarService carService;

    public HomeController(
        IStatsService statsService,
        ICarService carService)
    {
        this.statsService = statsService;
        this.carService = carService;
    }

    public async Task<IActionResult> Index()
    {
        var viewModel = new IndexViewModel()
        {
            CarsCount = await this.statsService.GetCountOfCars(),
            MakesCount = await this.statsService.GetCountOfMakes(),
            UsersCount = await this.statsService.GetCountOfUsers(),
            Cars = await this.carService.GetLastAddedCarsAsync(),
        };

        return this.View(viewModel);
    }

    public IActionResult Privacy()
    {
        return this.View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return this.View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
    }

    public IActionResult Chat()
    {
        return this.View();
    }

}
