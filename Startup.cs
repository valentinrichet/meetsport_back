using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MeetSport.Dbo;
using MeetSport.Repositories;
using MeetSport.Repositories.Database;
using MeetSport.Business;
using MeetSport.Business.Database;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;
using System.Reflection;
using System.IO;
using MeetSport.Options;

namespace MeetSport
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
            /* Configure Database */
            services.AddDbContext<MeetSportContext>(optionsBuilder =>
            {
                optionsBuilder.UseMySql(Configuration.GetConnectionString(Settings.CONNECTION_STRING_URL), x => x.ServerVersion(Configuration.GetConnectionString(Settings.CONNECTION_STRING_VERSION)));
            });
            /* ****************** */

            /* Configure Swagger */
            services.AddSwaggerGen(c =>
            {
                SwaggerOptions swaggerOptions = Configuration.GetSection(Settings.SWAGGER).Get<SwaggerOptions>();

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = swaggerOptions.Version,
                    Title = swaggerOptions.Title,
                    Description = swaggerOptions.Description,
                    Contact = new OpenApiContact
                    {
                        Name = swaggerOptions.Contact.Name,
                        Email = swaggerOptions.Contact.Email
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                string xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            /* ***************** */

            /* Register Automapper */
            services.AddAutoMapper(typeof(Startup));
            /* ******************* */

            /* Register Repositories */
            services.AddScoped<IRepository<Role>, DbRepository<Role, MeetSportContext>>();
            /* ********************* */

            /* Register Business */
            services.AddScoped<IBusiness<Role>, Business<Role, IRepository<Role>>>();
            /* ***************** */

            /* Configure Cors */
            services.AddCors();
            /* ************** */

            /* Configure Controllers */
            services.AddControllers();
            /* ********************* */

            /* Configure InvalidModelStateResponseFactory */
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    ValidationProblemDetails problemDetails = new ValidationProblemDetails(context.ModelState);
                    BadRequestObjectResult result = new BadRequestObjectResult(problemDetails.Errors);
                    return result;
                };
            });
            /* ****************************************** */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        IExceptionHandlerPathFeature exceptionHandler = context.Features.Get<IExceptionHandlerPathFeature>();
                        logger.LogError(exceptionHandler.Error, exceptionHandler.Path);
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("There was an error with the server, please try again later...");
                    });
                });
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                SwaggerOptions swaggerOptions = Configuration.GetSection(Settings.SWAGGER).Get<SwaggerOptions>();

                c.SwaggerEndpoint(swaggerOptions.EndPoint, $"{swaggerOptions.Title} {swaggerOptions.Version}");
            });

            app.UseRouting();

            // Global Cors Policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
