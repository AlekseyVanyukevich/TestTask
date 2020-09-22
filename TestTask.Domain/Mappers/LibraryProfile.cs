using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TestTask.Domain.Models;
using TestTask.Domain.ViewModels;

namespace TestTask.Domain.Mappers
{
    public class LibraryProfile : Profile
    {
        public LibraryProfile()
        {
            CreateMap<Book, BookViewModel>();
            CreateMap<Author, AuthorViewModel>();
            CreateMap<IEnumerable<Author>, List<AuthorViewModel>>();
            CreateMap<IEnumerable<Book>, List<BookViewModel>>();

        }
    }
}