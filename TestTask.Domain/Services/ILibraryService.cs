using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask.Domain.ViewModels;
using TestTask.Domain.ViewModels.Author;
using TestTask.Domain.ViewModels.Book;

namespace TestTask.Domain.Services
{
    public interface ILibraryService
    {
        Task<IEnumerable<BookModel>> GetLibraryBooks();

        Task<BookModel> GetBookInfoById(int id);

        Task<AuthorModel> GetAuthorInfoById(int id);

        Task CreateNewBook(BookModel book);

        Task UpdateBook(BookModel book);

        Task DeleteBook(int id);

        Task<IEnumerable<AuthorModel>> GetAuthors();
    }
}