using Commons.JWT;
using Microsoft.Extensions.DependencyInjection;

namespace Commons;

public class ModuleInitializer : IModuleInitializer
{
    public void Initialize(IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
    }
}
