namespace MobileBG.Web.ViewModels.Makes;

public class EditMakeViewModel
{
    public Guid Id { get; set; }

    [Required]
    [ValidateMakeName]
    public string Name { get; set; }
}
