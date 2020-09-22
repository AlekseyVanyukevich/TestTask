using System;
using System.Threading.Tasks;

namespace TestTask.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBooksRepository Books { get; }
        IAuthorsRepository Authors { get; }
        Task<int> SaveAsync();
    }
}