using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Module_25.EFW.Program;

namespace Module_25.EFW
{
    internal class DBGenreExec
    {
        protected internal static void Add(DB _db, string _name)
        {
            Genre _genre = new Genre();
            _genre.Var(_name);
            _db.context.Genres.Add(_genre);
            _db.context.SaveChanges();
        }
        protected internal static void DelById(DB _db, int _id)
        {
            _db.context.Genres.Where(b => b.Id == _id).ExecuteDelete();
            _db.context.SaveChanges();
        }
        protected internal static Genre[]? GetAll(DB _db)
        {
            Genre[]? _genres = _db.context.Genres.ToArray();
            if (_genres == null)
            {
                return null;
            }
            else
            {
                return _genres;
            }
        }
        protected internal static Genre? GetById(DB _db, int _id)
        {
            Genre? _genre = _db.context.Genres.FirstOrDefault(b => b.Id == _id);
            if (_genre == null)
            {
                return null;
            }
            else
            {
                return _genre;
            }
        }
    }
}
