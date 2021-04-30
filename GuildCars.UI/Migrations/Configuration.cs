namespace GuildCars.UI.Migrations
{
    using GuildCars.UI.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GuildCars.UI.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GuildCars.UI.Models.ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "Admin"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "Admin", FirstName = "Charles", LastName = "Carper", Email = "Charles.Carper@GuildCars.com", UserRole = "Admin" };

                manager.Create(user, "testing123!");
                manager.AddToRole(user.Id, "Admin");
            }

            if (!context.Roles.Any(r => r.Name == "Sales"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Sales" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "Sales"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "Sales", FirstName = "Barb", LastName = "Wire", Email = "barb@test.test", UserRole = "Sales" };

                manager.Create(user, "testing123!");
                manager.AddToRole(user.Id, "Sales");
            }
        }
    }
}
