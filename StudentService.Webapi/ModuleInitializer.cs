using FluentValidation;
using StudentService.Domain;
using StudentService.Domain.Entities;
using StudentService.Webapi.Validate;
using Zack.Commons;

namespace StudentService.Webapi;

public class ModuleInitializer : IModuleInitializer
{
    public void Initialize(IServiceCollection services)
    {
        services.AddScoped<IValidator<AddStudentReq>,AddStudentValidation>();
        services.AddScoped<IValidator<AddSectionReq>, AddSectionValidation>();
        services.AddScoped<IValidator<ChangeSectionReq>, ChangeSectionValidation>();
        services.AddScoped<IValidator<QueryStudentReq>, QueryStudentValidation>();
    }
}
