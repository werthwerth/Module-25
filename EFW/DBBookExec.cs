using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Module_25.EFW.Program;

namespace Module_25.EFW
{
    internal class DBBookExec
    {
        protected internal static void DBBookAdd(DB _db, string _name, DateOnly _datetime, User _user)
        {
            Book _book = new Book();
            _book.Var(_name, _datetime, _user);
            _db.context.Books.Add(_book);
            _db.context.SaveChanges();
        }
        protected internal static void DBBookAdd(DB _db, string _name, DateOnly _datetime)
        {
            Book _book = new Book();
            _book.Var(_name, _datetime);
            _db.context.Books.Add(_book);
            _db.context.SaveChanges();
        }
        protected internal static void DBBookAddToUser(DB _db, Book _book, User _user)
        {
            _book.User = _user;
            _db.context.Books.Update(_book);
            _db.context.SaveChanges();
        }
        protected internal static Book[]? DBUserGetByName(DB _db, string _name)
        {
            Book[]? _book = _db.context.Books.Where(b => b.Name == _name).ToArray();
            if (_book == null)
            {
                return null;
            }
            else
            {
                return _book;
            }
        }
        protected internal static Book[]? DBUserGetByUser(DB _db, string _name)
        {
            User? _user = _db.context.Users.FirstOrDefault(u => u.Name == _name);
            Book[]? _books = _db.context.Books.Where(b => b.User == _user).ToArray();
            if (_books == null)
            {
                return null;
            }
            else
            {
                return _books;
            }
        }
        protected internal static Book[]? DBUserGetAll(DB _db)
        {
            Book[]? _book = _db.context.Books.ToArray();
            if (_book == null)
            {
                return null;
            }
            else
            {
                return _book;
            }
        }
    }
}
