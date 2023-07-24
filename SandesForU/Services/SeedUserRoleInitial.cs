using Microsoft.AspNetCore.Identity;

namespace SandesForU.Services
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public SeedUserRoleInitial(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public void SeedRoes()
        {
            if (!roleManager.RoleExistsAsync("Member").Result)
            {
                var role = new IdentityRole();
                role.Name = "Member";
                role.NormalizedName = "MEMBER";
                IdentityResult result = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                IdentityResult result = roleManager.CreateAsync(role).Result;
            }
        }

        public void SeedUsers()
        {
            if (userManager.FindByEmailAsync("test@test.com").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "test@test";
                user.Email = "test@test.com";
                user.NormalizedUserName = "TEST@TEST.COM";
                user.NormalizedEmail = "TEST@TEST.COM";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = userManager.CreateAsync(user, "Testou@2023").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Member").Wait();
                }
            }

            if (userManager.FindByEmailAsync("admin@localhost.com").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "admin@localhost";
                user.Email = "admin@localhost.com";
                user.NormalizedUserName = "ADMIN@LOCALHOSTC";
                user.NormalizedEmail = "ADMIN@LOCALHOSTV";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = userManager.CreateAsync(user, "Testou@2023").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}

