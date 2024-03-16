using Bogus;
using hackatonposfiap.unit.test.Fixtures;

namespace hackatonposfiap.unit.test.Domain;

[Collection(nameof(GerenciadorImagemCollectionFixture))]
public class GerenciadorImagemTest
{
    private readonly Faker _faker;
    private readonly GerenciadorImagemFixture _gerenciadorImagemFixture;

    public GerenciadorImagemTest( GerenciadorImagemFixture gerenciadorImagemFixture)
    {
        _faker = new Faker();
        _gerenciadorImagemFixture = gerenciadorImagemFixture;
    }


    [Fact]
    public void DeveCriarGerenciadorImagemValida()
    {
        //arrange     
      
        var gerenciadorImagem = _gerenciadorImagemFixture.CreateGerenciadorImagemValida();
        //act
        var result = gerenciadorImagem.Validar();
        //assert
        Assert.True(result);
    }

 
    [Fact]
    public void DeveCriarGerenciadorImagemNula()
    {
        //arrange  
        var gerenciadorImagem = _gerenciadorImagemFixture.CreateGerenciadorImagemComListadeGerenciadorImagemItemNula();

        //act
          var result = gerenciadorImagem.Validar();
        //assert
        Assert.False(result);
    }
}
