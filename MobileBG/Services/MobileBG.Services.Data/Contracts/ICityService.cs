namespace MobileBG.Services.Data.Contracts;

using MobileBG.Web.ViewModels.Cities;

public interface ICityService
{
    public Task<ICollection<CityInfoViewModel>> GetAllCitiesAsync(string keyWord);

    public Task<Guid> CreateCityAsync(string cityName);

    public Task<bool> DeleteCityAsync(Guid cityId);
}

