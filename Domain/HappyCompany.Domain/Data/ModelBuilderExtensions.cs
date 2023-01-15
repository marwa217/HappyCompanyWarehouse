using HappyCompany.Infra.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HappyCompany.Domain.Data
{
    public static class ModelBuilderExtensions
    {
        private const string UserId = "b74ddd14-6340-4840-95c2-db12554843e5";
        private const string RoleId = "341743f0-asd2–42de-afbf-59kmkkmk72cf6";

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().Property(b => b.Active).HasDefaultValue(false);
            modelBuilder.Entity<Item>().HasIndex(i => i.Name).IsUnique();
            modelBuilder.Entity<Warehouse>().HasIndex(w => w.Name).IsUnique();

            SeedUser(modelBuilder);
            SeedRoles(modelBuilder);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() 
                { RoleId = RoleId, UserId = UserId }
                );
        }

        private static void SeedUser(ModelBuilder modelBuilder)
        {
            User user = new()
            {
                Id= UserId,
                FullName = "Administrator",
                Email = "admin@happywarehouse.com",
                NormalizedEmail = "admin@happywarehouse.com",
                UserName = "admin@happywarehouse.com",
                Active = true
            };
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
           user.PasswordHash = passwordHasher.HashPassword(user, "P@ssw0rd");

            modelBuilder.Entity<User>().HasData(user);
        }

        private static void SeedRoles(ModelBuilder modelBuilder)
        {
            int i = 1;
            foreach(var (key, value) in Roles.UserRoles)
            {
                modelBuilder.Entity<IdentityRole>().HasData(
                    new IdentityRole() { 
                        Id = key, Name = value, ConcurrencyStamp = i.ToString(), NormalizedName = value }
                 );
                i++;
            }
        }
    }
}
