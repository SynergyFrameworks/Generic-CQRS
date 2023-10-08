using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using OZK.Infrastructure.Sorting;
using OZK.Datalayer.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OZK.Services;
using OZKService.Contracts;
using OZKService.Models;
using Microsoft.OpenApi.Models;
using OZK.Domain.CQRS.Contracts;
using OZK.Domain.CQRS.Handlers;
using OZK.Domain.CQRS.Models;
using OZK.Datalayer.Context;


namespace OZK
{
    public class Startup
    {

        private readonly IConfigurationRoot _configuration;

        private readonly IWebHostEnvironment _currentEnvironment;

        private const string _ENV_DOCKER = "docker";
        public Startup(IWebHostEnvironment env)
        {
            _currentEnvironment = env;

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", reloadOnChange: true, optional: true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

         
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<OZKDbContext>(options => options
                      .UseSqlServer(_configuration.GetConnectionString(ConnectionStringKeys.OZKApp)));


            services.AddScoped<ICrudHandler<OZKDbContext>, CrudHandler>()
                  .AddScoped<IQueryHandler<OZKDbContext>, QueryHandler>()
                  .AddScoped<IService<BankAccount, DefaultSearch<OZKBankAccountSearchResult>>, BankAccountService>()
                  .AddScoped<IService<BankUser, DefaultSearch<OZKBankUserSearchResult>>, BankUserService>()
                  .AddScoped<ITransfer<OZKBankAccountInfo>,TransferService >()
                  .AddScoped<ISortingOption, SortOption>()
                  .AddScoped<ICommandHandler, CommandHandler>();



            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OZK", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OZK v1"));
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
