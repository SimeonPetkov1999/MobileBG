namespace MobileBG.Web.ViewModels.Cars;

public class CarInfoViewModel : IMapFrom<CarEntity>, IHaveCustomMappings
{
    public Guid Id { get; set; }

    public string MakeName { get; set; }

    public string ModelName { get; set; }

    public decimal Price { get; set; }

    public string ImageUrl { get; set; }

    public int HorsePower { get; set; }

    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<CarEntity, CarInfoViewModel>()
             .ForMember(m => m.ImageUrl, opt => opt.MapFrom(x => x.Images.Any() ? x.Images.FirstOrDefault().ImageUrl : null));
    }
}
