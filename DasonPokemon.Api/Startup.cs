using DasonPokemon.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Extensions.Repository;
using MongoDB.Extensions.Repository.Extensions;

namespace DasonPokemon.Api
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
            services.AddSpaStaticFiles(configuration =>
            {
              configuration.RootPath = "ClientApp/dist/dason-pokemon";
            });

            services.AddHttpClient();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DasonPokemon.Api", Version = "v1" });
            });

            services.AddSingleton<ICardService, CardService>();
            services.AddSingleton<ISetService, SetService>();
            services.AddSingleton<IDataRefreshService, DataRefreshService>();

            services.AddMongoRepositories(Configuration.GetConnectionString("mongo"))
                    // Registers all classes that implement IMongoRepository<> or IMongoEntityConfiguration<>
                    .FromAssembly(typeof(MongoRepository<>).Assembly);

            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DasonPokemon.Api v1"));
            } else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
              app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
              // To learn more about options for serving an Angular SPA from ASP.NET Core,
              // see https://go.microsoft.com/fwlink/?linkid=864501


              if (env.IsDevelopment())
              {
                spa.Options.SourcePath = "ClientApp";
                spa.UseAngularCliServer(npmScript: "start");
              }
            });

#pragma warning disable CS0618 // Type or member is obsolete
      //https://jira.mongodb.org/browse/CSHARP-3195
      BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}
