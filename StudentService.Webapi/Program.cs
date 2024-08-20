using Commons;
using Microsoft.EntityFrameworkCore;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using StudentService.Domain;
using StudentService.Domain.Entities;
using StudentService.Infrastructure;
using Zack.Commons;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging();
var assemblies = ReflectionHelper.GetAllReferencedAssemblies();
builder.Services.RunModuleInitializers(assemblies);
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StudentDbContext>(
    opt => opt.UseNpgsql("Host=localhost;Database=Student;Username=postgres;Persist Security Info=True;Password=postgre123456")
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("AddSection", async (StudentDomainService service, string sectionName, string gradeName) =>
{
    await service.AddGradeAndSectionAsync(sectionName, gradeName);
})
.WithMetadata(new UnitOfWorkAttribute(typeof(StudentDbContext)))
.AddEndpointFilter<UnitOfWorkEndpointFilter>();


app.MapPost("AddStudent", async (StudentDomainService service, AddStudentReq req) =>
{
    try
    {
        await service.SetionAddStudentAsync(req);
        return Results.Ok("学生添加成功!");
    }
    catch (System.Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithMetadata(new UnitOfWorkAttribute(typeof(StudentDbContext)))
.AddEndpointFilter<UnitOfWorkEndpointFilter>()
.AddFluentValidationAutoValidation();

app.MapPost("ChangeSection", async (StudentDomainService service, ChangeSectionReq req) =>
{
    await service.StudentChangeSectionAsync(req);
})
.WithMetadata(new UnitOfWorkAttribute(typeof(StudentDbContext)))
.AddEndpointFilter<UnitOfWorkEndpointFilter>();

app.MapPost("SectionHasStudent", async (StudentDomainService service, QueryStudentReq req) =>
{
    return await service.SectionExistStudentAsync(req);
});

app.MapGet("Students/{studentId}", async (StudentDomainService service, string studentId) =>
{
    return await service.FindStudentByIdAsync(studentId);
});

app.Urls.Add("http://*:5089");
app.Run();
