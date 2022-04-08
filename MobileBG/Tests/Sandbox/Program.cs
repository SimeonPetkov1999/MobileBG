namespace Sandbox;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

using MobileBG.Data;
using MobileBG.Data.Common;
using MobileBG.Data.Common.Repositories;
using MobileBG.Data.Models;
using MobileBG.Data.Repositories;
using MobileBG.Data.Seeding;
using MobileBG.Services.Data;
using MobileBG.Services.Messaging;

using CommandLine;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using Newtonsoft.Json;

public static class Program
{
    public static int Main(string[] args)
    {
        Console.WriteLine($"{typeof(Program).Namespace} ({string.Join(" ", args)}) starts working...");
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider(true);

        // Seed data on application startup
        using (var serviceScope = serviceProvider.CreateScope())
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
            new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
        }

        using (var serviceScope = serviceProvider.CreateScope())
        {
            serviceProvider = serviceScope.ServiceProvider;

            return Parser.Default.ParseArguments<SandboxOptions>(args).MapResult(
                opts => SandboxCode(opts, serviceProvider).GetAwaiter().GetResult(),
                _ => 255);
        }
    }

    private static async Task<int> SandboxCode(SandboxOptions options, IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetService<ApplicationDbContext>();

        var cars = await context
            .Cars
            .Include(x => x.Make)
            .Include(x => x.Model)
            .Include(x => x.PetrolType)
            .Include(x => x.Images)
            .Include(x => x.City)
            .ToListAsync();

        var obj = cars.Select(x => new
        {
            Make = x.Make.Name,
            Model = x.Model.Name,
            PetrolType = x.PetrolType.Name,
            City = x.City.Name,
            Price = x.Price,
            YearMade = x.YearMade,
            Km = x.Km,
            HorsePower = x.HorsePower,
            Description = x.Description,
            ImageLinks = x.Images.Select(x => x.ImageUrl),
        });

        var json = JsonConvert.SerializeObject(obj, Formatting.Indented);

        File.WriteAllText(@"C:\Simeon\Repos\MobileBG\MobileBG\Tests\Sandbox\Cars.json", json);

        return await Task.FromResult(0);
    }

    private static void ConfigureServices(ServiceCollection services)
    {
        var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .AddEnvironmentVariables()
            .Build();

        services.AddSingleton<IConfiguration>(configuration);

        services.AddDbContext<ApplicationDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .UseLoggerFactory(new LoggerFactory()));

        services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
            .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped<IDbQueryRunner, DbQueryRunner>();

        // Application services
        services.AddTransient<IEmailSender, NullMessageSender>();
    }
}
