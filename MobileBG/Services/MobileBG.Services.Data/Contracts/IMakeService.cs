namespace MobileBG.Services.Data.Contracts;

using MobileBG.Web.ViewModels.Makes;
public interface IMakeService
{
    public Task<ICollection<MakeInfoViewModel>> GetAllMakesAsync(string keyWord);
}
