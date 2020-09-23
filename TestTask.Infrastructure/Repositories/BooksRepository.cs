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
        public BooksRepository(LibraryDbContext context) : base(context)
        {
        }

        public async Task Add(Book book, IEnumerable<Author> authors)
        {
            if (authors.Count() == 0)
            {
                throw new ArgumentException("Must be at least one author");
            }
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            foreach (var author in authors)
            {
                var existedAuthor = await _context.Authors
                    .FirstOrDefaultAsync(a => a.Surname == author.Surname);

                if (existedAuthor == null)
                {
                    await _context.Authors.AddAsync(author);
                    await _context.SaveChangesAsync();
                }

                var entity = existedAuthor ?? author;

                await _context.BookAuthors
                    .AddAsync(new BookAuthor {BookId = book.Id, AuthorId = entity.Id});
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthorId(int authorId)
        {
            return await _context.BookAuthors
                .Include(ba => ba.Book)
                .Include(ba => ba.Author)
                .Where(ba => ba.AuthorId == authorId)
                .Select(ba => ba.Book)
                .ToListAsync();
        }

        public async Task Update(Book book, IEnumerable<Author> authors)
        {
            if (authors.Count() == 0)
            {
                throw new ArgumentException("Must be at least one author");
            }

            var bookAuthors = _context.BookAuthors.Where(ba => ba.BookId == book.Id);
            _context.BookAuthors.RemoveRange(bookAuthors);
            await _context.SaveChangesAsync();
            foreach (var author in authors)
            {
                var existedAuthor = await _context.Authors
                    .FirstOrDefaultAsync(a => a.Surname == author.Surname);

                if (existedAuthor == null)
                {
                    await _context.Authors.AddAsync(author);
                    await _context.SaveChangesAsync();
                }

                var entity = existedAuthor ?? author;

                await _context.BookAuthors
                    .AddAsync(new BookAuthor {BookId = book.Id, AuthorId = entity.Id});
                await _context.SaveChangesAsync();
            }
        }
    }
}