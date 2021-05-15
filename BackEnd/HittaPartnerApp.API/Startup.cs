using HittaPartnerApp.API.Data;
using HittaPartnerApp.API.Services.IRepositories;
using HittaPartnerApp.API.Services.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HittaPartnerApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<HittaPartnerDbContext>(options=>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddCors();
            services.AddScoped<IAuthentication, Authentication>();
            services.AddSwaggerGen(Options=>
            {
                Options.SwaggerDoc("HittaPartnerOpenAPISpec", new Microsoft.OpenApi.Models.OpenApiInfo
                { 
                    Title="Hitta Partner App API",
                    Version="1",
                    Description= "Hitta Partner App Open Api(Examensarbete)",
                    Contact=new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Email="bahaa.abokhaled83@gmail.com",
                        Name="Bahaa Abo khaled"
                    }
                });
                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.Xml";
                var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                Options.IncludeXmlComments(cmlCommentsFullPath);
                
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(Options=>
            {
                Options.SwaggerEndpoint("/Swagger/HittaPartnerOpenAPISpec/swagger.json", "Hitta Partner App API");
                Options.RoutePrefix = "";
            });

            app.UseRouting();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
