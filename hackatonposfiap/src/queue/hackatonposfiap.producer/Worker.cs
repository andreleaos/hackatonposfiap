using hackatonposfiap.domain.Interfaces;
using Newtonsoft.Json;

namespace hackatonposfiap.producer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IProducerRabbitMqService _producerRabbitMqService;
        public Worker(
            ILogger<Worker> logger,
            IProducerRabbitMqService producerRabbitMqService)
        {
            _logger = logger;
            _producerRabbitMqService = producerRabbitMqService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        private async Task Process()
        {
            for (int i = 1; i <= 10; i++)
            {
                Jogo jogo = _generatorDataService.Generate();
                var message = JsonConvert.SerializeObject(jogo);

                if (GlobalParameters.ENABLE_RABBIT_MQ_MESSAGE_SERVICE)
                    await _producerRabbitMqService.Publish(message, jogo.GetType(), publishDlq: false);
                else if (GlobalParameters.ENABLE_MASS_TRANSIT_MESSAGE_SERVICE)
                    await _producerMassTransitService.Publish(message, jogo.GetType(), publishDlq: false);
                else if (GlobalParameters.ENABLE_AZURE_SERVICE_BUS_MQ_MESSAGE_SERVICE)
                    await _producerAzureServiceBusService.Publish(message, jogo.GetType(), publishDlq: false);

                var logMessage = $"Jogo [{jogo.Nome.ToUpper()}] publicado na fila\n";
                _logger.LogInformation(logMessage);
            }
        }
    }
}