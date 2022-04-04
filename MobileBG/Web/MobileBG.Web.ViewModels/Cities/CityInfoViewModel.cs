namespace MobileBG.Web.ViewModels.Cities;

public class CityInfoViewModel : IMapFrom<CityEntity>
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}

