namespace hackatonposfiap.consumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var factory = new ConnectionFactory()
                    {
                        HostName = "localhost",
                        UserName = "guest",
                        Password = "guest"
                    };

                    using (var connection = factory.CreateConnection())
                    {
                        using (var channel = connection.CreateModel())
                        {
                            channel.QueueDeclare(
                                queue: "fila",
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null
                                );

                            var consumer = new EventingBasicConsumer(channel);

                            consumer.Received += (sender, args) =>
                            {
                                var body = args.Body.ToArray();
                                var message = Encoding.UTF8.GetString(body);

                                var pedido = JsonSerializer.Deserialize<Pedido>(message);

                                Console.WriteLine(pedido?.ToString());
                            };

                            channel.BasicConsume(
                                queue: "fila",
                                autoAck: true,
                                consumer: consumer
                                );
                        }

                    }

                    if (_logger.IsEnabled(LogLevel.Information))
                    {
                        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    }
                    await Task.Delay(5000, stoppingToken);
                }
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //await Task.Delay(1000, stoppingToken);
            }
        }
    }
}