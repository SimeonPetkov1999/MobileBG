namespace MobileBG.Services.Data;

using MobileBG.Web.ViewModels.Blogs;
using System;
using System.Collections.Generic;

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

    public async Task<BlogDataViewModel> GetAllAsync(int page, int itemsPerPage)
    {
        var blogs = await this.blogRepo
            .AllAsNoTracking()
            .OrderBy(x => x.CreatedOn)
            .Skip((page - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .To<BlogInfoViewModel>()
            .ToListAsync();

        return new BlogDataViewModel() { Blogs = blogs, Count = await this.blogRepo.AllAsNoTracking().CountAsync()};
    }

    public async Task<BlogInfoViewModel> GetByIdAsync(Guid blogId)
    {
        var blog = await this.blogRepo
            .AllAsNoTracking()
            .Where(x => x.Id == blogId)
            .To<BlogInfoViewModel>()
            .FirstOrDefaultAsync();

        return blog;
    }
}
