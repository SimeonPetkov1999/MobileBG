namespace MobileBG.Services.Data.Contracts;

using MobileBG.Web.ViewModels.Blogs;

public interface IBlogService
{
    public Task CreateBlogAsync(CreateBlogViewModel input);

    public Task<BlogInfoViewModel> GetByIdAsync(Guid blogId);

    public Task<BlogDataViewModel> GetAllAsync(int page, int itemsPerPage);
}
