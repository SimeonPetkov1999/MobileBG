namespace MobileBG.Web.Infrastructure.CustomAttributes;

using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

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

        foreach (var file in files)
        {
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);

                if (!this.extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult($"({extension}) files are not allowed!");
                }
            }
        }

        return ValidationResult.Success;
    }
}
