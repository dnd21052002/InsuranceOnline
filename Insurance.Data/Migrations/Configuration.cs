namespace Insurance.Data.Migrations
{
    using Insurance.Data.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Insurance.Data.InsuranceDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Insurance.Data.InsuranceDbContext context)
        {
            CreateAdminUser(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            
        }

        private void CreateAdminUser(InsuranceDbContext context)
        {
            if(!context.AdminUsers.Any())
            {
                context.AdminUsers.Add(new AdminUser
                {
                    Username = "admin",
                    Password = "0e7517141fb53f21ee439b355b5a1d0a",
                    Status = 1
                });
                context.SaveChanges();
            }
        }
    }
}
