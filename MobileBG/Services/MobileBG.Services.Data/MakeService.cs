namespace MobileBG.Services.Data;

using MobileBG.Web.ViewModels.Makes;
using System;

public class MakeService : IMakeService
{
    private readonly IRepository<MakeEntity> makeRepo;

    public MakeService(IRepository<MakeEntity> makeRepo)
    {
        this.makeRepo = makeRepo;
    }

    public async Task<Guid> CreateMakeAsync(string makeName)
    {
        var entity = new MakeEntity() { Name = makeName, Id = Guid.NewGuid() };
        await this.makeRepo.AddAsync(entity);
        await this.makeRepo.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<bool> CreateModelForMakeAsync(Guid makeId, string modelName)
    {
        var make = await this.makeRepo
            .All()
            .Include(x => x.Models)
            .Where(x => x.Id == makeId)
            .FirstOrDefaultAsync();

        if (make != null && !make.Models.Any(x => x.Name.ToLower() == modelName.ToLower()))
        {
            make.Models.Add(new ModelEntity() { Name = modelName });
            await this.makeRepo.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<bool> DeleteMakeAsync(Guid Id)
    {
        var make = await this.makeRepo
            .All()
            .Include(x => x.Models)
            .Where(x => x.Id == Id)
            .FirstOrDefaultAsync();

        if (make != null && make.Models.Any())
        {
            return false;
        }

        this.makeRepo.Delete(make);
        await this.makeRepo.SaveChangesAsync();
        return true;
    }

    public async Task EditMakeAsync(EditMakeViewModel model)
    {
        var entity = await this.makeRepo
            .All()
            .Where(x => x.Id == model.Id)
            .FirstOrDefaultAsync();

        entity.Name = model.Name;
        this.makeRepo.Update(entity);
        await this.makeRepo.SaveChangesAsync();
    }

    public async Task<ICollection<MakeInfoViewModel>> GetAllMakesAsync(string keyWord)
    {
        var query = this.makeRepo
            .AllAsNoTracking();

        if (keyWord != null)
        {
            query = query.Where(x => x.Name.ToLower().Contains(keyWord.ToLower()));
        }

        var makes = await query.To<MakeInfoViewModel>()
            .OrderBy(x => x.Name)
            .ToListAsync();

        return makes;
    }

    public async Task<MakeDetailsViewModel> GetMakeDetialsAsync(Guid makeId)
    {
        var make = await this.makeRepo
             .AllAsNoTracking()
             .Include(x => x.Models)
             .Where(x => x.Id == makeId)
             .To<MakeDetailsViewModel>()
             .FirstOrDefaultAsync();

        return make;
    }
}
