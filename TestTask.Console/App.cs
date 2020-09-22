using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask.Console.Extensions;
using TestTask.Console.Models;
using TestTask.Domain.Models;
using TestTask.Infrastructure.Repositories;

namespace TestTask.Console
{
    public class App
    {
        private readonly ILibraryRepository _libraryRepository;
        private readonly MenuCollection<MenuItem> _menuCollection;

        public App(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
            _menuCollection = new MenuCollection<MenuItem>
            {
                new MenuItem { Index = 1, ActionAsync = OnAdd, Title = "Create book"},
                new MenuItem { Index = 2, ActionAsync = OnGetAuthors, Title = "Display authors"},
                new MenuItem { Index = 3, ActionAsync = OnGetBooks, Title = "Display books"},
                new MenuItem { Index = 4, ActionAsync = OnBooksByAuthorId, Title = "Display author's books"},
                new MenuItem { Index = 5, ActionAsync = OnAuthorsByBookId, Title = "Display book's authors"},
                new MenuItem { Index = 6, ActionAsync = OnDeleteBook, Title = "Delete book by id"},
                new MenuItem { Index =  0, ActionAsync = OnExit, Title = "Exit"}
            };
        }

        internal async Task Run()
        {
            while (true)
            {
                System.Console.WriteLine(_menuCollection.ToString());
                System.Console.WriteLine("Choose one from menu");
                if (!int.TryParse(System.Console.ReadLine(), out int index))
                {
                    System.Console.WriteLine("Wrong index");
                    continue;
                }
                var menuItem = _menuCollection.FirstOrDefault(mi => mi.Index == index);
                System.Console.WriteLine(Environment.NewLine);
                if (menuItem == null)
                {
                    System.Console.WriteLine("Wrong menu item selected");
                }
                else
                {
                    try
                    {
                        await menuItem.ActionAsync();
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine($"Exception: {ex.Message}");
                    }
                }
                System.Console.WriteLine(Environment.NewLine);
            }
        }
        

        private async Task OnAdd()
        {
            System.Console.WriteLine("Enter the book name:");
            var bookName = System.Console.ReadLine();
            System.Console.WriteLine("Enter the publication year");
            if (!int.TryParse(System.Console.ReadLine(), out var year))
            {
                throw new ArgumentException("Year must be an integer");
            }
            if (DateTime.Now.Year < year)
            {
                throw new ArgumentException("Year must be less or equal to the current");
            }
            var authors = new List<Author>();
            while (true)
            {
                System.Console.WriteLine("Enter the author's surname");
                var surname = System.Console.ReadLine() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(surname))
                {
                    break;
                }

                authors.Add(new Author {Surname = surname});
            }
            var book = new Book{ Name = bookName, Year = year};
            await _libraryRepository.CreateBook(book, authors);
            System.Console.WriteLine("Book created successfully");
        }

        private async Task OnGetAuthors()
        {
            var authors = await _libraryRepository.GetAllAuthors();
            System.Console.WriteLine(authors.ToMenu(a => a.ToString()));
        }

        private async Task OnGetBooks()
        {
            var books = await _libraryRepository.GetBooks();
            System.Console.WriteLine(books.ToMenu(b => b.ToString()));
        }

        private async Task OnBooksByAuthorId()
        {
            System.Console.WriteLine("Enter author id");
            if (!int.TryParse(System.Console.ReadLine(), out int id))
            {
                throw new ArgumentException("Author id must be integer");
            }

            var authorBooks = await _libraryRepository.GetBooksByAuthorId(id);
            System.Console.WriteLine(authorBooks.ToMenu(b => b.ToString()));
        }
        
        private async Task OnAuthorsByBookId()
        {
            System.Console.WriteLine("Enter book id");
            if (!int.TryParse(System.Console.ReadLine(), out int id))
            {
                throw new ArgumentException("Book id must be integer");
            }

            var bookAuthors = await _libraryRepository.GetAuthorsByBookId(id);
            System.Console.WriteLine(bookAuthors.ToMenu(a => a.ToString()));
        }

        private async Task OnDeleteBook()
        {
            System.Console.WriteLine("Enter book id");
            if (!int.TryParse(System.Console.ReadLine(), out int id))
            {
                throw new ArgumentException("Book id must be integer");
            }

            await _libraryRepository.DeleteBook(id);
            System.Console.WriteLine("Book deleted successfully");
        }

        private Task OnExit()
        {
            Environment.Exit(0);
            return Task.CompletedTask;
        }
    }
}