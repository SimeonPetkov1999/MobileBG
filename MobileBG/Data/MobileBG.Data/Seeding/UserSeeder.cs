namespace MobileBG.Data.Seeding;

public class UserSeeder : ISeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

        var role = await roleManager.FindByNameAsync(GlobalConstants.AdministratorRoleName);
        if (userManager.Users.Include(x => x.Roles).Any(x => x.Roles.Any(r => r.RoleId == role.Id)) == false)
        {
            var result = await userManager.CreateAsync(new ApplicationUser() {UserName = "Admin@admin.com", Email = "Admin@admin.com", Id = Guid.NewGuid().ToString() }, "123456");
            dbContext.SaveChanges();

            var user = dbContext.Users.FirstOrDefault(x => x.Email == "Admin@admin.com");
            await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
        }
    }
}
