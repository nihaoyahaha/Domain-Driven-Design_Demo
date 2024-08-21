using Commons.JWT;
using Microsoft.Extensions.DependencyInjection;
using Zack.Commons;

namespace Commons;

public class ModuleInitializer : IModuleInitializer
{
    public void Initialize(IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
    }
}
