namespace MobileBG.Web.Areas.Administration.Controllers;

using MobileBG.Web.ViewModels.Administration.Dashboard;
using MobileBG.Services.Data;
using Microsoft.AspNetCore.Mvc;

public class DashboardController : AdministrationController
{
    public DashboardController()
    {
    }

    public IActionResult Index()
    {
        var viewModel = new IndexViewModel { SettingsCount = 5 };
        return this.View(viewModel);
    }
}
