namespace MobileBG.Services.Data;

using Microsoft.EntityFrameworkCore;
using MobileBG.Data.Common.Repositories;
using MobileBG.Data.Models;
using MobileBG.Services.Data.Contracts;
using MobileBG.Services.Mapping;
using MobileBG.Web.ViewModels.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            .ToListAsync();

        return models;
    }
}
