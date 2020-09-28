using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using TestTask.Domain.Models;
using TestTask.Domain.ViewModels;
using TestTask.Domain.ViewModels.Author;
using TestTask.Domain.ViewModels.Book;

namespace TestTask.Domain.Mappers
{
    public class LibraryProfile : Profile
    {
        public LibraryProfile()
        {
            CreateMap<Book, BookModel>()
                .ForMember(x => x.Authors, 
                    opt => 
                        opt.MapFrom(b => b.BookAuthors.Select(ba => ba.Author)));
            CreateMap<Author, AuthorModel>()
                .ForMember(x => x.Books, 
                    opt => 
                        opt.MapFrom(a => a.BookAuthors.Select(ba => ba.Book)));

            CreateMap<AuthorModel, Author>();
            CreateMap<BookModel, Book>();
            
            
            CreateMap<AuthorModel, AuthorFormModel>().ReverseMap();
            CreateMap<AuthorFormModel, Author>();

            CreateMap<BookModel, BookFormModel>()
                .ForMember(x => x.AuthorIds, opts =>
                {
                    opts.MapFrom(bm => bm.Authors.Select(a => a.Id));
                });

            CreateMap<BookFormModel, Book>()
                .ForMember(x => x.BookAuthors, opts =>
                {
                    opts.MapFrom(bfm => bfm.AuthorIds.Select(aId => new BookAuthor
                    {
                        BookId = bfm.Id,
                        AuthorId = aId
                    }));
                });
        }
    }
    
}