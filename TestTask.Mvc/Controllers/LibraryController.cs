using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestTask.Domain.Services;
using TestTask.Domain.ViewModels;

namespace TestTask.Mvc.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }
        // GET
        public async Task<IActionResult> Index()
        {
            var inventory = await _libraryService.GetLibraryBooks();
            return View(inventory);
        }

        public async Task<IActionResult> EditBook(int id)
        {
            var book = await _libraryService.GetBookInfoById(id);
            return View("CreateEditBook", book);
        }

        public async Task<IActionResult> BookDetails(int id)
        {
            var bookInfo = await _libraryService.GetBookInfoById(id);
            return View(bookInfo);
        }

        public IActionResult DeleteBook(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IActionResult> AuthorProfile(int id)
        {
            var authorInfo = await _libraryService.GetAuthorInfoById(id);
            return View(authorInfo);
        }

        public IActionResult CreateBook()
        {
            return View("CreateEditBook", new BookViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateEditBook(BookViewModel book)
        {
            if (ModelState.IsValid)
            {
                
            }

            return View(book);
        }
    }
}