using hackatonposfiap.domain.Dtos;
using hackatonposfiap.domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace hackatonposfiap.services;
public class ExtracaoImagensService : IExtracaoImagensService
{
    private readonly IConfiguration _configuration;
    public ExtracaoImagensService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task Processar(GerenciadorVideoDto gerenciadorVideoDto)
    {
        var diretorioBase = CriarDiretorioInicial();
    }

    private string CriarDiretorioInicial()
    {
        string directory = _configuration.GetSection("DiretorioArquivos")["DiretorioBaseImagens"];
        bool existsDirectory = Directory.Exists(directory);

        if (!existsDirectory)
            Directory.CreateDirectory(directory);

        return directory;
    }
}
