using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Module_25.EFW.Program;

namespace Module_25
{
    public class Book
    {
        protected internal void Var(string _Name, DateOnly _PublishedDate, User? _User)
        {
            Name = _Name;
            PublishedDate = _PublishedDate;
            User = _User;
        }
        protected internal void Var(string _Name, DateOnly _PublishedDate)
        {
            Name = _Name;
            PublishedDate = _PublishedDate;
        }

        [Key]
        public int Id { get; }
        public string? Name { get; set; }
        public DateOnly? PublishedDate { get; set; }
        public User? User { get; set; }
        public Author? Author { get; set; }
        public Genre? Genre { get; set; }
    }
}
