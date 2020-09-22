using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask.Domain.Models;

namespace TestTask.Domain.Repositories
{
    public interface IAuthorsRepository : IRepository<Author>
    {
        Task<IEnumerable<Author>> GetBookAuthors(int bookId);
    }
}