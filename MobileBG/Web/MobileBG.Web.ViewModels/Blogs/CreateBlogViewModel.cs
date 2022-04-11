namespace MobileBG.Web.ViewModels.Blogs;

public class CreateBlogViewModel
{
    [Display(Name = "Title")]
    public string Title { get; set; }

    public string Content { get; set; }

    [Display(Name = "Select main image")]
    public IFormFile Image { get; set; }
}
