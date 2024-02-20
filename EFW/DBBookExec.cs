using Microsoft.EntityFrameworkCore;
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
        protected internal static void DBBookAuthorGanreAdd(DB _db, int _id, string _author, string _ganre)
        {
            Book? _book = _db.context.Books.FirstOrDefault(x => x.Id == _id);
            if (_book != null)
            {
                _book.Author = _author;
                _book.Genre = _ganre;
                _db.context.SaveChanges();
            }
        }
        protected internal static void DBBookAddToUser(DB _db, Book _book, User _user)
        {
            _book.User = _user;
            _db.context.Books.Update(_book);
            _db.context.SaveChanges();
        }
        protected internal static Book? DBBookGetById(DB _db, int _id)
        {
            Book? _book = _db.context.Books.FirstOrDefault(b => b.Id == _id);
            if (_book == null)
            {
                return null;
            }
            else
            {
                return _book;
            }
        }
        protected internal static Book[]? DBUserGetByUser(DB _db, int _id)
        {
            User? _user = _db.context.Users.FirstOrDefault(u => u.Id == _id);
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
        protected internal static Book[]? DBBookGetAll(DB _db)
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
        protected internal static void DBBookDelById(DB _db, int _id)
        {
            _db.context.Books.Where(b => b.Id == _id).ExecuteDelete();
            _db.context.SaveChanges();
        }
        protected internal static void DBBookChangePubDate(DB _db, Book _book, DateOnly _date)
        {
            _book.PublishedDate = _date;
            _db.context.Books.Update(_book);
            _db.context.SaveChanges();
        }
        protected internal static void DBBookGenreUpdate(DB _db, Book _book, string _genre)
        {
            _book.Genre = _genre;
            _db.context.Books.Update(_book);
            _db.context.SaveChanges();
        }
        protected internal static void DBBookAuthorUpdate(DB _db, Book _book, string _author)
        {
            _book.Author = _author;
            _db.context.Books.Update(_book);
            _db.context.SaveChanges();
        }
    }
}
