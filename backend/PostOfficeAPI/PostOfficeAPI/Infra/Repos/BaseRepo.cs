

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostOfficeAPI.ApplicationCore.Contracts.Entities;
using PostOfficeAPI.ApplicationCore.Contracts.Repos;

namespace PostOfficeAPI.Infra.Repos
{
    public abstract partial class BaseRepo<T> : IRepo<T> where T : class, IEntity
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public BaseRepo(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> Get()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<T> CreateAsync(T entity)
        {
            _dbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<bool> UpdateAsync(string id, T entity)
        {
            if (id != entity.Id)
            {
                return false;
            }

            _dbContext.Entry(entity).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"No entity found with ID {id}.");
            }
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
