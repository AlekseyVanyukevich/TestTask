using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TestTask.Domain.Services
{
    public class CloudFileService : IFileService
    {
        public async Task<string> UploadFile(IFormFile file)
        {
            throw new System.NotImplementedException();
        }
    }
}