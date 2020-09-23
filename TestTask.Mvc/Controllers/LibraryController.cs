using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestTask.Domain.Services;
using TestTask.Domain.ViewModels;

namespace TestTask.Mvc.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILibraryService _libraryService;
        private ILogger<LibraryController> _logger;
        public LibraryController(ILibraryService libraryService, ILogger<LibraryController> logger)
        {
            _libraryService = libraryService;
            _logger = logger;
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

        public async Task<IActionResult> BookDetails(int id, bool? delete)
        {
            ViewBag.ToDelete = delete == true;
            var bookInfo = await _libraryService.GetBookInfoById(id);
            foreach (var author in bookInfo.Authors)
            {
                _logger.LogInformation((author == null).ToString());
            }
      
            return View(bookInfo);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _libraryService.DeleteBook(id);
                TempData["Success"] = "Book deleted successfully";
            }
            catch
            {
                TempData["Error"] = "Failed to delete book";
            }

            return RedirectToAction("Index");
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
        public async Task<IActionResult> CreateEditBook(BookViewModel book, string[] author)
        {
            if (author.Length > 0)
            {
                var newAuthors = author.Select(surname => new AuthorViewModel {Surname = surname});
                if (book.Authors?.Any() == true)
                {
                    foreach (var a in newAuthors)
                    {
                        book.Authors.Append(a);
                    }
                }
                else
                {
                    book.Authors = newAuthors;
                }
            }
            if (ModelState.IsValid)
            {
                
                try
                {
                    if (book.Id == 0)
                    {
                        await _libraryService.CreateNewBook(book);
                        TempData["Success"] = "User created successfully";
                    }
                    else
                    {
                        await _libraryService.UpdateBook(book);
                        TempData["Success"] = "User updated successfully";
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex.Message);
                    TempData["Error"] = "Failed to submit";
                }

                return RedirectToAction("Index");
            }
            return View(book);
        }

        public async Task<IActionResult> AuthorList()
        {
            var authors = await _libraryService.GetAuthors();
            return View(authors);
        }
    }
}