namespace MobileBG.Data.Models;

public class BlogEntity : BaseModel<Guid>
{
    public string Title { get; set; }

    public string Content { get; set; }

    public string ImageUrl { get; set; }
}
