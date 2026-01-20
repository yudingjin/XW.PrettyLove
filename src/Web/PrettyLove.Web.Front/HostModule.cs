using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PrettyLove.Application;
using PrettyLove.Core;
using System;
using System.IO;
using System.Reflection;

namespace PrettyLove.Web.Front
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(ApplicationModule))]
    public class HostModule : IWebModule
    {
        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="context"></param>
        public void ConfigureServices(ServiceConfigurationContext context)
        {
            // 解决中文乱码问题
            context.Services.AddControllers(options =>
            {
                // API统一返回固定格式的数据
                options.Filters.Add<FriendlyActionFilterAttribute>();
                // API统一异常过滤器
                options.Filters.Add<FriendlyExceptionFilterAttribute>();
            }).AddDynamicWebApi(context.Configuration)
              .AddNewtonsoftJson(options =>
              {
                  // 首字母小写(驼峰样式)
                  options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                  // 时间格式化
                  options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                  // 忽略循环引用
                  options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                  // 忽略空值
                  // options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                  // 添加自定义转换器
                  options.SerializerSettings.Converters.Add(new LongJsonConverter());
              });
            context.Services.AddEndpointsApiExplorer();
            context.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = $"PrettyLove HTTP API V1",
                    Description = $"PrettyLove HTTP API V1",
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "输入请求头中需要添加Jwt授权Token：Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme(new OpenApiSecurityScheme(new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        })),
                        new string[] { }
                    }
                });
                c.OrderActionsBy(o => o.RelativePath);
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, true);
            });

            // 禁用默认模型验证过滤器
            context.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // 添加自定义授权
            context.Services.AddCustomJwtBearer(context.Configuration.GetSection("Jwt").Get<JwtOptions>());
            // 跨域支持
            context.Services.AddCors(options =>
            {
                options.AddPolicy
                (
                    name: "myCors",
                    builde =>
                    {
                        builde.WithOrigins(context.Configuration.GetValue<string>("corsUrls").Split(","))
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    }
                );
            });
        }

        /// <summary>
        /// 应用初始化
        /// </summary>
        /// <param name="context"></param>
        public void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var builder = context.Builder.UseSerilogAsync(context.Builder.Configuration);
            var app = builder.Build();
            app.ConfigureApplication();
            var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "upload");
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "upload")),
                RequestPath = new Microsoft.AspNetCore.Http.PathString("/static")
            });
            app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "upload")),
                RequestPath = new Microsoft.AspNetCore.Http.PathString("/static")
            });
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/swagger/V1/swagger.json", $"PrettyLove HTTP API V1");
                    c.RoutePrefix = "swagger";
                });
            }
            app.UseCors("myCors");
            app.Use(async (context, next) =>
            {
                TransactionalAttribute.SetServiceProvider(context.RequestServices);
                await next();
            });
            app.UseCustomExceptionless();
            app.UseMiddleware<ForwardedHeadersMiddleware>();
            app.UseMiddleware<UnifyResultStatusCodesMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
