namespace MobileBG.Web.ViewModels.Cars;

public class SingleCarViewModel : IMapFrom<CarEntity>, IHaveCustomMappings
{
    public Guid Id { get; set; }

    public string Make { get; set; }

    public string Model { get; set; }

    public decimal Price { get; set; }

    public int HorsePower { get; set; }

    public string PetrolType { get; set; }

    public int YearMade { get; set; }

    public int Km { get; set; }

    public string Description { get; set; }

    public string CityName { get; set; }

    public ICollection<string> Images { get; set; }

    public string UserId { get; set; }

    public string UserName { get; set; }

    public string PhoneNumber { get; set; }

    public int UsersCars { get; set; }

    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<CarEntity, SingleCarViewModel>()
            .ForMember(m => m.Images, opt => opt.MapFrom(x => x.Images.Select(x => x.ImageUrl)))
            .ForMember(m => m.Make, opt => opt.MapFrom(x => x.Make.Name))
            .ForMember(m => m.Model, opt => opt.MapFrom(x => x.Model.Name))
            .ForMember(m => m.PetrolType, opt => opt.MapFrom(x => x.PetrolType.Name))
            .ForMember(m => m.YearMade, opt => opt.MapFrom(x => x.YearMade))
            .ForMember(m => m.UserName, opt => opt.MapFrom(x => x.User.Email))
            .ForMember(m => m.PhoneNumber, opt => opt.MapFrom(x => x.User.PhoneNumber));
    }
}
