using hackatonposfiap.domain.Dtos;
using hackatonposfiap.domain.Entities;
using hackatonposfiap.domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace hackatonposfiap.services;
public class ConsumerRabbitMqService
{
    private readonly IModel _channel;
    private readonly IExtracaoImagensService _extracaoImagensService;

    private static string _queuename = null;

    private GerenciadorVideoItemDto _currentObject;
    private bool _processedSuccessfully = false;

    public ConsumerRabbitMqService(
      IConfiguration configuration,
      IModel channel,
      IExtracaoImagensService extracaoImagensService)
    {
        _channel = channel;
        _queuename = configuration["RabbitMqConfig:Queue"];
        _extracaoImagensService = extracaoImagensService;
    }

    public ObjectMessageProcessedInfo Consume(bool consumeDlq = false)
    {
        EventingBasicConsumer consumer = new EventingBasicConsumer(_channel);

        string queuename = _queuename;
        consumer.Received += Consumer_Message;
        _channel.BasicConsume(queuename, false, consumer);

        return new ObjectMessageProcessedInfo
        {
            ProcessedSuccessfully = _processedSuccessfully
        };
    }
    private void Consumer_Message(object? sender, BasicDeliverEventArgs e)
    {
        bool processouComSucesso = false;
        string messageContent = Encoding.UTF8.GetString(e.Body.ToArray());
        GerenciadorVideoItemDto dto = null;

        if (!string.IsNullOrEmpty(messageContent))
        {
            Console.WriteLine($"Conteudo da mensagem: {messageContent}");
            try
            {
                dto = JsonConvert.DeserializeObject<GerenciadorVideoItemDto>(messageContent);
                _currentObject = dto ?? null;

                if (dto != null && !string.IsNullOrEmpty(dto.NomeArquivo))
                {
                    _extracaoImagensService.Processar(dto);
                    processouComSucesso = true;
                    _processedSuccessfully = true;
                    Console.WriteLine($"Video processado com sucesso - [{dto.NomeArquivo.ToUpper()}]\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Falha no tratamento da mensagem] - {ex.Message}");
            }
            finally
            {
                if (processouComSucesso)
                    _channel.BasicAck(e.DeliveryTag, false);
                else
                    _channel.BasicNack(e.DeliveryTag, false, false);
            }
        }
    }
}
