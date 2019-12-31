using Api.LeaderBoard.Service.Database;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace api_leaderboard_service
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        #region snippet_ConfigureServices
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            //services.AddDbContext<TodoContext>(opt =>
            //    opt.UseInMemoryDatabase("TodoList"));
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ScoreProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                //c.SwaggerDoc("v1", new OpenApiInfo
                //{
                //    Version = "v1",
                //    Title = "ToDo API",
                //    Description = "A simple example ASP.NET Core Web API",
                //    TermsOfService = new Uri("https://example.com/terms"),
                //    Contact = new OpenApiContact
                //    {
                //        Name = "Shayne Boyer",
                //        Email = string.Empty,
                //        Url = new Uri("https://twitter.com/spboyer"),
                //    },
                //    License = new OpenApiLicense
                //    {
                //        Name = "Use under LICX",
                //        Url = new Uri("https://example.com/license"),
                //    }
                //});

                //// Set the comments path for the Swagger JSON and UI.
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.DescribeAllEnumsAsStrings();
                c.DescribeStringEnumsInCamelCase();
            });
            services.AddScoped<IScoreDbRepository, ScoreDbRepository>();
        }
        #endregion

        #region snippet_Configure
        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API");
                c.RoutePrefix = "swagger/ui";
            });
            app.UseCors(
        options => options.WithOrigins("*").AllowAnyMethod().AllowAnyHeader());
            app.UseMvc();
        }
        #endregion
    }
}
