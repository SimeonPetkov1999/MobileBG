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
using System;

public class CarService : ICarService
{
    private readonly IRepository<MakeEntity> makeRepo;
    private readonly IRepository<PetrolTypeEntity> petrolTypeRepo;
    private readonly IRepository<CityEntity> cityTypeRepo;

    public CarService(
        IRepository<MakeEntity> makeRepo,
        IRepository<PetrolTypeEntity> petrolTypeRepo,
        IRepository<CityEntity> cityTypeRepo)
    {
        this.makeRepo = makeRepo;
        this.petrolTypeRepo = petrolTypeRepo;
        this.cityTypeRepo = cityTypeRepo;
    }

    public async Task<ICollection<DropdownDataViewModel>> GetAllCitiesAsync()
    {
        var cities = await this.cityTypeRepo
            .AllAsNoTracking()
            .To<DropdownDataViewModel>()
            .ToListAsync();

        return cities;
    }

    public async Task<ICollection<DropdownDataViewModel>> GetAllMakesAsync()
    {
        var makes = await this.makeRepo
            .AllAsNoTracking()
            .To<DropdownDataViewModel>()
            .ToListAsync();

        return makes;
    }

    public async Task<ICollection<DropdownDataViewModel>> GetAllPetrolTypesAsync()
    {
        var types = await this.petrolTypeRepo
            .AllAsNoTracking()
            .To<DropdownDataViewModel>()
            .ToListAsync();

        return types;
    }
}
