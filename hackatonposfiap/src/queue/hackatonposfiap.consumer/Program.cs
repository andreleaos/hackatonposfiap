using hackatonposfiap.IoC;

namespace hackatonposfiap.consumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                  .Build();

            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddSingleton(configuration);
                    services.AddHostedService<Worker>();
                    Startup.Configure(configuration, services, enableRabbitMq: true);
                })
                .Build();

            host.Run();
        }
    }
}