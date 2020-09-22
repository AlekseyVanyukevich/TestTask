using Microsoft.AspNetCore.Mvc;
using TestTask.Domain.Repositories;
using TestTask.Infrastructure;

namespace TestTask.Mvc.Controllers
{
    public class LibraryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LibraryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET
        public IActionResult Index()
        {

            var libraryInventory = _unitOfWork.Authors.
            return View();
        }
    }
}