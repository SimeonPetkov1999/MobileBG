namespace MobileBG.Web.ViewModels.Models;

public class ModelInfoViewModel : IMapFrom<ModelEntity>
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}

