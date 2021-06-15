using AutoMapper;
using HittaPartnerApp.API.Data;
using HittaPartnerApp.API.Helpers;
using HittaPartnerApp.API.Services.Data;
using HittaPartnerApp.API.Services.IRepositories;
using HittaPartnerApp.API.Services.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
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
            services.AddControllers().AddNewtonsoftJson(options =>

            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddDbContext<HittaPartnerDbContext>(options=>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddCors(opttions=> 
            {
                opttions.AddPolicy("AllowAllHeaders",
                    buildir =>
                    {
                        buildir.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
            services.AddSignalR(options=> 
            {
                options.EnableDetailedErrors = true;
            });
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
            services.AddTransient<TrialData>();
            services.AddScoped<IAuthentication, Authentication>();
            services.AddScoped<IHittaPartnerRepo, HittaPartnerRepo>();
            services.AddScoped<UserActivity>();
            services.AddAutoMapper(typeof(HittaPartnerProfile));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(Options =>
                {
                    Options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
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
                // Lägg till JWT token inom Swagger .
                Options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description =
                    "Jwt Authorization Header useing the Bearer Schena.\r\r\r\n Enter 'Bearer '[mellanslag]'och sedan din token i texttimatning nedan, ",
                    Name = "Authorization",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                Options.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {new OpenApiSecurityScheme
                    {
                        Reference= new OpenApiReference
                        {
                            Type= ReferenceType.SecurityScheme,
                            Id="Bearer"
                        },
                        Scheme="oauth2",
                        Name="Bearer",
                        In=ParameterLocation.Header,

                    },
                    new List<string>()
                    }

                });


            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,TrialData seedData)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(option =>
                {
                    option.Run(
                        async context =>
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            var ex = context.Features.Get<IExceptionHandlerFeature>();
                            if (ex != null)
                            {
                                await context.Response.WriteAsync(ex.Error.Message);
                            }
                        });
                 } );
                
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(Options=>
            {
                Options.SwaggerEndpoint("/Swagger/HittaPartnerOpenAPISpec/swagger.json", "Hitta Partner App API");
                Options.RoutePrefix = "";
            });
            
            app.UseRouting();
            //seedData.TrialUsers();
            app.UseCors("AllowAllHeaders");
            app.UseAuthentication();
            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chat");
            });
        }
    }
}
