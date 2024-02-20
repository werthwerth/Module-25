using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Module_25.EFW.Program;

namespace Module_25.EFW
{
    internal class DBAuthorExec
    {
        protected internal static void Add(DB _db, string _name)
        {
            Author _author = new Author();
            _author.Var(_name);
            _db.context.Authors.Add(_author);
            _db.context.SaveChanges();
        }
        protected internal static void DelById(DB _db, int _id)
        {
            _db.context.Authors.Where(b => b.Id == _id).ExecuteDelete();
            _db.context.SaveChanges();
        }
        protected internal static Author[]? GetAll(DB _db)
        {
            Author[]? _authors = _db.context.Authors.ToArray();
            if (_authors == null)
            {
                return null;
            }
            else
            {
                return _authors;
            }
        }
        protected internal static Author? GetById(DB _db, int _id)
        {
            Author? _author = _db.context.Authors.FirstOrDefault(b => b.Id == _id);
            if (_author == null)
            {
                return null;
            }
            else
            {
                return _author;
            }
        }
    }
}
