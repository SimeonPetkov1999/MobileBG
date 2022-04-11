using MobileBG.Web.ViewModels.Blogs;

namespace MobileBG.Web.Areas.Administration.Controllers;

public class BlogController : AdministrationController
{
    private readonly IBlogService blogService;

    public BlogController(IBlogService blogService)
    {
        this.blogService = blogService;
    }

    public IActionResult Create()
    {
        return this.View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateBlogViewModel input)
    {
        await this.blogService.CreateBlogAsync(input);
        return this.View();
    }
}
