using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Omar Mohamed",
                    Email = "dracocap@gmail.com",
                    PhoneNumber = "012345689",
                    UserName = "dracocap"
                };
                await userManager.CreateAsync(user,"Pa$$w0rd");
            }
        }
    }
}
