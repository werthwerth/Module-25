using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_25
{
    public class Genre
    {
        protected internal void Var(string _Name)
        {
            Name = _Name;
        }

        [Key]
        public int Id { get; }
        public string? Name { get; set; }

    }
}
