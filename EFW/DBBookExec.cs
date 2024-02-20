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
        protected internal static void Add(DB _db, string _name, DateOnly _datetime, User _user)
        {
            Book _book = new Book();
            _book.Var(_name, _datetime, _user);
            _db.context.Books.Add(_book);
            _db.context.SaveChanges();
        }
        protected internal static void Add(DB _db, string _name, DateOnly _datetime)
        {
            Book _book = new Book();
            _book.Var(_name, _datetime);
            _db.context.Books.Add(_book);
            _db.context.SaveChanges();
        }

        protected internal static void AddToUser(DB _db, Book _book, User _user)
        {
            _book.User = _user;
            _db.context.Books.Update(_book);
            _db.context.SaveChanges();
        }
        protected internal static Book? GetById(DB _db, int _id)
        {
            Book? _book = _db.context.Books.FirstOrDefault(b => b.Id == _id);
            return _book;
        }
        protected internal static Book[]? GetByUserId(DB _db, int _id)
        {
            User? _user = _db.context.Users.FirstOrDefault(u => u.Id == _id);
            Book[]? _books = _db.context.Books.Where(b => b.User == _user).ToArray();
            return _books;
        }
        protected internal static Book[]? GetByUser(DB _db, User _user)
        {
            Book[]? _books = _db.context.Books.Where(b => b.User == _user).ToArray();
            return _books;
        }
        protected internal static Book[]? GetByAuthor(DB _db, Author _author)
        {
            Book[]? _books = _db.context.Books.Where(b => b.Author == _author).ToArray();
            return _books;
        }
        protected internal static Book[]? GetByGenre(DB _db, Genre _genre)
        {
            Book[]? _books = _db.context.Books.Where(b => b.Genre == _genre).ToArray();
            return _books;
        }
        
        protected internal static Book[]? GetAll(DB _db)
        {
            Book[]? _book = _db.context.Books.ToArray();
            return _book;
        }
        protected internal static void DelById(DB _db, int _id)
        {
            _db.context.Books.Where(b => b.Id == _id).ExecuteDelete();
            _db.context.SaveChanges();
        }
        protected internal static void ChangePubDate(DB _db, Book _book, DateOnly _date)
        {
            _book.PublishedDate = _date;
            _db.context.Books.Update(_book);
            _db.context.SaveChanges();
        }
        protected internal static void GenreUpdate(DB _db, Book _book, Genre _genre)
        {
            _book.Genre = _genre;
            _db.context.Books.Update(_book);
            _db.context.SaveChanges();
        }
        protected internal static void AuthorUpdate(DB _db, Book _book, Author _author)
        {
            _book.Author = _author;
            _db.context.Books.Update(_book);
            _db.context.SaveChanges();
        }
        protected internal static Book[]? GetByDates(DB _db, DateOnly _dateFrom, DateOnly _dateTo)
        {
            Book[]? _books = _db.context.Books.Where(b => b.PublishedDate >= _dateFrom && b.PublishedDate <= _dateTo).ToArray();
            return _books;
        }
    }
}
