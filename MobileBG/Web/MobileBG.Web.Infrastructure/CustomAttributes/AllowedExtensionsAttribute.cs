namespace MobileBG.Web.Infrastructure.CustomAttributes;

public class AllowedExtensionsAttribute : ValidationAttribute
{
    private readonly string[] extensions;

    public AllowedExtensionsAttribute(params string[] extensions)
    {
        this.extensions = extensions;
    }

    protected override ValidationResult IsValid(
        object value,
        ValidationContext validationContext)
    {
        var files = value as List<IFormFile>;

        if (files != null)
        {
            foreach (var file in files)
            {
                var extension = Path.GetExtension(file.FileName);

                if (!this.extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult($"({extension}) files are not allowed!");
                }
            }
        }

        var singleFile = value as IFormFile;

        if (singleFile != null)
        {
            var extension = Path.GetExtension(singleFile.FileName);
            if (!this.extensions.Contains(extension.ToLower()))
            {
                return new ValidationResult($"({extension}) files are not allowed!");
            }
        }

        return ValidationResult.Success;
    }
}
