using Microsoft.AspNetCore.Http;

namespace hackatonposfiap.domain.Dtos;
public class GerenciadorVideoItemDto
{
    public string CaminhoArquivo { get; set; }
    public string NomeArquivo { get; set; }
    public string Intervalo { get; set; }
    public DateTime DtCriacao { get; set; }

    public IFormFile? Arquivo { get; set; }
}