namespace MongoCRUD
{
    public interface IRepository<T>
        where T : IEntity
    {
        public Task<List<T>> GetAllAsync();
        public Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter);
        public Task<T> GetAsync(Guid id);
        public Task<T> GetAsync(Expression<Func<T, bool>> filter);
        public Task<bool> PostAsync(T entity);
        public Task<bool> PostAllAsync(List<T> entity);
        public Task DeleteAsync(Guid id);

        public Task UpdateAsync(T entity);
    }
}
