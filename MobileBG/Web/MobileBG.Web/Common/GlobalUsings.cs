#pragma warning disable SA1200

global using System;
global using System.Collections.Generic;
global using System.Threading.Tasks;

global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using MobileBG.Data;
global using MobileBG.Data.Common;
global using MobileBG.Data.Common.Repositories;
global using MobileBG.Data.Models;
global using MobileBG.Data.Repositories;
global using MobileBG.Data.Seeding;
global using MobileBG.Services;
global using MobileBG.Services.Contracts;
global using MobileBG.Services.Data;
global using MobileBG.Services.Data.Contracts;
global using MobileBG.Services.Mapping;
global using MobileBG.Services.Messaging;
global using MobileBG.Web.Infrastructure;
global using MobileBG.Web.ViewModels;
global using MobileBG.Web.ViewModels.Cars;

#pragma warning restore SA1200