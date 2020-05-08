using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Furs2Feathers.DataAccess.Models;
using Furs2Feathers.DataAccess.Repositories;
using Furs2Feathers.Domain.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;


namespace Furs2FeathersAPI
{
    //dotnet ef dbcontext scaffold "Host=localhost;Database=mydatabase;Username=postgres;Password=35122jhb" Npgsql.EntityFrameworkCore.PostgreSQL -o Models
    public class Startup
    {
        private const string CorsPolicyName = "AllowConfiguredOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddApplicationInsightsTelemetry();

            // switch between database providers using runtime configuration
            // (the existing migrations are SQL-Server-specific, but the model itself is not)

            // this should be the name of a connection string.
            string whichDb = Configuration["DatabaseConnection"];
            if (whichDb is null)
            {
                throw new InvalidOperationException($"No value found for \"DatabaseConnection\"; unable to connect to a database.");
            }

            string connection = Configuration.GetConnectionString(whichDb);
            if (connection is null)
            {
                throw new InvalidOperationException($"No value found for \"{whichDb}\" connection; unable to connect to a database.");
            }

            if (whichDb.Contains("PostgreSql", StringComparison.InvariantCultureIgnoreCase))
            {
                services.AddDbContext<f2fdbContext>(options =>
                    options.UseNpgsql(connection));
            }
            else
            {
                services.AddDbContext<f2fdbContext>(options =>
                    options.UseSqlServer(connection));
            }

           
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IClaimsRepository, ClaimsRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<IPlanProLabelsRepository, PlanProLabelsRepository>();
            services.AddScoped<IPlanReviewsRepository, PlanReviewsRepository>();
            services.AddScoped<IPoliciesRepository, PoliciesRepository>();

            // support switching between database providers using runtime configuration
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fur2Feathers API", Version = "v1" });
            });
            
            var allowedOrigins = Configuration.GetSection("CorsOrigins").Get<string[]>();

            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName, builder =>
                    builder.WithOrigins(allowedOrigins ?? Array.Empty<string>())
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddControllers(options =>
            {
                // remove the default text/plain string formatter to clean up the OpenAPI document
                options.OutputFormatters.RemoveType<StringOutputFormatter>();

                options.ReturnHttpNotAcceptable = true;
                options.SuppressAsyncSuffixInActionNames = false;
            });
       
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, f2fdbContext fdbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (Configuration.GetValue("UseHttpsRedirection", defaultValue: true) is true)
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseCors(CorsPolicyName);

            app.UseAuthorization();
            using (var scope =
                    app.ApplicationServices.CreateScope())

                // Mish style injection and migrate
                fdbContext.Database.EnsureDeleted();
                fdbContext.Database.Migrate();
            /*fdbContext.Database.EnsureCreated();*/

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fur2Feathers API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
