using DesafioBemol.Filters;
using DesafioBemol.Repositories;
using DesafioBemol.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DesafioBemol
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configurações de injeção de dependência

            // Configurar o acesso ao Azure Cosmos DB
            services.AddSingleton<CosmosClient>(sp =>
            {
                string endpoint = Configuration["CosmosDB:Endpoint"];
                string key = Configuration["CosmosDB:Key"];

                return new CosmosClient(endpoint, key);
            });

            // Repositories
            services.AddScoped<IObjetoRepository, ObjetoRepository>();

            // Services
            services.AddScoped<IQueueSenderService, QueueSenderService>();

            // Filters
            services.AddScoped<AuthorizationFilter>();
            services.AddScoped<ExceptionFilter>();
            services.AddScoped<ActionFilter>();

            // Configuração do ASP.NET Core MVC
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(AuthorizationFilter));
                options.Filters.Add(typeof(ExceptionFilter));
                options.Filters.Add(typeof(ActionFilter));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}