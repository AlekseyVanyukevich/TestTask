using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask.Domain.ViewModels;

namespace TestTask.Domain.Services
{
    public interface ILibraryService
    {
        Task<IEnumerable<LibraryItemViewModel>> GetLibrary();
    }
}