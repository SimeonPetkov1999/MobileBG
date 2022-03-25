namespace MobileBG.Web.ViewModels.Cars;

using AutoMapper;
using MobileBG.Data.Models;
using MobileBG.Services.Mapping;
using System;
public class MakeViewModel : IMapFrom<MakeEntity>, IHaveCustomMappings
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<MakeEntity, MakeViewModel>().ForMember(
                m => m.Name,
                opt => opt.MapFrom(x => x.Name));
    }
}
