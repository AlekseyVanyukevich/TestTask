using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TestTask.Domain.Services
{
    public interface IFileService
    {
        Task<string> UploadFile(IFormFile file);
    }
}