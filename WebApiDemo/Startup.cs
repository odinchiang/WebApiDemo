using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebApiDemo.Filters;

namespace WebApiDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // 跨域存取
            services.AddCors(ac => ac.AddPolicy("any", ap => ap.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            // Swagger
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("V1", new OpenApiInfo()
                {
                    Title = "WebApiDemo",
                    Version = "V1",
                    Description = "WebApi 從入門到精通"
                });

                // 若要 api 在 swagger 中顯示註解
                // 專案/屬性/建置 => 勾選 "XML 文件檔案"
                // 路徑用相對路徑 => \WebApiDemo.xml
                // 勿用絕對路徑，否則放上伺服器會有路徑問題
                var xmlPath = AppDomain.CurrentDomain.BaseDirectory + "WebApiDemo.xml";
                x.IncludeXmlComments(xmlPath);
            });

            services.AddControllers();

            // 全域使用 action filter
            //services.AddControllers(o => o.Filters.Add(typeof(CtmActionFilterAttribute)));
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // 跨域存取 (一定要放在 UseRouting 下面)
            app.UseCors();

            app.UseAuthorization();

            // Swagger
            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/V1/swagger.json", "WebApi 從入門到精通");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
