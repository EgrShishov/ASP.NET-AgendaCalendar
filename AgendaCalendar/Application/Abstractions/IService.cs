namespace AgendaCalendar.Application.Abstractions
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task AddAsync(T item);
        Task<T> UpdateAsync(T item);
    }
}