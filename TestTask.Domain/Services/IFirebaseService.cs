using System.Threading.Tasks;

namespace TestTask.Domain.Services
{
    public interface IFirebaseService
    {
        Task SendNotifications(string title, string body, string topic);
    }
}