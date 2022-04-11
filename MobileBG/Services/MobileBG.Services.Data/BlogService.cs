namespace MobileBG.Services.Data;

using MobileBG.Web.ViewModels.Blogs;

public class BlogService : IBlogService
{
    private readonly IRepository<BlogEntity> blogRepo;

    public BlogService(IRepository<BlogEntity> blogRepo)
    {
        this.blogRepo = blogRepo;
    }

    public async Task CreateBlogAsync(CreateBlogViewModel input)
    {
        var entity = new BlogEntity() { Title = input.Title, Content = input.Content };

        await this.blogRepo.AddAsync(entity);
        await this.blogRepo.SaveChangesAsync();
    }
}
