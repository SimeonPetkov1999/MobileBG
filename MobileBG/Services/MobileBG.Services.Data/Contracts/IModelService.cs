namespace MobileBG.Services.Data.Contracts;

public interface IModelService
{
    public Task<ICollection<DropdownDataViewModel>> GetModelsForMakeAsync(Guid makeId);

    public Task<(bool IsDeleted, Guid MakeId)> DeleteModelAsync(Guid makeId);
}
