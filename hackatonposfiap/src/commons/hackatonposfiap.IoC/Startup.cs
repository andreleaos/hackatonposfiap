using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace hackatonposfiap.IoC;
public static class Startup
{
    public static void Configure(IConfiguration configuration, IServiceCollection services)
    {
        ConfigureDatabase(configuration, services);
    }

    private static void ConfigureDatabase(IConfiguration configuration, IServiceCollection services)
    {

    }

}
