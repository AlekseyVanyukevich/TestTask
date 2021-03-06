﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask.Domain.Models;

namespace TestTask.Domain.Repositories
{
    public interface IBooksRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksByAuthorId(int authorId);
        Task UpdateBook(Book book);
    }
}