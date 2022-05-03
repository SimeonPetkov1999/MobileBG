namespace MobileBG.Web.ViewModels.Cars;

using MobileBG.Common;

public class CreateCarInputViewModel
{
    [Required(ErrorMessage = GlobalConstants.ErrorMessages.RequiredErrorMessage)]
    [Display(Name = "Car brand")]
    public Guid MakeId { get; set; }

    [Required(ErrorMessage = GlobalConstants.ErrorMessages.RequiredErrorMessage)]
    [Display(Name = "Car model")]
    public Guid ModelId { get; set; }

    [Required(ErrorMessage = GlobalConstants.ErrorMessages.RequiredErrorMessage)]
    [Display(Name = "Petrol type")]
    public Guid PetrolTypeId { get; set; }

    [Required(ErrorMessage = GlobalConstants.ErrorMessages.RequiredErrorMessage)]
    [Display(Name = "City")]
    public Guid CityId { get; set; }

    [Required(ErrorMessage = GlobalConstants.ErrorMessages.RequiredErrorMessage)]
    [Display(Name = "Year")]
    [Range(1950, 2022, ErrorMessage = GlobalConstants.ErrorMessages.InvalidValueErrorMessage)]
    public int YearMade { get; set; }

    [Display(Name = "Km")]
    [Required(ErrorMessage = GlobalConstants.ErrorMessages.RequiredErrorMessage)]
    [Range(1, 1_000_000, ErrorMessage = GlobalConstants.ErrorMessages.InvalidValueErrorMessage)]
    public int Km { get; set; }

    [Required(ErrorMessage = GlobalConstants.ErrorMessages.RequiredErrorMessage)]
    [Display(Name = "Horse Power")]
    [Range(10, 1000, ErrorMessage = GlobalConstants.ErrorMessages.InvalidValueErrorMessage)]
    public int HorsePower { get; set; }

    [Required(ErrorMessage = GlobalConstants.ErrorMessages.RequiredErrorMessage)]
    [Display(Name = "Price")]
    [Range(100d, 1_000_000d, ErrorMessage = GlobalConstants.ErrorMessages.InvalidValueErrorMessage)]
    public decimal Price { get; set; }

    [StringLength(500, ErrorMessage = GlobalConstants.ErrorMessages.InvalidValueErrorMessage)]
    public string Description { get; set; }

    [Display(Name = "Choose maximum 5 images (.jpg, .png)")]
    [MaxLength(5, ErrorMessage = "Maximum 5 images!")]
    [AllowedExtensions(".jpg", ".png", ".jpeg")]
    [MaxFileSize(5 * 1000 * 1000)] // 5mb
    [Required(ErrorMessage = GlobalConstants.ErrorMessages.ImageErrorMessage)]
    public ICollection<IFormFile> Images { get; set; }

    public ICollection<DropdownDataViewModel> Makes { get; set; }

    public ICollection<DropdownDataViewModel> PetrolTypes { get; set; }

    public ICollection<DropdownDataViewModel> Cities { get; set; }
}
