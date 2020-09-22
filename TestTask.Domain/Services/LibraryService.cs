﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TestTask.Domain.Repositories;
using TestTask.Domain.ViewModels;

namespace TestTask.Domain.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LibraryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<LibraryItemViewModel>> GetLibrary()
        {
            var books = await _unitOfWork.Books.GetAll();
            var inventory = new List<LibraryItemViewModel>();
            foreach (var book in books)
            {
                var authors = await _unitOfWork.Authors.GetBookAuthors(book.Id);
                inventory
                    .Add(
                        new LibraryItemViewModel
                        {
                            Authors = authors.Select(a => _mapper.Map<AuthorViewModel>(a)), 
                            Book = _mapper.Map<BookViewModel>(book)
                        });
            }

            return inventory;
        }
    }
}