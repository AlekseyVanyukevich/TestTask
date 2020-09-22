using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask.Domain.Models;

namespace TestTask.Domain.Repositories
{
    public interface IBooksRepository : IRepository<Book>
    {
        Task Add(Book book, IEnumerable<Author> authors);
        Task<IEnumerable<Book>> GetBooksByAuthorId(int authorId);
    }
}