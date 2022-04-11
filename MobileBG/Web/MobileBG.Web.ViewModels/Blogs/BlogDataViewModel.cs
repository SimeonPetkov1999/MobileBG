namespace MobileBG.Web.ViewModels.Blogs;

public class BlogDataViewModel
{
    public ICollection<BlogInfoViewModel> Blogs { get; set; }

    public int Count { get; set; }
}
