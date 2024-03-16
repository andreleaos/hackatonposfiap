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


    public Task Processar(GerenciadorVideoDto gerenciadorVideoDto)
    {
        var diretorioBase = CriarDiretorioInicial();
        var outputFolder = diretorioBase[0];
        string destinationZipFilePath = diretorioBase[1];

        foreach (GerenciadorVideoItemDto video in gerenciadorVideoDto.Arquivos)
        {
           int  videoId = SaveInfoVideo(video);

            Directory.CreateDirectory(outputFolder);

            var videoInfo = FFProbe.Analyse(video.CaminhoArquivo);
            var duration = videoInfo.Duration;

            var interval = TimeSpan.FromSeconds(Convert.ToDouble(video.Intervalo));

            for (var currentTime = TimeSpan.Zero; currentTime < duration; currentTime += interval)
            {
                var outputPath = Path.Combine(outputFolder, $"frame_at_{currentTime.TotalSeconds}.jpg");
                FFMpeg.Snapshot(video.CaminhoArquivo, outputPath, new Size(1920, 1080), currentTime);
            }

            SaveInfoImages(outputFolder);

            ZipFile.CreateFromDirectory(outputFolder, destinationZipFilePath);
        }

        return Task.CompletedTask;
    }

    private int SaveInfoVideo(GerenciadorVideoItemDto video)
    {
        var newVideo = new GerenciadorVideoItem()
        {

            CaminhoArquivo = video.CaminhoArquivo,
            NomeArquivo = video.NomeArquivo,
            Intervalo = video.Intervalo
        };
        var retorno = _gerenciadorRepository.Create(newVideo);        

        return _gerenciadorRepository.GetByName(newVideo.NomeArquivo).Id;
    }

    private void SaveInfoImages(string outputFolder)
    {
        var listaImagens = Directory.GetFiles(outputFolder);

        foreach (var imagem in listaImagens)
        {
            var imagemItem = new GerenciadorImagemItem();

            imagemItem.CaminhoArquivo = outputFolder + @$"/{imagemItem}";
            imagemItem.NomeArquivo = imagem;

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
