namespace hackatonposfiap.domain.Dtos;
public class GerenciadorImagemDto
{
    public int Id { get; set; }
    public string ArquivoZip { get; set; }
    public List<GerenciadorImagemItemDto> Imagens { get; set; }
    public int VideoId { get; set; }
    public GerenciadorVideoItemDto Video { get; set; }
}
