using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TestTask.Domain.Models;
using TestTask.Domain.Repositories;
using TestTask.Domain.ViewModels;
using TestTask.Domain.ViewModels.Author;
using TestTask.Domain.ViewModels.Book;

namespace TestTask.Domain.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LibraryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<BookModel>> GetLibraryBooks()
        {
            var books = await _unitOfWork.Books.GetAll();
            var mappedBooks = _mapper.Map<IEnumerable<BookModel>>(books);
            return mappedBooks;
        }

        public async Task<BookModel> GetBookInfoById(int id)
        {
            var book = await _unitOfWork.Books.Get(id);
            var mappedBook = _mapper.Map<BookModel>(book);
            return mappedBook;
        }

        public async Task<AuthorModel> GetAuthorInfoById(int id)
        {
            var author = await _unitOfWork.Authors.Get(id);
            var mappedAuthor = _mapper.Map<AuthorModel>(author);
            return mappedAuthor;
        }

        public async Task CreateNewBook(BookFormModel bookFormModel)
        {
            var book = _mapper.Map<Book>(bookFormModel);
            await _unitOfWork.Books.Add(book);
            await _unitOfWork.SaveAsync();
        }
        
        public async Task UpdateBook(BookFormModel bookFormModel)
        {
            var book = _mapper.Map<Book>(bookFormModel);
            await _unitOfWork.Books.UpdateBook(book);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteBook(int id)
        {
            var bookModel = await _unitOfWork.Books.Get(id);
            var book = _mapper.Map<Book>(bookModel);
            _unitOfWork.Books.Delete(book);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<AuthorModel>> GetAuthors()
        {
            var authors = await _unitOfWork.Authors.GetAll();
            var mappedAuthors = _mapper.Map<IEnumerable<AuthorModel>>(authors);
            return mappedAuthors;
        }

        public async Task AddAuthor(AuthorFormModel authorFormModel)
        {
            var author = _mapper.Map<Author>(authorFormModel);
            await _unitOfWork.Authors.Add(author);
            await _unitOfWork.SaveAsync();
        }

        public BookFormModel CreateBookFormModel(BookModel bookModel)
        {
            return _mapper.Map<BookFormModel>(bookModel);
        }
    }
}