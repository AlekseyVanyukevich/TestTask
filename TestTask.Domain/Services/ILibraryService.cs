using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask.Domain.ViewModels;

namespace TestTask.Domain.Services
{
    public interface ILibraryService
    {
        Task<IEnumerable<BookViewModel>> GetLibraryBooks();

        Task<BookViewModel> GetBookInfoById(int id);

        Task<AuthorViewModel> GetAuthorInfoById(int id);

        Task CreateNewBook(BookViewModel book);

        Task UpdateBook(BookViewModel book);

        Task DeleteBook(int id);
    }
}