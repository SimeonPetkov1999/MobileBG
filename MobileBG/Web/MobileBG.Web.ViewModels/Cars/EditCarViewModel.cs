namespace MobileBG.Web.ViewModels.Cars;
public class EditCarViewModel : IMapFrom<CarEntity>, IHaveCustomMappings
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Car brand is required")]
    [Display(Name = "Car brand")]
    public Guid MakeId { get; set; }

    [Required(ErrorMessage = "Car model is required")]
    [Display(Name = "Car model")]
    public Guid ModelId { get; set; }

    public string ModelName { get; set; } //Initial model name

    [Required(ErrorMessage = "Petrol type is required")]
    [Display(Name = "Petrol type")]
    public Guid PetrolTypeId { get; set; }

    [Required(ErrorMessage = "City is required")]
    [Display(Name = "City")]
    public Guid CityId { get; set; }

    [Required(ErrorMessage = "Year is required")]
    [Display(Name = "Year")]
    [Range(1950, 2022, ErrorMessage = "The Year must be between {1} and {2}!")]
    public int YearMade { get; set; }

    [Required(ErrorMessage = "Km is required")]
    [Display(Name = "Km")]
    [Range(1, 1_000_000, ErrorMessage = "The Km must be between {1} and {2}!")]
    public int Km { get; set; }

    [Required(ErrorMessage = "Horse Power is required")]
    [Display(Name = "Horse Power")]
    [Range(10, 1000, ErrorMessage = "The Horse Power must be between {1} and {2}!")]
    public int HorsePower { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Display(Name = "Price (in BGN)")]
    [Range(100d, 1_000_000d, ErrorMessage = "Price must be between {1} and {2}!")]
    public decimal Price { get; set; }

    [StringLength(500, ErrorMessage = "Description should be no longer than 500 characters")]
    public string Description { get; set; }

    public ICollection<string> ImageUrls { get; set; }

    [Display(Name = "Choose maximum 5 images (.jpg, .png)")]
    [AllowedExtensions(".jpg", ".png")]
    [MaxFileSize(3 * 1000 * 1000)] // 3mb
    public ICollection<IFormFile> Images { get; set; }

    public ICollection<DropdownDataViewModel> Makes { get; set; }

    public ICollection<DropdownDataViewModel> PetrolTypes { get; set; }

    public ICollection<DropdownDataViewModel> Cities { get; set; }

    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<CarEntity, EditCarViewModel>()
              .ForMember(m => m.ImageUrls,  opt => opt.MapFrom(x => x.Images.Select(x => x.ImageUrl)))
              .ForMember(m => m.Images,  opt => opt.Ignore());
    }
}
