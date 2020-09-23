using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            CreateMap<Book, BookViewModel>().ReverseMap()
                .ForMember(x => x.Authors, opt => opt.Ignore());
            CreateMap<Author, AuthorViewModel>().ReverseMap()
                .ForMember(x => x.Books, opt => opt.Ignore());
            CreateMap<IEnumerable<Author>, List<AuthorViewModel>>();
            CreateMap<IEnumerable<Book>, List<BookViewModel>>();
        }
    }
}