namespace MobileBG.Web.ViewModels.Blogs;

public class AllBlogsViewModel : PagingViewModel
{
    public ICollection<BlogInfoViewModel> Blogs { get; set; }
}
