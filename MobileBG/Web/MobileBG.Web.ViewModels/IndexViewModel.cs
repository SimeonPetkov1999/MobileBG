namespace MobileBG.Web.ViewModels;

using MobileBG.Web.ViewModels.Cars;

public class IndexViewModel
{
    public int MakesCount { get; set; }

    public int CarsCount { get; set; }

    public int UsersCount { get; set; }

    public ICollection<CarInfoViewModel> Cars { get; set; }
}
