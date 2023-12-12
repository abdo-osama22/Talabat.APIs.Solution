using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Service;
using Talabat.Repository.Identity;
using Talabat.Service;

namespace TalabatAPIs.Extensions
{
    public static class IdentityServicesExtensions
    {

        // 1: Call AddIdentityServices <AppUser, IdentityRole> () with Get Class and Interfaces;
        // 1: Fire Dependency Injection for three class User and Role ,..



        /// <summary>
        /// Call AddIdentity,AddEntityFrameworkStores,AddAuthentication,AddScoped<ITokenService, TokenService>
        /// </summary>
        /// <param name="Services"></param>
        /// <returns></returns>
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services, IConfiguration configuration)
        {

            Services.AddIdentity<AppUser, IdentityRole>(option => { })
                    .AddEntityFrameworkStores<AppIdentityDbContext>();

           

               
                Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer( option =>
                {


                    option.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        ValidAudience = configuration["JWT:ValidAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                    };
                });

            Services.AddScoped<ITokenService, TokenService>();

            return Services;
        }
    }
}
