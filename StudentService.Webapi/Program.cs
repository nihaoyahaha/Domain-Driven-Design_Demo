using Commons;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using StudentService.Domain;
using StudentService.Domain.Entities;
using StudentService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureExtraServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>{
    c.SwaggerDoc("v1", new() { Title = "StudentDemo.WebAPI", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("AddSection", async (StudentDomainService service, AddSectionReq req) =>
{
    try
    {
        await service.AddGradeAndSectionAsync(req);
        return Results.Ok("班级添加成功!");
    }
    catch (System.Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithMetadata(new UnitOfWorkAttribute(typeof(StudentDbContext)))
.AddEndpointFilter<UnitOfWorkEndpointFilter>()
.AddFluentValidationAutoValidation();

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
    try
    {
        await service.StudentChangeSectionAsync(req);
        return Results.Ok("学生调班成功!");
    }
    catch (System.Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }

})
.WithMetadata(new UnitOfWorkAttribute(typeof(StudentDbContext)))
.AddEndpointFilter<UnitOfWorkEndpointFilter>()
.AddFluentValidationAutoValidation();

app.MapPost("SectionHasStudent", async (StudentDomainService service, QueryStudentReq req) =>
{
    try
    {
        bool result = await service.SectionExistStudentAsync(req);
        return Results.Ok(result);
    }
    catch (System.Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.AddFluentValidationAutoValidation();

app.MapGet("Students/{studentId}", async (StudentDomainService service, string studentId) =>
{
    return await service.FindStudentByIdAsync(studentId);
});

app.UseDefault();
app.Urls.Add("http://*:5089");
app.Run();
