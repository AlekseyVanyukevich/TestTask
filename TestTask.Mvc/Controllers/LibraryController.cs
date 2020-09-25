using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestTask.Domain.Data;
using TestTask.Domain.Services;
using TestTask.Domain.ViewModels;
using TestTask.Mvc.ResponseModels;

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

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> EditBook(BookViewModel bookModel)
        {
            return RedirectToAction("Index");
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _libraryService.DeleteBook(id);
                TempData["Alert"] = new AlertViewModel
                {
                    Content = "Book deleted successfully",
                    Type = AlertType.Success
                };
            }
            catch
            {
                TempData["Alert"] = new AlertViewModel
                {
                    Content = "Failed to delete book",
                    Type = AlertType.Danger
                };
            }

            return RedirectToAction("Index");
        }

        public IActionResult CreateBook()
        {
            return View("CreateEditBook", new BookViewModel());
        }

        public IActionResult CreateEditBook(BookViewModel bookModel)
        {
            return View(bookModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateBook(BookViewModel bookModel)
        {
            return RedirectToAction("Index");
        }
        
        // public async Task<IActionResult> CreateEditBook(BookViewModel book, string[] author)
        // {
        //     if (author.Length > 0)
        //     {
        //         var newAuthors = author.Select(surname => new AuthorViewModel {Surname = surname});
        //         if (book.Authors?.Any() == true)
        //         {
        //             foreach (var a in newAuthors)
        //             {
        //                 book.Authors.Append(a);
        //             }
        //         }
        //         else
        //         {
        //             book.Authors = newAuthors;
        //         }
        //     }
        //     if (ModelState.IsValid)
        //     {
        //         
        //         try
        //         {
        //             if (book.Id == 0)
        //             {
        //                 await _libraryService.CreateNewBook(book);
        //                 TempData["Alert"] = "Book created successfully";
        //             }
        //             else
        //             {
        //                 await _libraryService.UpdateBook(book);
        //                 TempData["Success"] = "Book updated successfully";
        //             }
        //         }
        //         catch (Exception ex)
        //         {
        //             TempData["Error"] = ex.Message;
        //         }
        //
        //         return RedirectToAction("Index");
        //     }
        //     return View(book);
        // }
        


        public async Task<IActionResult> AuthorList(int pageIndex = 1, int pageSize = 10)
        {
            var authors = await _libraryService.GetAuthors();
            var authorsOnPage = authors.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            var paginatedList = new PaginatedResponseModel<AuthorViewModel>(
                pageIndex, pageSize, authors.Count(), authorsOnPage);
            
            return View(paginatedList);
        }
        public async Task<IActionResult> AuthorProfile(int id)
        {
            var authorInfo = await _libraryService.GetAuthorInfoById(id);
            return View(authorInfo);
        }
        public async Task<IActionResult> CreateEditAuthor(AuthorViewModel authorModel)
        {
            return View(authorModel);
        }

        public IActionResult CreateAuthor()
        {
            return View("CreateEditAuthor", new AuthorViewModel());
        }

        public IActionResult EditAuthor(AuthorViewModel authorModel)
        {
            return View("CreateEditAuthor", authorModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAuthor()
    }
}