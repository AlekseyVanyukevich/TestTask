using System;
using System.Threading.Tasks;
using TestTask.Domain.Repositories;
using TestTask.Infrastructure.Repositories;

namespace TestTask.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _context;
        
        public IBooksRepository Books { get; }
        public IAuthorsRepository Authors { get; }

        public UnitOfWork(LibraryDbContext context)
        {
            _context = context;
            Books = new BooksRepository(_context);
            Authors = new AuthorsRepository(_context);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
