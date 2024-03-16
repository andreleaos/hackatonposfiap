namespace hackatonposfiap.domain.Interfaces
{
    public interface IGenericApiService<T, Y>
    {
        Task Create(T entity);
        Task<T> GetById(Y id);
        Task<List<T>> GetAll();
    }
}
