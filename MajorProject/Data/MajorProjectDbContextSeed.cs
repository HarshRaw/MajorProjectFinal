
using System;
using MajorProject.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace MajorProject.Data
{
    public class MajorProjectDbContextSeed
    {
        public static async Task SeedIdentityRoleAsync(RoleManager<IdentityRole> rm)
        {
            foreach(MyIdentityRoleNames rolename in Enum.GetValues(typeof(MyIdentityRoleNames)))
            {
                if(!await rm.RoleExistsAsync(rolename.ToString()))
                {
                    await rm.CreateAsync(new IdentityRole { Name = rolename.ToString() });
                }
            }
        }


        public static async Task SeedIdentityUserAsync(UserManager<IdentityUser> um)
        {
            IdentityUser user;
            user = await um.FindByNameAsync("Admin");
            if(user == null)
            {
                user = new IdentityUser()
                {
                    UserName = "Admin",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true
                };
                await um.CreateAsync(user,password:"Admin@1234");

                await um.AddToRolesAsync(user, new string[] 
                { 
                    MyIdentityRoleNames.RoleAdmin.ToString(), 
                    MyIdentityRoleNames.RoleUser.ToString() 
                });
            }
            user = await um.FindByNameAsync("Harsh");
            if(user == null)
            {
                user = new IdentityUser()
                {
                    UserName = "Harsh",
                    Email = "hr@gmail.com",
                    EmailConfirmed= true
                };
                await um.CreateAsync(user, password: "Hr@1234");
                await um.AddToRoleAsync(user, 
                    MyIdentityRoleNames.RoleUser.ToString()
                );
            }
            
        }
    }
}
