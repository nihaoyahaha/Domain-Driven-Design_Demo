using Commons;
using Microsoft.Extensions.DependencyInjection;
using StudentService.Domain;
using Zack.Commons;

namespace StudentService.Infrastructure;

public class ModuleInitializer : IModuleInitializer
{
    public void Initialize(IServiceCollection services)
    {
        services.AddScoped<IStudentRepository,StudentRepository>();
        services.AddScoped<IIdentityRepository, IdentityRepository>();
    }

}
