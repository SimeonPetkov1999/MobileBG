namespace MobileBG.Web.ViewModels.Makes;
public class MakeInfoViewModel : IMapFrom<MakeEntity>
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}
