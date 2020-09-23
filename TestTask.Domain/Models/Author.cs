using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTask.Domain.Models
{
    public class Author
    {
        public int Id { get; set; }
        [StringLength(20)]
        [Required]
        public string Surname { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }

        public override string ToString()
        {
            return $"{Id}. {Surname}";
        }
    }
}