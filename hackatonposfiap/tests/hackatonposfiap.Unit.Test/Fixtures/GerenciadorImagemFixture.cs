using Bogus;
using hackatonposfiap.domain.Entities;

namespace hackatonposfiap.unit.test.Fixtures;
public class GerenciadorImagemFixture
{
    private readonly Faker _faker;
    public GerenciadorImagemFixture()
    {
        _faker = new Faker();
    }
    public GerenciadorImagem CreateGerenciadorImagemValida()
    {
        return new GerenciadorImagem
        {
            Id = _faker.Random.Int(),
            IsValid = true,
            Imagens = CreateGerenciadorImagemItemValida(),
            ArquivoZip = _faker.Random.String(),
            VideoId = _faker.Random.Int(),

        };
    }

    public GerenciadorImagem CreateGerenciadorImagemComListadeGerenciadorImagemItemNula()
    {
        return new GerenciadorImagem
        {
            Id = _faker.Random.Int(),
            IsValid = false,
            Imagens = null,
            ArquivoZip = _faker.Random.String(),
            VideoId = _faker.Random.Int(),

        };
    }
  
    public List<GerenciadorImagemItem> CreateGerenciadorImagemItemValida()
    {
        return new List<GerenciadorImagemItem>
        {
            new GerenciadorImagemItem
            {
                Id = _faker.Random.Int(),                
                CaminhoArquivo = _faker.Random.String(),
                NomeArquivo = _faker.Random.String()
            }
        };
    }

    public List<GerenciadorImagemItem> CreateGerenciadorImagemItemInvalida()
    {
        return new List<GerenciadorImagemItem>
        {
            null
        };
    }
}
