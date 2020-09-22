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
    }
}