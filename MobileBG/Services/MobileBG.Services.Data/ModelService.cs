namespace MobileBG.Services.Data;

public class ModelService : IModelService
{
    private readonly IRepository<ModelEntity> modelRepo;

    public ModelService(IRepository<ModelEntity> modelRepo)
    {
        this.modelRepo = modelRepo;
    }

    public async Task<(bool IsDeleted, Guid MakeId)> DeleteModelAsync(Guid makeId)
    {
        var model = await this.modelRepo
            .All()
            .Include(x => x.Cars)
            .Where(x => x.Id == makeId)
            .FirstOrDefaultAsync();

        if (model != null && model.Cars.Any())
        {
            return (false, model.MakeId);
        }

        this.modelRepo.Delete(model);
        await this.modelRepo.SaveChangesAsync();
        return (true, model.MakeId);
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
