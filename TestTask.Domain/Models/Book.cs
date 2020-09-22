using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestTask.Domain.Attributes;

namespace TestTask.Domain.Models
{
    public class Book
    {
        public int Id { get; set; }
        [MaxLength(30)]
        [Required]
        public string Name { get; set; }
        [Required]
        [ValidYear]
        public int Year { get; set; }
        public virtual ICollection<BookAuthor> Authors { get; set; }

        public override string ToString()
        {
            return $"{Id}. {Name}";
        }
    }
}