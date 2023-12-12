namespace TalabatAPIs.Extensions
{
    public static class AddSwaggerExtention
    {
        public static IApplicationBuilder UseSwaggerMiddleWare(this IApplicationBuilder app)
        => app.UseSwagger().UseSwaggerUI();
        
    }
}
