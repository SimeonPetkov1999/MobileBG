namespace MobileBG.Services.Data;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MobileBG.Data.Common.Repositories;
using MobileBG.Data.Models;
using MobileBG.Services.Data.Contracts;
using MobileBG.Web.ViewModels.Cars;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MobileBG.Services.Mapping;

public class CarService : ICarService
{
    private readonly IRepository<MakeEntity> makeRepo;

    public CarService(
        IRepository<MakeEntity> makeRepo)
    {
        this.makeRepo = makeRepo;
    }

    public async Task<ICollection<MakeViewModel>> GetAllMakesAsync()
    {
        var makes = await this.makeRepo
            .AllAsNoTracking()
            .To<MakeViewModel>()
            .ToListAsync();

        return makes;
    }
}