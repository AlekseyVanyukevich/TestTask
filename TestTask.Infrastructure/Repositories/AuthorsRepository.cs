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
        public AuthorsRepository(LibraryDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Author>> GetBookAuthors(int bookId)
        {
            return await _context.BookAuthors
                .Include(ba => ba.Author)
                .Include(ba => ba.Book)
                .Where(ba => ba.BookId == bookId)
                .Select(ba => ba.Author)
                .ToListAsync();
        }
    }
}