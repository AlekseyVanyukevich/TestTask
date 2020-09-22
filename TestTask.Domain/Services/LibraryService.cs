using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Domain.Repositories;
using TestTask.Domain.ViewModels;

namespace TestTask.Domain.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LibraryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<IEnumerable<LibraryItemViewModel>> GetLibrary()
        {
            var books = await _unitOfWork.Books.GetAll();
            var inventory = new List<LibraryItemViewModel>();
            foreach (var book in books)
            {
                var authors = await _unitOfWork.Authors.GetBookAuthors(book.Id);
                inventory.Add(new LibraryItemViewModel{ Authors = authors, Book = book});
            }

            return inventory;
        }
    }
}