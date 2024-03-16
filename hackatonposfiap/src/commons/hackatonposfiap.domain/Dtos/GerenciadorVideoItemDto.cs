using Microsoft.AspNetCore.Http;

namespace hackatonposfiap.domain.Dtos;
public class GerenciadorVideoItemDto
{
    public string CaminhoVideo { get; set; }
    public string NomeArquivo { get; set; }
    public string Intervalo { get; set; }

    public IFormFile? Arquivo { get; set; }
}