using Commons.JWT;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Formatting.Compact;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.SwaggerGen;
using Zack.Commons;

namespace Commons;

public static class WebApplicationBuilderExtensions
{
	public static void ConfigureExtraServices(this WebApplicationBuilder builder)
	{
		//从环境变量读取配置信息
		builder.Services.Configure<DatabaseConfig>(builder.Configuration.AddEnvironmentVariables("DB_").Build());
		builder.Services.Configure<RedisConfig>(builder.Configuration.AddEnvironmentVariables("redis_").Build());
		builder.Services.Configure<JWTOptions>(builder.Configuration.AddEnvironmentVariables("jwt_").Build());
		builder.Services.Configure<CorsSettings>(builder.Configuration.AddEnvironmentVariables("Cors_").Build());

		//注册所有程序集中的服务
		var assemblies = ReflectionHelper.GetAllReferencedAssemblies();
		builder.Services.RunModuleInitializers(assemblies);
		//注册所有DbContext
		builder.Services.AddAllDbContexts(ctx =>
		{
			DatabaseConfig dbConf = builder.Configuration.Get<DatabaseConfig>();
			var connStr = $"Host={dbConf.DBHost};Port={dbConf.DBPort};Database={dbConf.DBDatabase};Username={dbConf.DBUsername};Password={dbConf.DBPassword};Persist Security Info=True";
			//string connStr ="Host=localhost;Database=Student;Username=postgres;Persist Security Info=True;Password=postgre123456";
			ctx.UseNpgsql(connStr);
		}, assemblies);

		//身份认证和swagger配置
		builder.Services.AddAuthorization();
		builder.Services.AddAuthentication();
		JWTOptions jwtOpt = builder.Configuration.Get<JWTOptions>();
		builder.Services.AddJWTAuthentication(jwtOpt);
		builder.Services.Configure<SwaggerGenOptions>(c =>
		{
			c.AddAuthenticationHeader();
		});

		//注册FlentValidation验证
		builder.Services.AddFluentValidationAutoValidation();

		//跨域配置
		builder.Services.AddCors(options =>
		{
			var corsOpt = builder.Configuration.Get<CorsSettings>();
			string[] urls = corsOpt.Origins.Split(',');
			options.AddDefaultPolicy(builder => builder.WithOrigins(urls)
					.AllowAnyMethod().AllowAnyHeader().AllowCredentials());
		});

		//redis配置
		RedisConfig redisConfig = builder.Configuration.Get<RedisConfig>();
		var confOpt = new ConfigurationOptions
		{
			EndPoints = { { redisConfig.Host, int.Parse(redisConfig.Port) } },
			Password = redisConfig.Password
		};
		builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(confOpt));

		//阶段2初始化,配置最终的记录器
		builder.Services.AddSerilog((services, lc) => lc
			.ReadFrom.Configuration(builder.Configuration)
			.ReadFrom.Services(services)
			.Enrich.FromLogContext()
			.WriteTo.File(
		new RenderedCompactJsonFormatter(),
		Path.Combine("logs", "log-.json"), // 本地文件路径，包含日期
		rollingInterval: RollingInterval.Hour,
				 fileSizeLimitBytes: 5 * 1024 * 1024,
				 retainedFileCountLimit: 10,
				 rollOnFileSizeLimit: true,
				 shared: true,
				 flushToDiskInterval: TimeSpan.FromSeconds(1))
		.WriteTo.File(
			Path.Combine("logs", "sec-log-.json"),
			outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
		.WriteTo.Console());

	}
}
