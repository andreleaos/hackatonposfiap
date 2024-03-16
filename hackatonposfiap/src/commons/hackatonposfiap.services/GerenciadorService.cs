using hackatonposfiap.domain.Dtos;
using hackatonposfiap.domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace hackatonposfiap.services
{
    public class GerenciadorService : IGerenciadorService
    {
        #region Atributos

        private HttpClient _httpClient = null;
        private string _url_base_address = string.Empty;

        #endregion

        public GerenciadorService(IConfiguration configuration)
        {
            _url_base_address = configuration.GetSection("API_GERENCIADOR").Value;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_url_base_address);
        }


        public async Task Create(GerenciadorImagemDto entity)
        {
            //try
            //{
            entity.Video.CaminhoArquivo = GetUrlImagemServidor(entity.Video.Arquivo);

            var newsContent = ObterConteudo(entity.Video);

            using (var response = await _httpClient.PostAsync(_url_base_address, newsContent))
            {
                response.EnsureSuccessStatusCode();
            }
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        protected HttpContent ObterConteudo(object dado)
        {
            //try
            //{
            if (dado == null)
                throw new Exception("Objeto Informado Nulo, não é possível realizar a conversão de tipo de dados");

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(dado);
            byte[] data = Encoding.UTF8.GetBytes(json);
            ByteArrayContent byteContent = new ByteArrayContent(data);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpContent content = byteContent;
            return content;
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        private string GetUrlImagemServidor(IFormFile? arquivo)
        {
            string urlImagem = string.Empty;
            //try
            //{
            string path = Path.Combine(Directory.GetCurrentDirectory(), "ArquivosRecebidos");

            if (arquivo != null)
            {
                FileInfo fileInfo = new FileInfo(arquivo.FileName);
                string fileName = arquivo.FileName;

                string fileNameWithPath = string.Empty;

                fileNameWithPath = Path.Combine(path, fileName);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                if (!File.Exists(fileNameWithPath))
                {
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        arquivo.CopyTo(stream);
                    }
                }

                urlImagem = fileNameWithPath;
            }
            //}
            //catch (Exception)
            //{
            //    throw;
            //}

            return urlImagem;
        }

        public async Task<List<GerenciadorImagemDto>> GetAll()
        {
            string endpoint = $"{_url_base_address}";
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<List<GerenciadorImagemDto>>(content);

            return result;
        }

        public async Task<GerenciadorImagemDto> GetById(int id)
        {
            string endpoint = $"{_url_base_address}/{id}";

            GerenciadorImagemDto consulta = null;

            using (var response = await _httpClient.GetAsync(endpoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var consultaJson = await response.Content.ReadAsStringAsync();
                    consulta = JsonConvert.DeserializeObject<GerenciadorImagemDto>(consultaJson);
                }
            }

            return consulta;
        }
    }
}
