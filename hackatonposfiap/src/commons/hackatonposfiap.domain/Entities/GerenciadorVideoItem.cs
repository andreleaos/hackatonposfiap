namespace hackatonposfiap.domain.Entities;
public class GerenciadorVideoItem
{
    public int Id { get; set; }
    public string CaminhoArquivo { get; set; }
    public string NomeArquivo { get; set; }
    public string Intervalo { get; set; }
    public DateTime DtCriacao { get; set; }
}
