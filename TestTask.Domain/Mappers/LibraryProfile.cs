﻿using AutoMapper;
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
        }
    }
}