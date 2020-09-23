using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TestTask.Domain.Models;
using TestTask.Domain.Repositories;
using TestTask.Domain.ViewModels;

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
        
        public async Task<IEnumerable<BookViewModel>> GetLibraryBooks()
        {
            var books = await _unitOfWork.Books.GetAll();
            var inventory = new List<BookViewModel>();
            foreach (var book in books)
            {
                var authors = await _unitOfWork.Authors.GetBookAuthors(book.Id);
                var mappedBook = _mapper.Map<BookViewModel>(book);
                mappedBook.Authors = _mapper.Map<IEnumerable<AuthorViewModel>>(authors);
                inventory.Add(mappedBook);
            }

            return inventory;
        }

        public async Task<BookViewModel> GetBookInfoById(int id)
        {
            var book = await _unitOfWork.Books.Get(id);
            var authors = await _unitOfWork.Authors.GetBookAuthors(id);
            var mappedBook = _mapper.Map<BookViewModel>(book);
            mappedBook.Authors = _mapper.Map<IEnumerable<AuthorViewModel>>(authors);
            return mappedBook;
        }

        public async Task<AuthorViewModel> GetAuthorInfoById(int id)
        {
            var author = await _unitOfWork.Authors.Get(id);
            var books = await _unitOfWork.Books.GetBooksByAuthorId(id);
            var mappedAuthor = _mapper.Map<AuthorViewModel>(author);
            mappedAuthor.Books = _mapper.Map<IEnumerable<BookViewModel>>(books);
            return mappedAuthor;
        }

        public async Task CreateNewBook(BookViewModel bookModel)
        {
            var book = _mapper.Map<Book>(bookModel);
            var authors = _mapper.Map<IEnumerable<Author>>(bookModel.Authors);
            await _unitOfWork.Books.Add(book, authors);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateBook(BookViewModel bookModel)
        {
            var book = _mapper.Map<Book>(bookModel);
            var authors = _mapper.Map<IEnumerable<Author>>(bookModel.Authors);
            await _unitOfWork.Books.Update(book, authors);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteBook(int id)
        {
            var bookModel = await _unitOfWork.Books.Get(id);
            var book = _mapper.Map<Book>(bookModel);
            _unitOfWork.Books.Delete(book);
            await _unitOfWork.SaveAsync();
        }
    }
}