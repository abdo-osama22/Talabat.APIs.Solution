using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.Core.Entities.Identity;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using TalabatAPIs.Extensions;
using TalabatAPIs.Middleware;

namespace TalabatAPIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region Add services to the container.

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<StoreContext>(op =>
            {
                op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(option =>
            {
                var connection = builder.Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(connection);
            });

            builder.Services.AddDbContext<AppIdentityDbContext>(op =>
            {
                op.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });


            builder.Services.AddApplicationServices();

            builder.Services.AddIdentityServices(builder.Configuration);

            #endregion


            var app = builder.Build();

            #region Update database and  Data Seeding

            //StoreContext context = new StoreContext();
            //await context.Database.MigrateAsync();


            using var Scope = app.Services.CreateScope(); // group of Service lifetime is Scope
            var Services = Scope.ServiceProvider; // attach Services // Like DbContext

            var lggerfactory = Services.GetRequiredService<ILoggerFactory>();// Ask CLR Create object of ILoggerFactory  Explicity
            try
            {
                var dbContext = Services.GetRequiredService<StoreContext>(); // Ask CLR Create object of StoreContext  Explicity
                await dbContext.Database.MigrateAsync(); // Apply Migration

                var IdentityContext = Services.GetRequiredService<AppIdentityDbContext>();
                await IdentityContext.Database.MigrateAsync();


                await StoreContextSeed.SeedAsync(dbContext);


                var userManager = Services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUserAsync(userManager);
            }
            catch (Exception e)
            {
                var loger = lggerfactory.CreateLogger<Program>();
                loger.LogError(e, "An Error Occurred  During Apply Database");
            }

            #endregion



            #region Configure the HTTP request pipeline

            app.UseMiddleware<ExceptionMiddleWare>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWare();

                //app.UseSwagger();
                //app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");


            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();

            #endregion
        }
    }
}