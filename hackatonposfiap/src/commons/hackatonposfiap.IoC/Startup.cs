using hackatonposfiap.domain.Interfaces;
using hackatonposfiap.infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using MySql.Data.MySqlClient;
using RabbitMQ.Client;
using hackatonposfiap.services;

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

    private static void ConfigureRabbitMq(IConfiguration configuration, IServiceCollection services)
    {
        string rabbitConnStr = configuration.GetConnectionString("ServerRabbitMQ");

        var rabbitMqFactory = new ConnectionFactory
        {
            Uri = new Uri(rabbitConnStr)
        };

        var rabbitMqConnection = rabbitMqFactory.CreateConnection();
        var rabbitMqChannel = rabbitMqConnection.CreateModel();

        var producerService = new ProducerRabbitMqService(configuration, rabbitMqChannel);

        services.AddSingleton<IProducerRabbitMqService, ProducerRabbitMqService>(sp =>
        {
            return producerService;
        });

        var connStr = configuration.GetConnectionString("ConnStr_MySql");
        services.AddScoped<IDbConnection>(provider => new MySqlConnection(connStr));
        IDbConnection dbConnection = new MySqlConnection(connStr);

        IGerenciadorImagemRepository imagemRepository = new GerenciadorImagemRepository(dbConnection);
        IGerenciadorVideoRepository videoRepository = new GerenciadorVideoRepository(dbConnection);

        IExtracaoImagensService extracaoImagensService = new ExtracaoImagensService(configuration, imagemRepository, videoRepository);

        services.AddSingleton<IConsumerRabbitMqService, ConsumerRabbitMqService>(sp =>
        {
            return new ConsumerRabbitMqService(configuration, rabbitMqChannel, extracaoImagensService);
        });
    }
}
