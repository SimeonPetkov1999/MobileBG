namespace MobileBG.Web.ViewModels.Makes;

public class MakeInputModel
{
    [Required(ErrorMessage = "Name is required")]
    [Display(Name="Make name:")]
    [ValidateMakeName]
    public string Name { get; set; }
}
