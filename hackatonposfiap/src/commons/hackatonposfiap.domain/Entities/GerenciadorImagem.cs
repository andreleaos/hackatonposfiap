using hackatonposfiap.domain.Dtos;

namespace hackatonposfiap.domain.Entities;
public class GerenciadorImagem
{
    public int Id { get; set; }
    public string ArquivoZip { get; set; }
    public List<GerenciadorImagemItem> Imagens { get; set; }
    public int VideoId { get; set; }
    public GerenciadorVideoItem Video { get; set; }
}
