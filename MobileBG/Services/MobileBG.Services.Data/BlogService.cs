namespace MobileBG.Services.Data;

using MobileBG.Web.ViewModels.Blogs;
using System;
using System.Collections.Generic;

public class BlogService : IBlogService
{
    private readonly IRepository<BlogEntity> blogRepo;
    private readonly ICloudinaryService cloudinaryService;

    public BlogService(
        IRepository<BlogEntity> blogRepo,
        ICloudinaryService cloudinaryService)
    {
        this.blogRepo = blogRepo;
        this.cloudinaryService = cloudinaryService;
    }

    public async Task CreateBlogAsync(CreateBlogViewModel input)
    {
        var entity = new BlogEntity() { Title = input.Title, Content = input.Content };
        var url = await this.cloudinaryService.UploadAsync(input.Image, Guid.NewGuid());
        entity.ImageUrl = url;
        await this.blogRepo.AddAsync(entity);
        await this.blogRepo.SaveChangesAsync();
    }

    public async Task DeleteBlogAsync(Guid Id)
    {
        var entity = await this.blogRepo
            .All()
            .FirstOrDefaultAsync(x => x.Id == Id);

        await this.cloudinaryService.DeleteAsync(entity.ImageUrl);

        this.blogRepo.Delete(entity);
        await this.blogRepo.SaveChangesAsync();
    }

    public async Task EditBlogAsync(EditBlogViewModel model)
    {
        var entity = await this.blogRepo
            .All()
            .FirstOrDefaultAsync(x => x.Id == model.Id);

        entity.Title = model.Title;
        entity.Content = model.Content;

        if (model.Image != null)
        {
            await this.cloudinaryService.DeleteAsync(entity.ImageUrl);
            var newUrl = await this.cloudinaryService.UploadAsync(model.Image, Guid.NewGuid());
            entity.ImageUrl = newUrl;
        }

        this.blogRepo.Update(entity);
        await this.blogRepo.SaveChangesAsync();
    }

    public async Task<BlogDataViewModel> GetAllAsync(int page, int itemsPerPage)
    {
        var blogs = await this.blogRepo
            .AllAsNoTracking()
            .OrderByDescending(x => x.CreatedOn)
            .Skip((page - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .To<BlogInfoViewModel>()
            .ToListAsync();

        return new BlogDataViewModel() { Blogs = blogs, Count = await this.blogRepo.AllAsNoTracking().CountAsync() };
    }

    public async Task<T> GetByIdAsync<T>(Guid blogId)
    {
        var blog = await this.blogRepo
            .AllAsNoTracking()
            .Where(x => x.Id == blogId)
            .To<T>()
            .FirstOrDefaultAsync();

        return blog;
    }
}
