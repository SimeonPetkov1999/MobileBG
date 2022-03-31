namespace MobileBG.Services.Data;

public class ModelService : IModelService
{
    private readonly IRepository<ModelEntity> modelRepo;

    public ModelService(IRepository<ModelEntity> modelRepo)
    {
        this.modelRepo = modelRepo;
    }

    public async Task<ICollection<DropdownDataViewModel>> GetModelsForMakeAsync(Guid makeId)
    {
        var models = await this.modelRepo
            .AllAsNoTracking()
            .Where(x => x.MakeId == makeId)
            .To<DropdownDataViewModel>()
            .OrderBy(x => x.Name)
            .ToListAsync();

        return models;
    }
}
