using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameCollection.WebApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace GameCollection.WebApi
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

            //Cors
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithMethods("http://localhost:1234")
                    .AllowAnyMethod()
                    .AllowCredentials());
            });

            //Jwt
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Issuer"]))
                    };
                });

            services.AddMvcCore().AddVersionedApiExplorer(
                    options =>
                    {
                        options.GroupNameFormat = "'v'VVV";

                                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                                    // can also be used to control the format of the API version in route templates
                                    options.SubstituteApiVersionInUrl = true;
                    });

            services.AddMvc();

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);

            });
           
            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach(var desc in provider.ApiVersionDescriptions)
                {
                    c.SwaggerDoc(desc.GroupName, CreateInfoSwagger(desc));
                }

                // Set the comments path for the Swagger JSON and UI.
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "GameCollection.WebApi.xml");
                c.IncludeXmlComments(xmlPath);
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //UseCors
            app.UseCors("AllowSpecificOrigin");

            app.UseSecurityHeadersMiddleware(s =>
            {
                s.RemoveHeader("X-Powered-By")
                .AddHeader("X-Content-Type-Options", "=nosniff")
                .AddHeader("Referrer-Policy","no-referrer");
                
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                foreach(var desciption in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{desciption.GroupName}/swagger.json", desciption.GroupName.ToUpperInvariant());
                }
            });


            app.UseAuthentication();

            app.UseMvc();


        }

        private static Info CreateInfoSwagger(ApiVersionDescription desc)
        {
            var info = new Info()
            {
                Title = $"GameCollection API {desc.ApiVersion}",
                Version = desc.ApiVersion.ToString(),
                Description = "A simple example ASP.NET Core Web API",
                TermsOfService = "None",
                Contact = new Contact { Name = "Ludovic HUSSON", Email = "", Url = "" },
                License = new License { Name = "Use under LICX", Url = "https://example.com/license" }
            };

            return info;
        }
    }
}
