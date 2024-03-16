using hackatonposfiap.domain.Dtos;
using hackatonposfiap.domain.Entities;
using hackatonposfiap.domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace hackatonposfiap.api.Controllers;
[ApiController]
[Route("[controller]")]
public class GerenciadorImagemController : ControllerBase
{
    private readonly IProducerRabbitMqService _producerRabbitMqService;

    public GerenciadorImagemController(IProducerRabbitMqService producerRabbitMqService)
    {
        _producerRabbitMqService = producerRabbitMqService;
    }

    [HttpPost]
    public async Task<IActionResult> CadastrarGerenciadorImagem([FromBody] GerenciadorImagemDto dto)
    {
        var gerenciadorImagem = new GerenciadorImagem
        {

            ArquivoZip = dto.ArquivoZip,
            Imagens = dto.Imagens.Select(x => new GerenciadorImagemItem
            {
                CaminhoArquivo = x.CaminhoArquivo,
                NomeArquivo = x.NomeArquivo
            }).ToList()
        };

        var message = JsonConvert.SerializeObject(gerenciadorImagem);
        await _producerRabbitMqService.Publish(message, typeof(GerenciadorImagem));
        return Ok(gerenciadorImagem);
    }

 

    [HttpGet]
    public IActionResult ListarGerenciadoresImagem()
    {
        var gerenciadoresImagem = new List<GerenciadorImagem>
        {
            new GerenciadorImagem
            {
                ArquivoZip = "arquivo.zip",
                Imagens = new List<GerenciadorImagemItem>
                {
                    new GerenciadorImagemItem
                    {
                        CaminhoArquivo = "caminho",
                        NomeArquivo = "nome"
                    }
                }
            }
        };

        return Ok(gerenciadoresImagem);
    }
   
    
}
