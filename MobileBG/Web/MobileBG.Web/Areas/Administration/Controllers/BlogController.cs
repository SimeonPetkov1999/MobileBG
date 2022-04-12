namespace MobileBG.Web.Areas.Administration.Controllers;

using MobileBG.Web.ViewModels.Blogs;

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
        if (!this.ModelState.IsValid)
        {
            return this.View(input);
        }

        await this.blogService.CreateBlogAsync(input);
        this.TempData["Success"] = $"Blog {input.Title} is created!";
        return this.RedirectToAction("All", "Blog", new { area = string.Empty });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid Id)
    {
        var blog = await this.blogService.GetByIdAsync<EditBlogViewModel>(Id);
        return this.View(blog);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditBlogViewModel input)
    {
        await this.blogService.EditBlogAsync(input);
        this.TempData["Success"] = "Blog is updated!";
        return this.RedirectToAction("Details", "Blog", new { area = string.Empty, Id = input.Id });
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid Id)
    {
        await this.blogService.DeleteBlogAsync(Id);
        this.TempData["Danger"] = "Blog is delted!";
        return this.RedirectToAction("All", "Blog", new { area = string.Empty });
    }
}
