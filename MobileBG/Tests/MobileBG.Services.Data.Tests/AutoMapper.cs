namespace MobileBG.Services.Data.Tests;

using MobileBG.Web.ViewModels.Makes;

public static class AutoMapper
{
    public static void Configure()
    {
        AutoMapperConfig.RegisterMappings(typeof(CityInfoViewModel).GetTypeInfo().Assembly);

        AutoMapperConfig.RegisterMappings(typeof(DropdownDataViewModel).GetTypeInfo().Assembly);

        AutoMapperConfig.RegisterMappings(typeof(MakeInfoViewModel).GetTypeInfo().Assembly);

        AutoMapperConfig.RegisterMappings(typeof(CarInfoViewModel).GetTypeInfo().Assembly);
    }
}

