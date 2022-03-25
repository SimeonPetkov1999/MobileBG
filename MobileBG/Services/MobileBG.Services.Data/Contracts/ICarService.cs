namespace MobileBG.Services.Data.Contracts;

using MobileBG.Web.ViewModels.Cars;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICarService
{
    public Task<ICollection<MakeViewModel>> GetAllMakesAsync();
}
