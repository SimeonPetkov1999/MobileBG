namespace MobileBG.Web.ViewModels.Cities;
public class AllCitiesViewModel
{
    public ICollection<CityInfoViewModel> Cities { get; set; }

    public string KeyWord { get; set; }
}
