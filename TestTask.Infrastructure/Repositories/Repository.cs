using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Repositories;

namespace TestTask.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected internal readonly DbContext Context;
        public Repository(DbContext context)
        {
            Context = context;
        }
        public async Task<T> Get(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public async Task Add(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            Context.Set<T>().Update(entity);
        }
    }
}