namespace MobileBG.Services.Data.Contracts;

using MobileBG.Web.ViewModels.Makes;
public interface IMakeService
{
    public Task<ICollection<MakeInfoViewModel>> GetAllMakesAsync(string keyWord);

    public Task<Guid> CreateMakeAsync(string makeName);

    public Task<MakeDetailsViewModel> GetMakeDetialsAsync(Guid makeId);

    public Task<bool> CreateModelForMakeAsync(Guid makeId, string modelName);

    public Task EditMakeAsync(EditMakeViewModel model);

    public Task<bool> DeleteMakeAsync(Guid Id);
}
