namespace MobileBG.Web.Infrastructure.CustomAttributes;

using MobileBG.Data;

public class ValidateCityName : ValidationAttribute
{
    protected override ValidationResult IsValid(
        object value,
        ValidationContext validationContext)
    {
        var cityName = value as string;

        var dbContext = (ApplicationDbContext)validationContext
                         .GetService(typeof(ApplicationDbContext));

        if (cityName != null)
        {
            if (dbContext.Cities.Any(x => x.Name.ToLower() == cityName.ToLower()))
            {
                return new ValidationResult($"City with name {cityName} already exists");
            }
        }

        return ValidationResult.Success;
    }
}