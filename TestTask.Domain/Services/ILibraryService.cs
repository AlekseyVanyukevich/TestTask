#nullable enable
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

        Task CreateNewBook(BookFormModel bookFormModel);

        Task UpdateBook(BookFormModel bookFormModel);
        
        Task DeleteBook(int id);

        Task<IEnumerable<AuthorModel>> GetAuthors();

        Task AddAuthor(AuthorFormModel authorFormModel);

        BookFormModel CreateBookFormModel(BookModel bookModel);
    }
}