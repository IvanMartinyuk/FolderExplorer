using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderExplorerDAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        public Task<T> GetAsync(int id);
        public Task<IEnumerable<T>> GetAll();
        public Task UpdateAsync(T entity);
        public Task AddAsync(T entity);
        public Task AddRangeAsync(IEnumerable<T> entiies);
        public Task RemoveAsync(T entity);
        public Task RemoveAllAsync();
        public Task SaveChanges();
    }
}
