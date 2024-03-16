using hackatonposfiap.domain.Dtos;

namespace hackatonposfiap.domain.Interfaces;
public interface IExtracaoImagensService
{
    Task Processar(GerenciadorVideoItemDto gerenciadorVideoDto);
}
