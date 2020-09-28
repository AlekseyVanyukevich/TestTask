using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using TestTask.Domain.Models;
using TestTask.Domain.Repositories;

namespace TestTask.Infrastructure.Repositories
{
    public class BooksRepository : Repository<Book>, IBooksRepository
    {
        private new readonly LibraryDbContext Context;
        public BooksRepository(LibraryDbContext context) : base(context)
        {
            Context = context;
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthorId(int authorId)
        {
            return await Context.BookAuthors
                .Include(ba => ba.Book)
                .Include(ba => ba.Author)
                .Where(ba => ba.AuthorId == authorId)
                .Select(ba => ba.Book)
                .ToListAsync();
        }

        public async Task Update(Book book, IEnumerable<Author> authors)
        {
            if (!authors.Any())
            {
                throw new ArgumentException("Must be at least one author");
            }

            var bookAuthors = Context.BookAuthors.Where(ba => ba.BookId == book.Id);
            Context.BookAuthors.RemoveRange(bookAuthors);
            Context.Update(book);
            await Context.SaveChangesAsync();
            foreach (var author in authors)
            {
                var existedAuthor = await Context.Authors
                    .FirstOrDefaultAsync(a => a.Surname == author.Surname);

                if (existedAuthor == null)
                {
                    await Context.Authors.AddAsync(author);
                    await Context.SaveChangesAsync();
                }

                var entity = existedAuthor ?? author;

                await Context.BookAuthors
                    .AddAsync(new BookAuthor {BookId = book.Id, AuthorId = entity.Id});
                await Context.SaveChangesAsync();
            }
        }

        public new async Task<Book> Get(int id)
        {
            return await Context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Book)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
        
        public new async Task<IEnumerable<Book>> GetAll()
        {
            return await Context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Book)
                .ToListAsync();
        }
    }
}