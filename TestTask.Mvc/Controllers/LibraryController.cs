using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestTask.Domain.Services;

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
            var inventory = await _libraryService.GetLibrary();
            return View(inventory);
        }

        public IActionResult EditBook(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IActionResult> BookDetails(int id)
        {
            var bookInfo = await _libraryService.GetBookInfo(id);
            return View(bookInfo);
        }

        public IActionResult DeleteBook(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IActionResult> AuthorProfile(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}