using hackatonposfiap.domain.Interfaces;
using hackatonposfiap.infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using MySql.Data.MySqlClient;

namespace hackatonposfiap.IoC;
public static class Startup
{
    public static void Configure(IConfiguration configuration, IServiceCollection services)
    {
        ConfigureDatabase(configuration, services);

        services.AddScoped<IGerenciadorVideoRepository, GerenciadorVideoRepository>();
        services.AddScoped<IGerenciadorImagemRepository, GerenciadorImagemRepository>();

    }

    private static void ConfigureDatabase(IConfiguration configuration, IServiceCollection services)
    {
        string connStrMySql = configuration.GetConnectionString("ConnStr_MySql");

        services.AddScoped<IDbConnection>(provider => new MySqlConnection(connStrMySql));
    }

}
