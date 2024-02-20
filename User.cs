using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_25
{
    public class User
    {
        protected internal void Var(string _Name, string _Email)
        {
            Name = _Name;
            Email = _Email;
        }
        [Key]
        public int Id { get; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}
