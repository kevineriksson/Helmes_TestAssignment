using Microsoft.AspNetCore.Mvc;

namespace PostOfficeAPI.Contracts.Repos
{
    public interface IRepo<T> where T : class
    {
        public Task<IEnumerable<T>> Get();
        public Task<T> CreateAsync(T entity);
        public Task<bool> UpdateAsync(string id, T entity);
        public Task<bool> DeleteAsync(string id);
    }
}
