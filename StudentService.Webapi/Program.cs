using Commons;
using Microsoft.EntityFrameworkCore;
using StudentService.Domain;
using StudentService.Domain.Entities;
using StudentService.Infrastructure;
using Zack.Commons;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging();
var assemblies = ReflectionHelper.GetAllReferencedAssemblies();
builder.Services.RunModuleInitializers(assemblies);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StudentDbContext>(
    opt=> opt.UseNpgsql("Host=localhost;Database=Student;Username=postgres;Persist Security Info=True;Password=postgre123456")
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("AddSection",async (StudentDomainService service,string sectionName,string gradeName)=>{
    await service.AddGradeAndSectionAsync(sectionName, gradeName);
})
.WithMetadata(new UnitOfWorkAttribute(typeof(StudentDbContext)))
.AddEndpointFilter<UnitOfWorkEndpointFilter>();


app.MapPost("AddStudent",async (StudentDomainService service,AddStudentReq req)=>{
    await service.SetionAddStudentAsync(req);
})
.WithMetadata(new UnitOfWorkAttribute(typeof(StudentDbContext)))
.AddEndpointFilter<UnitOfWorkEndpointFilter>();

app.MapPost("ChangeSection",async(StudentDomainService service,ChangeSectionReq req)=>{
    await service.StudentChangeSectionAsync(req);
})
.WithMetadata(new UnitOfWorkAttribute(typeof(StudentDbContext)))
.AddEndpointFilter<UnitOfWorkEndpointFilter>();

app.MapPost("SectionHasStudent",async(StudentDomainService service,QueryStudentReq req)=>{
    return await service.SectionExistStudentAsync(req);
});

app.MapGet("Students/{studentId}",async (StudentDomainService service, string studentId)=>{
    return await service.FindStudentByIdAsync(studentId);
});

app.Urls.Add("http://*:5089");
app.Run();
