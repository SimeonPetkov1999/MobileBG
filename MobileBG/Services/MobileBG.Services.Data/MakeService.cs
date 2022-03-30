namespace MobileBG.Services.Data;

using MobileBG.Web.ViewModels.Makes;

public class MakeService : IMakeService
{
    private readonly IRepository<MakeEntity> makeRepo;

    public MakeService(IRepository<MakeEntity> makeRepo)
    {
        this.makeRepo = makeRepo;
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
            .ToListAsync();

        return makes;
    }
}
