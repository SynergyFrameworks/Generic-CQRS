using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OZK.Datalayer.Context;

namespace OZK.Datalayer
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
            var sqlConnection = Configuration.GetConnectionString("OZKDbConnection");

            services.AddDbContext<OZKDbContext>(options =>
                options.UseSqlServer(sqlConnection));
        }















    }
}
