namespace MobileBG.Web.ViewModels.Cities;
public class CreateCityInputModel
{
    [Required(ErrorMessage = "Name is required")]
    [Display(Name = "Make name:")]
    [ValidateCityName]
    public string Name { get; set; }
}
