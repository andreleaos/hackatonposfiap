using hackatonposfiap.domain.Dtos;

namespace hackatonposfiap.domain.Interfaces;
public interface IExtracaoImagensService
{
    Task Processar(GerenciadorVideoDto gerenciadorVideoDto);
}
