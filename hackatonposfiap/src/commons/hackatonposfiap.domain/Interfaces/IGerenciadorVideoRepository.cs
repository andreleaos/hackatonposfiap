using hackatonposfiap.domain.Entities;

namespace hackatonposfiap.domain.Interfaces;

public interface IGerenciadorVideoRepository : IBaseRepoService<GerenciadorVideoItem, int>
{
    Task<GerenciadorVideoItem> GetByName(string nomeArquivo);
    Task Update(GerenciadorVideoItem entity);
}
