namespace MobileBG.Web.ViewModels.Blogs;
public class EditBlogViewModel : IMapFrom<BlogEntity>
{
    public Guid Id { get; set; }

    [Display(Name = "Title")]
    [Required]
    [MinLength(3, ErrorMessage = "Enter a Title with minimum length of 3 symbols")]
    public string Title { get; set; }

    [Required]
    [MinLength(50, ErrorMessage = "Enter a Content with minimum length of 50 symbols")]
    public string Content { get; set; }

    [Display(Name = "Select new image")]
    [AllowedExtensions(".png", ".jpg", ".jpeg")]
    public IFormFile Image { get; set; }
}
