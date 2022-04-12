namespace MobileBG.Services.Data.Contracts;

using MobileBG.Web.ViewModels.Blogs;

public interface IBlogService
{
    public Task CreateBlogAsync(CreateBlogViewModel input);

    public Task DeleteBlogAsync(Guid Id);

    public Task<T> GetByIdAsync<T>(Guid blogId);

    public Task<BlogDataViewModel> GetAllAsync(int page, int itemsPerPage);

    public Task EditBlogAsync(EditBlogViewModel model);
}
