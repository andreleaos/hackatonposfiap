using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace hackatonposfiap.integration.test;
public class GerenciadorImagem_GET : IClassFixture<HackaWebApplicationFactory>
{
    private readonly HackaWebApplicationFactory _factory;

    public GerenciadorImagem_GET(HackaWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ObterGerenciadorImagem_RetornaSucesso()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/GerenciadorImagem");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
