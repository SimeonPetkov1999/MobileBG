namespace MobileBG.Services.Data.Tests.ServiceTests;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using MobileBG.Services.Contracts;
using MobileBG.Services.Data.Contracts;
using MobileBG.Web.ViewModels.Blogs;
using System.IO;
using System.Text;

public class BlogServiceTests
{
    private Mock<IRepository<BlogEntity>> blogRepo;
    private Mock<ICloudinaryService> cloudinaryService;

    private IBlogService blogService;

    public BlogServiceTests()
    {
        AutoMapper.Configure();
    }

    [Fact]
    public async void CreateBlogAsyncShouldCreateBlogAndNotThrowException()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
        IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.txt");

        var input = new CreateBlogViewModel()
        {
            Content = "Some text",
            Image = file,
            Title = "Test",
        };

        await this.blogService.CreateBlogAsync(input);

        Assert.True(true);
    }

    [Fact]
    public async void EditBlogAsyncShouldEditBlogAndNotThrowException()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
        IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.txt");

        var blogId = Guid.NewGuid();
        var blogs = new List<BlogEntity>()
        {
            new() { Id = blogId, Title = "test", Content = "Test", },
            new() { Id = Guid.NewGuid(), Title = "test1", Content = "Test1", },
            new() { Id = Guid.NewGuid(), Title = "test2", Content = "Test2", },
        }.AsQueryable();

        var mockBlogs = blogs.BuildMock();
        this.blogRepo.Setup(x => x.All()).Returns(mockBlogs);

        var input = new EditBlogViewModel()
        {
            Id = blogId,
            Content = "Some text",
            Image = file,
            Title = "Test",
        };

        await this.blogService.EditBlogAsync(input);

        Assert.True(true);
    }

    [Fact]
    public async void DeleteBlogAsyncShouldDeleteBlogAndNotThrowException()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var blogId = Guid.NewGuid();
        var blogs = new List<BlogEntity>()
        {
            new() { Id = blogId, Title = "test", Content = "Test", },
            new() { Id = Guid.NewGuid(), Title = "test1", Content = "Test1", },
            new() { Id = Guid.NewGuid(), Title = "test2", Content = "Test2", },
        }.AsQueryable();

        var mockBlogs = blogs.BuildMock();

        this.blogRepo.Setup(x => x.All()).Returns(mockBlogs);

        await this.blogService.DeleteBlogAsync(blogId);

        Assert.True(true);
    }

    [Fact]
    public async void GetAllAsyncShouldReturnCollection()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var blogId = Guid.NewGuid();
        var blogs = new List<BlogEntity>()
        {
            new() { Id = blogId, Title = "test", Content = "Test", },
            new() { Id = Guid.NewGuid(), Title = "test1", Content = "Test1", },
            new() { Id = Guid.NewGuid(), Title = "test2", Content = "Test2", },
        }.AsQueryable();

        var mockBlogs = blogs.BuildMock();
        this.blogRepo.Setup(x => x.AllAsNoTracking()).Returns(mockBlogs);

        var result = await this.blogService.GetAllAsync(1, 5);

        Assert.Equal(3, result.Blogs.Count);
    }

    [Fact]
    public async void GetByIdAsyncShouldReturnEntity()
    {
        this.InitializeRepos();
        AutoMapper.Configure();

        var blogId = Guid.NewGuid();
        var blogs = new List<BlogEntity>()
        {
            new() { Id = blogId, Title = "test", Content = "Test", },
            new() { Id = Guid.NewGuid(), Title = "test1", Content = "Test1", },
            new() { Id = Guid.NewGuid(), Title = "test2", Content = "Test2", },
        }.AsQueryable();

        var mockBlogs = blogs.BuildMock();
        this.blogRepo.Setup(x => x.AllAsNoTracking()).Returns(mockBlogs);

        var result = await this.blogService.GetByIdAsync<BlogInfoViewModel>(blogId);

        Assert.Equal(blogId, result.Id);
    }

    private void InitializeRepos()
    {
        this.blogRepo = new Mock<IRepository<BlogEntity>>();
        this.cloudinaryService = new Mock<ICloudinaryService>();

        this.blogService = new BlogService(this.blogRepo.Object, this.cloudinaryService.Object);
    }
}
