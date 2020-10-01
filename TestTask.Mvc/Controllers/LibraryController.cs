using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TestTask.Domain.Data;
using TestTask.Domain.Services;
using TestTask.Domain.ViewModels;
using TestTask.Domain.ViewModels.Author;
using TestTask.Domain.ViewModels.Book;
using TestTask.Mvc.Extensions;
using TestTask.Mvc.ResponseModels;

namespace TestTask.Mvc.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILibraryService _libraryService;
        private readonly IFirebaseService _firebase;
        private readonly IConfiguration _configuration;
        private ILogger<LibraryController> _logger;
        public LibraryController(ILibraryService libraryService, IFirebaseService firebase, IConfiguration configuration)
        {
            _libraryService = libraryService;
            _firebase = firebase;
            _configuration = configuration;
        }
        // GET
        public async Task<IActionResult> Index()
        {
            var r = _configuration.GetValue<string>("firebaseFile");
            // await _firebase.SendNotifications("App", "App is starting", "TestTask");
            var inventory = await _libraryService.GetLibraryBooks();
            return Content(r);
            return View(inventory);
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
                TempData.Put("Alert", new AlertModel
                {
                    Content = "Book deleted successfully",
                    Type = AlertType.Success
                });
            }
            catch
            {
                TempData.Put("Alert", new AlertModel
                {
                    Content = "Failed to delete book",
                    Type = AlertType.Danger
                });
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CreateBook()
        {
            var formModel = new BookFormModel();
            ViewBag.Authors = await GetSelectedList(formModel);
            return View("CreateEditBook", formModel);
        }

        public async Task<IActionResult> EditBook(int id)
        {
            var book = await _libraryService.GetBookInfoById(id);
            var formModel = _libraryService.CreateBookFormModel(book);
            ViewBag.Authors = await GetSelectedList(formModel);
            return View("CreateEditBook", formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBook(BookFormModel bookFormModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Authors = await GetSelectedList(bookFormModel);
                return View("CreateEditBook", bookFormModel);
            }

            try
            {
                await _libraryService.CreateNewBook(bookFormModel);
                TempData.Put("Alert", new AlertModel
                {
                    Content = "Book created successfully",
                    Type = AlertType.Success
                });
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Authors = await GetSelectedList(bookFormModel);
                ViewBag.Alert = new AlertModel
                {
                    Content = ex.Message,
                    Type = AlertType.Danger
                };
                return View("CreateEditBook", bookFormModel);
            }
        }
        

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> EditBook(BookFormModel bookFormModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Authors = await GetSelectedList(bookFormModel);
                return View("CreateEditBook", bookFormModel);
            }

            try
            {
                await _libraryService.UpdateBook(bookFormModel);
                TempData.Put("Alert", new AlertModel
                {
                    Content = "Book updated successfully",
                    Type = AlertType.Success
                });
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Authors = await GetSelectedList(bookFormModel);
                ViewBag.Alert = new AlertModel
                {
                    Content = ex.Message,
                    Type = AlertType.Danger
                };
                return View("CreateEditBook", bookFormModel);
            }
        }
        


        public async Task<IActionResult> AuthorList(int pageIndex = 1, int pageSize = 4)
        {
            var authors = await _libraryService.GetAuthors();
            var authorsOnPage = authors.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            var paginatedList = new PaginatedResponseModel<AuthorModel>(
                pageIndex, pageSize, authors.Count(), authorsOnPage);
            
            return View(paginatedList);
        }
        public async Task<IActionResult> AuthorProfile(int id)
        {
            var authorInfo = await _libraryService.GetAuthorInfoById(id);
            return View(authorInfo);
        }
        public async Task<IActionResult> CreateEditAuthor(AuthorFormModel authorFormModel)
        {
            return View(authorFormModel);
        }

        public IActionResult CreateAuthor()
        {
            return View("CreateEditAuthor", new AuthorFormModel());
        }

        public IActionResult EditAuthor(AuthorModel authorModel)
        {
            return View("CreateEditAuthor", new AuthorFormModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAuthor(AuthorFormModel authorForm)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateEditAuthor", authorForm);
            }

            try
            {
                await _libraryService.AddAuthor(authorForm);
                var alert = new AlertModel
                {
                    Content = "Author created successfully",
                    Type = AlertType.Success
                };
                TempData.Put("Alert", alert);
                return RedirectToAction("AuthorList");
            }
            catch (Exception ex)
            {
                ViewBag.Alert = new AlertModel
                {
                    Content = ex.Message,
                    Type = AlertType.Danger
                };
                return View("CreateEditAuthor", authorForm);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAuthor(AuthorFormModel authorForm)
        {
            return RedirectToAction("AuthorProfile", new { id = authorForm.Id});
        }

        private async Task<List<SelectListItem>> GetSelectedList(BookFormModel bookFormModel)
        {
            return (await _libraryService
                    .GetAuthors())
                .Select(a => new SelectListItem
                {
                    Text = a.Surname,
                    Value = a.Id.ToString(),
                    Selected = bookFormModel?.AuthorIds?.Contains(a.Id) ?? false
                }).ToList();
        }
    }
}