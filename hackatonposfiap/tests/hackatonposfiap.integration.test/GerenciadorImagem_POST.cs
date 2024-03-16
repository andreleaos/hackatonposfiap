using hackatonposfiap.domain.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace hackatonposfiap.integration.test;
public class GerenciadorImagem_POST:IClassFixture<HackaWebApplicationFactory>
{
    private readonly HackaWebApplicationFactory _factory;

    public GerenciadorImagem_POST(HackaWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task CadastrarGerenciadorImagem_RetornaSucesso()
    {
        // Arrange
        var client = _factory.CreateClient();
        var gerenciadorImagemDto = new GerenciadorImagemDto
        {
            ArquivoZip = "arquivo.zip",
            Imagens = new List<GerenciadorImagemItemDto>
            {
                new GerenciadorImagemItemDto
                {
                    CaminhoArquivo = "caminho",
                    NomeArquivo = "nome"
                }
            }
        };

        // Act
        var response = await client.PostAsJsonAsync("GerenciadorImagem/", gerenciadorImagemDto);

        // Assert
        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
    }
}
