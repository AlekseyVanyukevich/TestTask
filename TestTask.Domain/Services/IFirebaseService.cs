using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestTask.Domain.Services
{
    public interface IFirebaseService
    {
        void AddToken(string token);
        Task SendNotifications(string title, string body);

        Task SendNotification(string title, string body, string token);
    }
}