using Commons.JWT;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
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
        //日志配置
        builder.Services.AddLogging(builder =>
        {
                Log.Logger = new LoggerConfiguration()
                   // .MinimumLevel.Information().Enrich.FromLogContext()
                   .WriteTo.Console()
                   .CreateLogger();
                builder.AddSerilog();
        });
       
    }
}
