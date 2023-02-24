using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderExplorerDAL.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected DbContext context;
        protected DbSet<T> table;
        public GenericRepository(DbContext context)
        {
            this.context = context;
            table = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await table.AddAsync(entity);
            await context.SaveChangesAsync();
        }
        public async Task AddRangeAsync(IEnumerable<T> entiies)
        {
            try
            {
                await table.AddRangeAsync(entiies);
            }
            catch (Exception e)
            {

            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return table.AsNoTracking();
        }
        public async Task<T> GetAsync(int id)
        {
            var entity = await table.FindAsync(id);
            context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task RemoveAsync(T entity)
        {
            await Task.Run(() => table.Remove(entity));
        }
        public async Task RemoveAllAsync()
        {
            await Task.Run(() => table.RemoveRange(table.ToList()));
        }
        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            await Task.Run(() => table.Update(entity));
        }
    }
}
