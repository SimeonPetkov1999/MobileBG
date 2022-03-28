namespace MobileBG.Web.ViewModels.Cars;

public class SearchCarViewModel : PagingViewModel
{
    [Display(Name = "Make")]
    public Guid? MakeId { get; set; }

    [Display(Name = "Model")]
    public Guid? ModelId { get; set; }

    [Display(Name = "Petrol Type")]
    public Guid? PetrolTypeId { get; set; }

    [Display(Name = "City")]
    public Guid? CityId { get; set; }

    [Display(Name = "Year from")]
    public int? YearFrom { get; set; }

    [Display(Name = "Year to")]
    public int? YearTo { get; set; }

    [Display(Name = "Min Horse Power")]
    public int? MinHorsePower { get; set; }

    [Display(Name = "Min price")]
    public decimal? MinPrice { get; set; }

    [Display(Name = "Max price")]
    public decimal? MaxPrice { get; set; }

    [Display(Name = "Order by")]
    public OrderBy? Order { get; set; }

    // Data for dropdown
    public ICollection<DropdownDataViewModel> Makes { get; set; }

    public ICollection<DropdownDataViewModel> PetrolTypes { get; set; }

    public ICollection<DropdownDataViewModel> Cities { get; set; }

    // Cars
    public ICollection<CarInfoViewModel> Cars { get; set; }
}

