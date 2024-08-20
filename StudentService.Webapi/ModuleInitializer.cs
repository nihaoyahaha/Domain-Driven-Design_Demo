using FluentValidation;
using StudentService.Domain;
using StudentService.Webapi.Validate;
using Zack.Commons;

namespace StudentService.Webapi;

public class ModuleInitializer : IModuleInitializer
{
    public void Initialize(IServiceCollection services)
    {
        services.AddScoped<IValidator<AddStudentReq>,AddStudentValidation>();
    }

}
