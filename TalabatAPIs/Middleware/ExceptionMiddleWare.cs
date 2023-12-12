using System.Net;
using System.Text.Json;
using TalabatAPIs.Errors;

namespace TalabatAPIs.Middleware
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleWare> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleWare(RequestDelegate next, ILogger<ExceptionMiddleWare> logger,IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }


        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
              await  next.Invoke(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode =(int) HttpStatusCode.InternalServerError;
               
                var Response = env.IsDevelopment() ? new ApiExceptionResponse(500, ex.Message, ex.StackTrace.ToString()): new ApiExceptionResponse(500);

                var JsonResponse = JsonSerializer.Serialize(Response, new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                }) ;

               await context.Response.WriteAsync(JsonResponse);

            }

        }



    }
}
