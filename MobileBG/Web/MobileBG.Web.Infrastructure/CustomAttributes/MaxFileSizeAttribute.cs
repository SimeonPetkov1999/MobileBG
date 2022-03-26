namespace MobileBG.Web.Infrastructure.CustomAttributes;

public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly int maxFileSize;

    public MaxFileSizeAttribute(int maxFileSize)
    {
        this.maxFileSize = maxFileSize;
    }

    protected override ValidationResult IsValid(
    object value, ValidationContext validationContext)
    {
        var files = value as List<IFormFile>;
        if (files != null)
        {
            foreach (var file in files)
            {
                if (file.Length > this.maxFileSize)
                {
                    return new ValidationResult($"{file.FileName} is too big. Max file size is 3MB");
                }
            }
        }

        return ValidationResult.Success;
    }
}
