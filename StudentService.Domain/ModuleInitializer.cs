using Microsoft.Extensions.DependencyInjection;
using Zack.Commons;

namespace StudentService.Domain;

public class ModuleInitializer : IModuleInitializer
{
    public void Initialize(IServiceCollection services)
    {
        services.AddScoped<StudentDomainService>();
        services.AddScoped<IdentityDomainService>();
    }

}
