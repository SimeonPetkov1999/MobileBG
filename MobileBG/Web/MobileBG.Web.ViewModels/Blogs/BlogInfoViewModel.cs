namespace MobileBG.Web.ViewModels.Blogs;

public class BlogInfoViewModel : IMapFrom<BlogEntity>
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public string ImageUrl { get; set; }

    public DateTime CreatedOn { get; set; }
}
