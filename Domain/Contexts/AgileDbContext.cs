using AgileTask.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AgileTask.Domain.Contexts
{
    public class AgileDbContext : IdentityDbContext
    {
        public AgileDbContext(DbContextOptions<AgileDbContext> options)
           : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<VacationDay> VacationDays { get; set; }
        public DbSet<VacationApplication> VacationApplications { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            string ADMIN_ID = "02174cf0–9412–4cfe–afbf–59f706d72cf6";
            string ROLE_ID = "341743f0–asd2–42de–afbf–59kmkkmk72cf6";

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
                Id = ROLE_ID,
                ConcurrencyStamp = ROLE_ID
            },
            new IdentityRole { Name = "HR", NormalizedName = "HR" },
            new IdentityRole { Name="User", NormalizedName="USER"}
            );

            var appUser = new ApplicationUser
            {
                Id = ADMIN_ID,
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                Name="Admin"
            };

            PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
            appUser.PasswordHash = ph.HashPassword(appUser, "Admin123");

            builder.Entity<ApplicationUser>().HasData(appUser);

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });

        }



    }
}
