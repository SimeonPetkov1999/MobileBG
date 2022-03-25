namespace MobileBG.Web.ViewModels.Cars;

using AutoMapper;
using MobileBG.Data.Models;
using MobileBG.Services.Mapping;
using System;
public class DropdownDataViewModel : IMapFrom<MakeEntity>,  IMapFrom<ModelEntity>, IMapFrom<PetrolTypeEntity> ,IMapFrom<CityEntity>
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}
