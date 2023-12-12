using Microsoft.AspNetCore.Mvc;
using Talabat.Core;
using Talabat.Core.Repository;
using Talabat.Core.Service;
using Talabat.Repository;
using Talabat.Service;
using TalabatAPIs.Errors;
using TalabatAPIs.Helper;

namespace TalabatAPIs.Extensions
{
    public static class ApplicationServicesExtension
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            #region Generic Repository ,UnitOfWork Servisce


            //Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product> >();
            Services.AddScoped<IOrderService, OrderService>();
            Services.AddScoped<IPaymentService, PaymentService>();

            Services.AddScoped<IUnitOfWork, UnitOfWork > ();
           // Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));  // we don't need that now because we use Unit of work

            #endregion


            #region Mapper 

            //builder.Services.AddAutoMapper( m => m.AddProfile(new MappingProfiles()) );
            Services.AddAutoMapper(typeof(MappingProfiles)); 
            #endregion


            #region HandelErrorBehavior BadRequest

            Services.Configure<ApiBehaviorOptions>(ABO =>
            {
                ABO.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    // ModelState => Dic [KeyValuePair] (Key => Name of parm, values => Error)
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                 .SelectMany(p => p.Value.Errors)
                                 .Select(e => e.ErrorMessage)
                                 .ToList();

                    var VER = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(VER);
                };
            });

            #endregion


            //Services.AddScoped<IBasketRepository, BasketRepository>();
            Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));


            return Services;
        }

    }
}
