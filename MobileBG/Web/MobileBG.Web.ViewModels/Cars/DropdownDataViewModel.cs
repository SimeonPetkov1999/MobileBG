namespace MobileBG.Web.ViewModels.Cars;

public class DropdownDataViewModel : IMapFrom<MakeEntity>,  IMapFrom<ModelEntity>, IMapFrom<PetrolTypeEntity>, IMapFrom<CityEntity>
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}
