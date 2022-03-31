namespace MobileBG.Web.ViewModels.Makes;

using MobileBG.Web.ViewModels.Models;

public class MakeDetailsViewModel : IMapFrom<MakeEntity>
{
    public string Name { get; set; }

    public ICollection<ModelInfoViewModel> Models { get; set; }
}
