using Microsoft.AspNetCore.Builder;

namespace Commons;

 public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDefault(this IApplicationBuilder app)
        {
            app.UseCors();
            app.UseForwardedHeaders();
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }
    }