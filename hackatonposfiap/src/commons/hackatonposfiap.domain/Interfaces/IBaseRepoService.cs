namespace hackatonposfiap.domain.Interfaces;
public interface IBaseRepoService<T, Y> where T : class
{
    Task Create(T entity);
    Task<List<T>> GetAll();
    Task<T> GetById(Y id);
}
