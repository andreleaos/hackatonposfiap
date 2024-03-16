using hackatonposfiap.domain.Dtos;

namespace hackatonposfiap.domain.Entities;
public class GerenciadorImagem
{
    public int Id { get; set; }
    public string ArquivoZip { get; set; }
    public List<GerenciadorImagemItem> Imagens { get; set; }
    public int VideoId { get; set; }
    public GerenciadorVideoItem Video { get; set; }

    public bool IsValid { get; set; }
    public bool Validar()
    {
        //validar se o arquivo zip é nulo
        if (string.IsNullOrEmpty(ArquivoZip))
        {
            IsValid = false;
    
        }
        //validar se a lista de imagens é nula 
        if (Imagens == null)
        {
            IsValid = false;
            return IsValid;

        }
        //validar se a lista de imagens é vazia
        if (Imagens.Count == 0)
        {
            IsValid = false;
        
        }
        //validar se a lista de imagens contém algum item nulo
        if (Imagens.Any(x => x == null))
        {
            IsValid = false;
            
        }

        return IsValid;
    }
}
