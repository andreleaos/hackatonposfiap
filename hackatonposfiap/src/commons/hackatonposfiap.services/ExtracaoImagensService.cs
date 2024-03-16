using FFMpegCore;
using hackatonposfiap.domain.Dtos;
using hackatonposfiap.domain.Entities;
using hackatonposfiap.domain.Interfaces;

using System.Drawing;
using System.IO.Compression;
using System.Linq;

using Microsoft.Extensions.Configuration;

namespace hackatonposfiap.services
{
    public class ExtracaoImagensService : IExtracaoImagensService
    {
        private readonly IGerenciadorImagemRepository _imagemRepository;
        private readonly IConfiguration _configuration;
        public ExtracaoImagensService(IGerenciadorImagemRepository imagemRepository, IConfiguration configuration)
        {
            _imagemRepository = imagemRepository;
            _configuration = configuration;
        }
        public Task Processar(GerenciadorVideoDto gerenciadorVideoDto)
        {
            var diretorioBase = CriarDiretorioInicial();
            var outputFolder = diretorioBase[0];
            string destinationZipFilePath = diretorioBase[1];


            foreach (GerenciadorVideoItemDto video in gerenciadorVideoDto.Arquivos)
            {
                Directory.CreateDirectory(outputFolder);

                var videoInfo = FFProbe.Analyse(video.CaminhoArquivo);
                var duration = videoInfo.Duration;

                var interval = TimeSpan.FromSeconds(Convert.ToDouble(video.Intervalo));

                for (var currentTime = TimeSpan.Zero; currentTime < duration; currentTime += interval)
                {
                    var outputPath = Path.Combine(outputFolder, $"frame_at_{currentTime.TotalSeconds}.jpg");
                    FFMpeg.Snapshot(video.CaminhoArquivo, outputPath, new Size(1920, 1080), currentTime);
                }

                var listaImagens = Directory.GetFiles(outputFolder);

                foreach (var imagem in listaImagens)
                {
                    var imagemItem = new GerenciadorImagemItem();

                    imagemItem.CaminhoArquivo = outputFolder + @$"/{imagemItem}";
                    imagemItem.NomeArquivo = imagem;

                    _imagemRepository.Create(imagemItem);
                }


                ZipFile.CreateFromDirectory(outputFolder, destinationZipFilePath);

            }

            return Task.CompletedTask;
        }

        private string[] CriarDiretorioInicial()
        {
            string[] directorys = new string[]
                {
                _configuration.GetSection("DiretorioArquivos")["DiretorioBaseImagens"],
                _configuration.GetSection("DiretorioArquivos")["DiretorioBaseOutPuts"],
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
}
