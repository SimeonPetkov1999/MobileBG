namespace MobileBG.Services.Data.Contracts;

using MobileBG.Web.ViewModels.Blogs;

public interface IBlogService
{
    public Task CreateBlogAsync(CreateBlogViewModel input);
}
