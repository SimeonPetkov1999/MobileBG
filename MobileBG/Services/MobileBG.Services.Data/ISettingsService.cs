namespace MobileBG.Services.Data;

public interface ISettingsService
{
    int GetCount();

    IEnumerable<T> GetAll<T>();
}
