using Microsoft.Extensions.Configuration;

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
