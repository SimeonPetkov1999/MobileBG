namespace MobileBG.Services.Data.Contracts;

using MobileBG.Web.ViewModels.Cars;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IModelService
{
    public Task<ICollection<DropdownDataViewModel>> GetModelsForMakeAsync(Guid makeId);
}
