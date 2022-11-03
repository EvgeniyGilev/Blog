using BlogAPI.DATA.Models;
using Microsoft.AspNetCore.Identity;

namespace BlogAPI
{
    public class Initializer
    {
        public static async Task CheckAdminUser(
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Администратор"))
            {
                var role = new Role { Name = "Администратор", Description = "Полные права" };
                await roleManager.CreateAsync(role);
            }

            if (!await roleManager.RoleExistsAsync("Пользователь"))
            {
                var role = new Role { Name = "Пользователь", Description = "Простые права" };
                await roleManager.CreateAsync(role);
            }

            var admin = await userManager.FindByNameAsync("admin@admin.ru");
            if (admin == null)
            {
                var adminModel = new User
                {
                    UserName = "admin@admin.ru",
                    Email = "admin@admin.ru",
                };
                await userManager.CreateAsync(adminModel, "123");
                await userManager.AddToRoleAsync(adminModel, "Администратор");
                await userManager.AddToRoleAsync(adminModel, "Пользователь");
            }
        }
    }
}

