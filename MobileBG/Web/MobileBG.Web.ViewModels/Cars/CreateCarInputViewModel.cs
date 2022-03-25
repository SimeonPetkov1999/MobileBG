namespace MobileBG.Web.ViewModels.Cars;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class CreateCarInputViewModel
{
    [Required]
    [Display(Name = "Car brand")]
    public Guid MakeId { get; set; }

    [Required]
    [Display(Name = "Car model")]
    public Guid ModelId { get; set; }

    [Required]
    [Display(Name = "Petrol type")]
    public Guid PetrolTypeId { get; set; }

    [Required]
    [Display(Name = "City")]
    public Guid CityId { get; set; }

    [Required]
    [Display(Name = "Year")]
    [Range(1950, 2022, ErrorMessage = "The Year must be between {1} and {2}!")]
    public int YearMade { get; set; }

    [Required]
    [Display(Name = "Horse Power")]
    [Range(10, 1000, ErrorMessage = "The Horse Power must be between {1} and {2}!")]
    public int HorsePower { get; set; }

    public ICollection<DropdownDataViewModel> Makes { get; set; }

    public ICollection<DropdownDataViewModel> PetrolTypes { get; set; }

    public ICollection<DropdownDataViewModel> Cities { get; set; }
}
