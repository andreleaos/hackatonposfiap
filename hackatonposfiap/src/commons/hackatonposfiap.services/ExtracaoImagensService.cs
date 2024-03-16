using FFMpegCore;
using hackatonposfiap.domain.Dtos;
using hackatonposfiap.domain.Entities;
using hackatonposfiap.domain.Interfaces;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace hackatonposfiap.services;
public class ExtracaoImagensService : IExtracaoImagensService
{
    private readonly IGerenciadorImagemRepository _imagemRepository;
    private readonly IConfiguration _configuration;
    private readonly IGerenciadorVideoRepository _gerenciadorRepository;

    public ExtracaoImagensService(IGerenciadorImagemRepository imagemRepository, IConfiguration configuration, IGerenciadorVideoRepository gerenciadorRepository)
    {
        _imagemRepository = imagemRepository;
        _configuration = configuration;
        _gerenciadorRepository = gerenciadorRepository;
    }


    public async Task Processar(GerenciadorVideoItemDto gerenciadorVideoDto)
    {
        var diretorioBase = CriarDiretorioInicial();
        var outputFolder = diretorioBase[0];
        string destinationZipFilePath = diretorioBase[1];

  
           int  videoId = await SaveInfoVideoAsync(gerenciadorVideoDto);

            Directory.CreateDirectory(outputFolder);

            var videoInfo = FFProbe.Analyse(gerenciadorVideoDto.CaminhoArquivo);
            var duration = videoInfo.Duration;

            var interval = TimeSpan.FromSeconds(Convert.ToDouble(gerenciadorVideoDto.Intervalo));

            for (var currentTime = TimeSpan.Zero; currentTime < duration; currentTime += interval)
            {
                var outputPath = Path.Combine(outputFolder, $"frame_at_{currentTime.TotalSeconds}.jpg");
                FFMpeg.Snapshot(gerenciadorVideoDto.CaminhoArquivo, outputPath, new Size(1920, 1080), currentTime);
            }

            SaveInfoImages(outputFolder, videoId);

            ZipFile.CreateFromDirectory(outputFolder, destinationZipFilePath);
        
    }

    //Mira para service App
    private async Task<int> SaveInfoVideoAsync(GerenciadorVideoItemDto video)
    {
        var newVideo = new GerenciadorVideoItem()
        {

            CaminhoArquivo = video.CaminhoArquivo,
            NomeArquivo = video.NomeArquivo,
            Intervalo = video.Intervalo,
            DtCriacao = DateTime.Now,
        };
        var retorno = _gerenciadorRepository.Create(newVideo);        

        var retornoBusca =  await _gerenciadorRepository.GetByName(newVideo.NomeArquivo);

        return retornoBusca.Id;
    }

    private void SaveInfoImages(string outputFolder, int videoId)
    {
        var listaImagens = Directory.GetFiles(outputFolder);

        foreach (var imagem in listaImagens)
        {
            var imagemItem = new GerenciadorImagemItem();

            imagemItem.CaminhoArquivo = outputFolder + @$"/{imagemItem}";
            imagemItem.NomeArquivo = imagem;
            imagemItem.IdVideo = videoId;
            imagemItem.DtCriacao = DateTime.Now;

            _imagemRepository.Create(imagemItem);
        }
    }

    private string[] CriarDiretorioInicial()
    {
        string[] directorys = new string[]
            {
            _configuration.GetSection("DiretorioArquivos")["DiretorioBaseImagens"],
            _configuration.GetSection("DiretorioArquivos")["DiretorioBaseOutPuts"]
        };

        foreach (var directory in directorys)
        {

            if (Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        return directorys;
    }
 }
