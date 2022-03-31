namespace MobileBG.Web.Infrastructure.CustomAttributes;

using MobileBG.Data;

public class ValidateMakeNameAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(
        object value,
        ValidationContext validationContext)
    {
        var makeName = value as string;

        var dbContext = (ApplicationDbContext)validationContext
                         .GetService(typeof(ApplicationDbContext));

        if (makeName != null)
        {
            if (dbContext.Makes.Any(x => x.Name.ToLower() == makeName.ToLower()))
            {
                return new ValidationResult($"Make with name {makeName} already exists");
            }
        }

        return ValidationResult.Success;
    }
}

