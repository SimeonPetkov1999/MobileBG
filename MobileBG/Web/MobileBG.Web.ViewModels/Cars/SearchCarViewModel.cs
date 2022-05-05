namespace MobileBG.Web.ViewModels.Cars;

public class SearchCarViewModel : PagingViewModel
{
    [Display(Name = "Модел")]
    public Guid? MakeId { get; set; }

    [Display(Name = "Марка")]
    public Guid? ModelId { get; set; }

    [Display(Name = "Гориво")]
    public Guid? PetrolTypeId { get; set; }

    [Display(Name = "Град")]
    public Guid? CityId { get; set; }

    [Display(Name = "Година от")]
    public int? YearFrom { get; set; }

    [Display(Name = "Година до")]
    public int? YearTo { get; set; }

    [Display(Name = "Конски сили (MIN)")]
    public int? MinHorsePower { get; set; }

    [Display(Name = "Конски сили (MAX)")]
    public decimal? MinPrice { get; set; }

    [Display(Name = "Максимална цена")]
    public decimal? MaxPrice { get; set; }

    [Display(Name = "Подреди по")]
    public OrderBy? Order { get; set; }

    // Data for dropdown
    public ICollection<DropdownDataViewModel> Makes { get; set; }

    public ICollection<DropdownDataViewModel> PetrolTypes { get; set; }

    public ICollection<DropdownDataViewModel> Cities { get; set; }

    // Cars
    public ICollection<CarInfoViewModel> Cars { get; set; }
}

