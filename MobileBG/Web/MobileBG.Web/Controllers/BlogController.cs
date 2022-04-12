namespace MobileBG.Web.Controllers;

using MobileBG.Common;
using MobileBG.Web.ViewModels.Blogs;

public class BlogController : BaseController
{
    private readonly IBlogService blogService;

    public BlogController(IBlogService blogService)
    {
        this.blogService = blogService;
    }

    public async Task<IActionResult> Details(Guid Id)
    {
        var blog = await this.blogService.GetByIdAsync<BlogInfoViewModel>(Id);

        return this.View(blog);
    }

    public async Task<IActionResult> All(int Id = 1)
    {
        var model = await this.blogService.GetAllAsync(Id, GlobalConstants.BlogsPerPage);
        var viewModel = new AllBlogsViewModel()
        {
            Blogs = model.Blogs,
            ItemsCount = model.Count,
            ItemsPerPage = GlobalConstants.BlogsPerPage,
            PageNumber = Id,
        };
        return this.View(viewModel);
    }
}
