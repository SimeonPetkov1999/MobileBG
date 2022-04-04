namespace MobileBG.Services.Data.Tests;

public static class AutoMapper
{
    public static void Configure()
    {
        AutoMapperConfig.RegisterMappings(typeof(CityInfoViewModel).GetTypeInfo().Assembly);

        AutoMapperConfig.RegisterMappings(typeof(DropdownDataViewModel).GetTypeInfo().Assembly);
    }
}

