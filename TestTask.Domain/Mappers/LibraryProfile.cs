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
            CreateMap<Book, BookViewModel>()
                .ForMember(x => x.Authors, 
                    opt => 
                        opt.MapFrom(b => b.BookAuthors.Select(ba => ba.Author)));
            CreateMap<Author, AuthorViewModel>()
                .ForMember(x => x.Books, 
                    opt => 
                        opt.MapFrom(a => a.BookAuthors.Select(ba => ba.Book)));

            CreateMap<AuthorViewModel, Author>();
            CreateMap<BookViewModel, Book>();
        }
    }
    
}