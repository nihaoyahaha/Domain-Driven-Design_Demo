using Commons;
using Microsoft.Extensions.DependencyInjection;
using StudentService.Domain;

namespace StudentService.Infrastructure;

public class ModuleInitializer : IModuleInitializer
{
    public void Initialize(IServiceCollection services)
    {
        services.AddScoped<IStudentRepository,StudentRepository>();
        services.AddScoped<IIdentityRepository, IdentityRepository>();
		services.AddScoped<StudentDomainService>();
		services.AddScoped<IdentityDomainService>();
	}

}
