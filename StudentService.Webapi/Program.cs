using Commons;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Formatting.Compact;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using StackExchange.Redis;
using StudentService.Domain;
using StudentService.Domain.Entities;
using StudentService.Infrastructure;
using System.Reflection;

//serilog阶段1初始化,创建引导记录器
Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Verbose()
	.Enrich.FromLogContext()
	.WriteTo.Console()
	.WriteTo.File(
			new RenderedCompactJsonFormatter(),
			 Path.Combine("Logs/BootstrapLogs", "bootstrap-log-.json"),
			rollingInterval: RollingInterval.Hour,
			fileSizeLimitBytes: 5 * 1024 * 1024,
			retainedFileCountLimit: 10,
			rollOnFileSizeLimit: true,
			shared: true,
			flushToDiskInterval: TimeSpan.FromSeconds(1))
	.CreateBootstrapLogger();

try
{
	Log.Information("Starting web application");
	var builder = WebApplication.CreateBuilder(args);
	builder.Services.AddHttpContextAccessor();
	builder.ConfigureExtraServices();
	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen(options =>
	{
		options.SwaggerDoc("v1", new() {
			Version = "v1",
			Title = "DDD-StudentDemo.WebAPI",
		});
	});
	//配置Identity
	builder.Services.AddDataProtection();
	builder.Services.AddIdentityCore<User>(opt =>
	{ //Guyugui123456_
	  //密码是否要求数字
		opt.Password.RequireDigit = true;
		//密码长度
		opt.Password.RequiredLength = 6;
		//密码是否需要小写  
		opt.Password.RequireLowercase = true;
		//密码是否需要大写
		opt.Password.RequireUppercase = true;
		//密码是否必须包含非字母数字字符
		opt.Password.RequireNonAlphanumeric = true;
		//错误锁定用户的次数
		opt.Lockout.MaxFailedAccessAttempts = 10;
		//锁定时间5分钟
		opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
		opt.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
		opt.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
	});
	IdentityBuilder ibuilder = new IdentityBuilder(typeof(User), typeof(StudentService.Domain.Role), builder.Services);
	ibuilder.AddEntityFrameworkStores<StudentDbContext>()
	.AddDefaultTokenProviders()
	.AddUserManager<UserManager<User>>()
	.AddRoleManager<RoleManager<StudentService.Domain.Role>>();

	var app = builder.Build();
	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();
	}

	app.UseHttpsRedirection();

	app.MapPost("AddSection", async (StudentDomainService service, AddSectionReq req) =>
	{
		await service.AddGradeAndSectionAsync(req);
		return Results.Ok("班级添加成功!");
	})
	.WithMetadata(new UnitOfWorkAttribute(typeof(StudentDbContext)))
	.AddEndpointFilter<UnitOfWorkEndpointFilter>()
	.AddFluentValidationAutoValidation();

	app.MapPost("AddStudent", async (StudentDomainService service, AddStudentReq req) =>
	{
		await service.SetionAddStudentAsync(req);
		return Results.Ok("学生添加成功!");
	})
	.WithMetadata(new UnitOfWorkAttribute(typeof(StudentDbContext)))
	.AddEndpointFilter<UnitOfWorkEndpointFilter>()
	.AddFluentValidationAutoValidation();

	app.MapPost("ChangeSection", async (StudentDomainService service, ChangeSectionReq req) =>
	{
		await service.StudentChangeSectionAsync(req);
		return Results.Ok("学生调班成功!");
	})
	.WithMetadata(new UnitOfWorkAttribute(typeof(StudentDbContext)))
	.AddEndpointFilter<UnitOfWorkEndpointFilter>()
	.AddFluentValidationAutoValidation();

	app.MapPost("SectionHasStudent", async (StudentDomainService service, QueryStudentReq req) =>
	{
		bool result = await service.SectionExistStudentAsync(req);
		return Results.Ok(result);
	})
	.AddFluentValidationAutoValidation();

	app.MapGet("Students/{studentId}", async (ILogger<Program> log, IConnectionMultiplexer rdb, StudentDomainService service, string studentId) =>
	{
		return await service.FindStudentByIdAsync(studentId);
	});

	app.UseDefault();
	app.Urls.Add("http://*:5089");
	app.Run();
}
catch (System.Exception ex)
{
	Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
	await Log.CloseAndFlushAsync();
}
