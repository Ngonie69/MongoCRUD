namespace MongoCRUD
{
    public class DbService<T> : IRepository<T>
        where T : IEntity
    {
        private readonly IMongoCollection<T> collection;

        private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;

        public DbService(IMongoDatabase mongoClient, string collectionName)
        {
            collection = mongoClient.GetCollection<T>(collectionName);
        }

        public async Task DeleteAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.id, id);
            await collection.DeleteOneAsync(filter);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await collection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            return await collection.Find(filter).ToListAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.id, id);
            return await collection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await collection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<bool> PostAllAsync(List<T> entity)
        {
            await collection.InsertManyAsync(entity);
            return true;
        }

        public async Task<bool> PostAsync(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentException(nameof(entity));
            }
            else
            {
                await collection.InsertOneAsync(entity);
                return true;
            }
            ;
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentException(nameof(entity));
            }
            else
            {
                var filter = filterBuilder.Eq(item => item.id, entity.id);
                await collection.ReplaceOneAsync(filter, entity);
            }
        }
    }
}