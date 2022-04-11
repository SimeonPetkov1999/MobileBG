namespace MobileBG.Web.Areas.Administration.Controllers;

public class BlogController : AdministrationController
{
    public BlogController()
    {

    }

    public IActionResult Create()
    {
        return this.View();
    }
}
