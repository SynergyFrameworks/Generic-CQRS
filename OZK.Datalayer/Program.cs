using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace OZK.Datalayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
              Host.CreateDefaultBuilder(args)
                  .ConfigureServices((hostBuilderContext, serviceCollection) => new Startup(hostBuilderContext.Configuration).ConfigureServices(serviceCollection));


    }
}
