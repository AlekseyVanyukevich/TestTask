using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTask.Domain.Models
{
    public class Author
    {
        public int Id { get; set; }
        [StringLength(15)]
        [Required]
        public string Name { get; set; }
        [StringLength(20)]
        [Required]
        public string Surname { get; set; }
        // public string Image { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
    }
}