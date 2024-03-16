using hackatonposfiap.domain.Dtos;
using System.Net;
using System.Net.Http.Json;

namespace hackatonposfiap.integration.test;
public class GerenciadorImagem_POST:IClassFixture<HackaWebApplicationFactory>
{
    private readonly HackaWebApplicationFactory _factory;

    public GerenciadorImagem_POST(HackaWebApplicationFactory factory)
    {
        _factory = factory;
    }
 
    [Fact]
    public async Task CriarGerenciadorImagem_RetornaSucesso()
    {
        // Arrange
        var client = _factory.CreateClient();
        var gerenciadorImagem = new GerenciadorImagemDto
        {
            ArquivoZip = "ArquivoZip",
            Imagens = new List<GerenciadorImagemItemDto>
            {
                new GerenciadorImagemItemDto
                {
                    Id= 1,
                    CaminhoArquivo = "CaminhoArquivo",
                    NomeArquivo = "NomeArquivo"
                }
            },
            VideoId = 1,
            Video = new GerenciadorVideoItemDto
            {
                Intervalo = "Intervalo",
                NomeArquivo = "NomeArquivo",
                CaminhoVideo = "CaminhoVideo"

            }
        };

        // Act
        var response = await client.PostAsJsonAsync("/GerenciadorImagem", gerenciadorImagem);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
}
