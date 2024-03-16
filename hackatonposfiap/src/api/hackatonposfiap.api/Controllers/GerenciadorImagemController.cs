using hackatonposfiap.domain.Dtos;
using hackatonposfiap.domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace hackatonposfiap.api.Controllers;
[ApiController]
[Route("[controller]")]
public class GerenciadorImagemController : ControllerBase
{
   
    [HttpPost]
    public IActionResult CadastrarGerenciadorImagem([FromBody] GerenciadorImagemDto dto)
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
