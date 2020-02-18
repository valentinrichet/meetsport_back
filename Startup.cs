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
using MeetSport.Models;
using Microsoft.OpenApi.Models;

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
            services.AddDbContext<projetContext>(optionsBuilder =>
            {
                optionsBuilder.UseMySql(Configuration.GetConnectionString(Settings.CONNECTION_STRING_URL), x => x.ServerVersion(Configuration.GetConnectionString(Settings.CONNECTION_STRING_VERSION)));
            });

            /* Configure Swagger */
            services.AddSwaggerGen(c =>
            {
                IConfigurationSection swaggerSection = Configuration.GetSection(Settings.SWAGGER);
                IConfigurationSection swaggerContactSection = swaggerSection.GetSection(Settings.SWAGGER_CONTACT);

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = swaggerSection.GetValue<string>(Settings.SWAGGER_VERSION),
                    Title = swaggerSection.GetValue<string>(Settings.SWAGGER_TITLE),
                    Description = swaggerSection.GetValue<string>(Settings.SWAGGER_DESCRIPTION),
                    Contact = new OpenApiContact
                    {
                        Name = swaggerContactSection.GetValue<string>(Settings.SWAGGER_CONTACT_NAME),
                        Email = swaggerContactSection.GetValue<string>(Settings.SWAGGER_CONTACT_EMAIL)
                    }
                });
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                IConfigurationSection swaggerSection = Configuration.GetSection(Settings.SWAGGER);

                c.SwaggerEndpoint(swaggerSection.GetValue<string>(Settings.SWAGGER_ENDPOINT), $"{swaggerSection.GetValue<string>(Settings.SWAGGER_TITLE)} {swaggerSection.GetValue<string>(Settings.SWAGGER_VERSION)}");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
