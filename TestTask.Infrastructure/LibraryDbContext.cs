using System;
using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Models;

namespace TestTask.Infrastructure
{
    public class LibraryDbContext: DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) {}
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlServer("server=.\\SQLEXPRESS;database=TestTask;Trusted_Connection=True;");
        // }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });  
            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.Authors)
                .HasForeignKey(bc => bc.BookId);  
            modelBuilder.Entity<BookAuthor>()
                .HasOne(bc => bc.Author)
                .WithMany(c => c.Books)
                .HasForeignKey(bc => bc.AuthorId);

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        
        public DbSet<BookAuthor> BookAuthors { get; set; }
    }
}
