namespace Talabat.APIs.Extensions
{
    public static class AddSwaggerExtention
    {
        public static WebApplication UseSwaggerMiddelewares(this WebApplication app) 
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
