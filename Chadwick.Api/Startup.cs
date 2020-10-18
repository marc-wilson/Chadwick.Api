using System.IO;
using Chadwick.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Chadwick.Api
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="hostingEnvironment"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        private IConfiguration Configuration { get; }
        private IWebHostEnvironment HostingEnvironment { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<ChadwickDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSwaggerGen(ConfigureSwaggerUi);
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
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Chadwick API v1");
                c.DisplayOperationId();
                c.DocumentTitle = "Chadwick API";
                c.DisplayRequestDuration();
            });
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
        
        /// <summary>
        /// Configures Swagger
        /// </summary>
        /// <param name="swaggerGenOptions"></param>
        private void ConfigureSwaggerUi(SwaggerGenOptions swaggerGenOptions)
        {
            swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Chadwick API",
                Version = "v1"
            });
            
            // swaggerGenOptions.AddSecurityDefinition("JWT", new OpenApiSecurityScheme
            // {
            //     Name = "Bearer",
            //     BearerFormat = "JWT",
            //     Scheme = "bearer",
            //     Description = "Specify the authorization token.",
            //     In = ParameterLocation.Header,
            //     Type = SecuritySchemeType.Http,
            // });
            //
            // swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement {
            //     {
            //         new OpenApiSecurityScheme
            //         {
            //             Reference = new OpenApiReference
            //             {
            //                 Type = ReferenceType.SecurityScheme,
            //                 Id = "JWT"
            //             }
            //         },
            //         new string[] { }
            //     }
            // });

            var filePath = Path.Combine(HostingEnvironment.ContentRootPath, "ChadwickAPI.config");
            swaggerGenOptions.IncludeXmlComments(filePath);
        }
    }
}