using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Models;
using TestTask.Domain.Repositories;

namespace TestTask.Infrastructure.Repositories
{
    public class AuthorsRepository : Repository<Author>, IAuthorsRepository
    {
        private new readonly LibraryDbContext Context;
        public AuthorsRepository(LibraryDbContext context) : base(context)
        {
            Context = context;
        }

        public async Task<IEnumerable<Author>> GetBookAuthors(int bookId)
        {
            return await Context.BookAuthors
                .Include(ba => ba.Author)
                .Include(ba => ba.Book)
                .Where(ba => ba.BookId == bookId)
                .Select(ba => ba.Author)
                .ToListAsync();
        }

        public new async Task<Author> Get(int id)
        {
            return await Context.Authors
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Book)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public new async Task<IEnumerable<Author>> GetAll()
        {
            return await Context.Authors
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Book)
                .ToListAsync();
        }

        public new async Task Add(Author entity)
        {
            var author = await Context.Authors.FirstOrDefaultAsync(a => a.Surname == entity.Surname);
            if (author != null)
            {
                throw new ArgumentException("Author with such surname is already exists");
            }

            await Context.AddAsync(entity);
        }
    }
}