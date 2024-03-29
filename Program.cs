﻿using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Module_25.EFW.Program;

namespace Module_25.EFW
{
    public class Program
    {
        public static void help()
        {
            Console.WriteLine("Список реализованных команд:");
            Console.WriteLine("help - Список команд");
            Console.WriteLine("addUser - Добавить пользователя");
            Console.WriteLine("addBook - Добавить книгу");
            Console.WriteLine("bookToUser - Выдать книгу кользователю");
            Console.WriteLine("getBooks - Скписок всех книг");
            Console.WriteLine("getUsers - Список всех пользователей");
            Console.WriteLine("delBook - Удалить книгу (по id)");
            Console.WriteLine("changePubDate - Изменить книге (по id) дату выхода");
            Console.WriteLine("addAuthor - Добавить автора");
            Console.WriteLine("getAuthors - Получить список всех авторов");
            Console.WriteLine("changeAuthor - Изменить автора книги (id, id)");
            Console.WriteLine("addGenre - Добавить жанр");
            Console.WriteLine("getGenres - Список всех жанров");
            Console.WriteLine("changeGenre - Изменить жанр книги (id, id)");
            Console.WriteLine("getBooksByDates - Получить список книг по дате выхода (диапазон)");
            Console.WriteLine("getBooksByAuthor - Получить список книг по автору (id)");
            Console.WriteLine("getBooksCountByAuthor - Получить количество книг по автору (id)");
            Console.WriteLine("findByAuthorAndName - Узнать наличие книги по автору и части названия (id, название)");
            Console.WriteLine("findByOwnerAndName - Узнать находится ли книга у пользователя (id, id)");
            Console.WriteLine("getCountBookOnUser - Получить количество книг у пользователя (id)");
            Console.WriteLine("getLastBook - Получить последнюю вышедшую книгу");
            Console.WriteLine("getBooksSortedByName - получить все книги, отсортированые по названию");
            Console.WriteLine("getBooksSortedByPubDateDesc - получить все книги, отсортированые по дате выхода в порядке убывания");
            Console.WriteLine("exit - без комментариев");
        }
        public static void BooksPrinter(Book[] _books)
        {
            foreach (var _b in _books)
            {
                string? _genrename;
                string? _authorname;
                if (_b.Genre == null) { _genrename = "null"; } else { _genrename = _b.Genre.Name; }
                if (_b.Author == null) { _authorname = "null"; } else { _authorname = _b.Author.Name; }
                if (_b.User != null)
                {
                    Console.WriteLine(string.Format("ID: \"{0}\" Bookname: \"{1}\" publish date: \"{2}\" current reader: \"{3}\" genre: \"{4}\" author: \"{5}\"", _b.Id, _b.Name, _b.PublishedDate, _b.User.Name, _genrename, _authorname));
                }
                else
                {
                    Console.WriteLine(string.Format("ID: \"{0}\" Bookname: \"{1}\" publish date: \"{2}\" current reader: \"none\" genre: \"{3}\" author: \"{4}\"", _b.Id, _b.Name, _b.PublishedDate, _genrename, _authorname));
                }
            }
        }
        public static void BooksPrinter(Book _book)
        {
            string? _genrename;
            string? _authorname;
            if (_book.Genre == null) { _genrename = "null"; } else { _genrename = _book.Genre.Name; }
            if (_book.Author == null) { _authorname = "null"; } else { _authorname = _book.Author.Name; }
            if (_book.User != null)
            {
                Console.WriteLine(string.Format("ID: \"{0}\" Bookname: \"{1}\" publish date: \"{2}\" current reader: \"{3}\" genre: \"{4}\" author: \"{5}\"", _book.Id, _book.Name, _book.PublishedDate, _book.User.Name, _genrename, _authorname));
            }
            else
            {
                Console.WriteLine(string.Format("ID: \"{0}\" Bookname: \"{1}\" publish date: \"{2}\" current reader: \"none\" genre: \"{3}\" author: \"{4}\"", _book.Id, _book.Name, _book.PublishedDate, _genrename, _authorname));
            }
        }
        static void Main()
        {
            DB _db = new DB();
            bool onAir = true;
            string? _command;
            help();
            while (onAir)
            {

                Console.Write("Введите команду:");
                _command = Console.ReadLine();
                if (_command == "help")
                {
                    help();
                }
                else if (_command == "addUser")
                {

                    Console.Write("Enter fullname:");
                    var _username = Console.ReadLine();
                    Console.Write("Enter email:");
                    var _email = Console.ReadLine();
                    if (!String.IsNullOrEmpty(_username) && !String.IsNullOrEmpty(_email))
                    {
                        DBUserExec.Add(_db, _username, _email);
                    }
                }
                else if (_command == "addBook")
                {
                    Console.Write("Enter bookname:");
                    var _bookname = Console.ReadLine();
                    Console.Write("Enter published date (yyyy-mm-dd):");
                    DateOnly.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", out var _date);
                    if (!String.IsNullOrEmpty(_bookname))
                    {
                        DBBookExec.Add(_db, _bookname, _date);
                    }
                }
                else if (_command == "bookToUser")
                {
                    Console.Write("Enter user id:");
                    if (Int32.TryParse(Console.ReadLine(), out var _userid))
                    {
                        Console.Write("Enter book id:");
                        if (Int32.TryParse(Console.ReadLine(), out var _bookid))
                        {
                            User? _user = DBUserExec.GetById(_db, _userid);
                            var _book = DBBookExec.GetById(_db, _bookid);
                            if (_user != null && _book != null)
                            {
                                DBBookExec.AddToUser(_db, _book, _user);
                            }
                        }
                    }

                }
                else if (_command == "getBooks")
                {
                    Book[]? _books = DBBookExec.GetAll(_db);
                    if (_books != null)
                    {
                        BooksPrinter(_books);
                    }
                    else
                    {
                        Console.WriteLine("empty");
                    }
                }
                else if (_command == "getUsers")
                {
                    User[]? _userList = DBUserExec.GetAll(_db);
                    if (_userList != null)
                    {
                        foreach (var _user in _userList)
                        {
                            Console.WriteLine(string.Format("ID: \"{0}\" Username: \"{1}\" email: \"{2}\"", _user.Id, _user.Name, _user.Email));
                        }
                    }
                    else
                    {
                        Console.WriteLine("none");
                    }
                }
                else if (_command == "delBook")
                {
                    Console.Write("Enter book id: ");
                    if (Int32.TryParse(Console.ReadLine(), out var _bookid))
                    {
                        DBBookExec.DelById(_db, _bookid);
                    }

                }
                else if (_command == "changePubDate")
                {
                    Console.Write("Enter book id: ");
                    if (Int32.TryParse(Console.ReadLine(), out var _bookid))
                    {
                        Console.Write("Enter published date (yyyy-mm-dd):");
                        if (DateOnly.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", out var _date))
                        {
                            var _book = DBBookExec.GetById(_db, _bookid);
                            if (_book != null)
                            {
                                DBBookExec.ChangePubDate(_db, _book, _date);
                            }
                        }
                    }
                }
                else if (_command == "changeGenre")
                {
                    Console.Write("Enter book id: ");
                    if (Int32.TryParse(Console.ReadLine(), out var _bookid))
                    {
                        Console.Write("Enter genre id :");
                        if (Int32.TryParse(Console.ReadLine(), out var _genreid))
                        {
                            var _book = DBBookExec.GetById(_db, _bookid);
                            var _genre = DBGenreExec.GetById(_db, _genreid);
                            if (_book != null && _genre != null)
                            {
                                DBBookExec.GenreUpdate(_db, _book, _genre);
                            }
                        }
                    }
                }
                else if (_command == "changeAuthor")
                {
                    Console.Write("Enter book id: ");
                    if (Int32.TryParse(Console.ReadLine(), out var _bookid))
                    {
                        Console.Write("Enter author id :");
                        if (Int32.TryParse(Console.ReadLine(), out var _authorid))
                        {
                            var _book = DBBookExec.GetById(_db, _bookid);
                            var _author = DBAuthorExec.GetById(_db, _authorid);
                            if (_book != null && _author != null)
                            {
                                DBBookExec.AuthorUpdate(_db, _book, _author);
                            }
                        }
                    }
                }
                else if (_command == "addGenre")
                {
                    Console.Write("Enter genre name: ");
                    var _genrename = Console.ReadLine();
                    if (!string.IsNullOrEmpty(_genrename))
                    {
                        DBGenreExec.Add(_db, _genrename);
                    }
                }
                else if (_command == "getGenres")
                {
                    var _genres = DBGenreExec.GetAll(_db);
                    if (_genres != null)
                    {
                        foreach (var _g in _genres)
                        {
                            Console.WriteLine(string.Format("ID: \"{0}\" Genre name: \"{1}\"", _g.Id, _g.Name));
                        }
                    }
                }
                else if (_command == "addAuthor")
                {
                    Console.Write("Enter author  fullname: ");
                    var _authorname = Console.ReadLine();
                    if (!string.IsNullOrEmpty(_authorname))
                    {
                        DBAuthorExec.Add(_db, _authorname);
                    }
                }
                else if (_command == "getAuthors")
                {
                    var _authors = DBAuthorExec.GetAll(_db);
                    if (_authors != null)
                    {
                        foreach (var _a in _authors)
                        {
                            Console.WriteLine(string.Format("ID: \"{0}\" Genre name: \"{1}\"", _a.Id, _a.Name));
                        }
                    }
                }
                else if (_command == "getBooksByDates")
                {
                    Console.Write("Дата с (yyyy-mm-dd):");
                    if (DateOnly.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", out var _dateFrom))
                    {
                        Console.Write("Дата по (yyyy-mm-dd):");
                        if (DateOnly.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", out var _dateTo))
                        {
                            var _books = DBBookExec.GetByDates(_db, _dateFrom, _dateTo);
                            if (_books != null)
                            {
                                BooksPrinter(_books);
                            }
                        }
                    }
                }
                else if (_command == "getBooksByAuthor")
                {
                    Console.Write("Введите Id автора:");
                    if (Int32.TryParse(Console.ReadLine(), out var _id))
                    {
                        var _author = DBAuthorExec.GetById(_db, _id);
                        if(_author != null)
                        {
                            var _books = DBBookExec.GetByAuthor(_db, _author);
                            if( _books != null )
                            {
                                BooksPrinter(_books);
                            }
                        }
                        
                    }
                }
                else if (_command == "getBooksCountByAuthor")
                {
                    Console.Write("Введите Id автора:");
                    if (Int32.TryParse(Console.ReadLine(), out var _id))
                    {
                        var _author = DBAuthorExec.GetById(_db, _id);
                        if (_author != null)
                        {
                            var _books = DBBookExec.GetByAuthor(_db, _author);
                            if (_books != null)
                            {
                                Console.WriteLine("Количество книг этого автора: " + _books.Count());
                            }
                        }

                    }
                }
                else if (_command == "getBooksCountByGenre")
                {
                    Console.Write("Введите Id жанра:");
                    if (Int32.TryParse(Console.ReadLine(), out var _id))
                    {
                        var _genre = DBGenreExec.GetById(_db, _id);
                        if (_genre != null)
                        {
                            var _books = DBBookExec.GetByGenre(_db, _genre);
                            if (_books != null)
                            {
                                Console.WriteLine("Количество книг этого автора: " + _books.Count());
                            }
                        }
                    }
                }
                else if (_command == "findByAuthorAndName")
                {
                    Console.Write("Введите Id автора:");
                    if (Int32.TryParse(Console.ReadLine(), out var _id))
                    {
                        var _author = DBAuthorExec.GetById(_db, _id);
                        if (_author != null)
                        {
                            Console.Write("Введите полное название или его часть");
                            var _name = Console.ReadLine();
                            if(_name != null)
                            {
                                var _books = DBBookExec.GetByAuthorIdAndNamePart(_db, _author, _name);
                                if (_books != null)
                                {
                                    Console.WriteLine("True");
                                }
                                else
                                {
                                    Console.WriteLine("False");
                                }
                            }
                        }
                    }
                }
                else if (_command == "findByOwnerAndName")
                {
                    Console.Write("Введите Id пользователя:");
                    if (Int32.TryParse(Console.ReadLine(), out var _userid))
                    {
                        Console.Write("Введите Id книги:");
                        if (Int32.TryParse(Console.ReadLine(), out var _bookid))
                        {
                            Book? _book = DBBookExec.GetById(_db, _bookid);
                            User? _user = DBUserExec.GetById(_db, _userid);
                            if (_user != null && _book != null)
                            {
                                if(_book.User == _user)
                                {
                                    Console.WriteLine("True");
                                }
                                else 
                                { 
                                    Console.WriteLine("False"); 
                                }
                            }
                        }
                    }
                }
                else if (_command == "getCountBookOnUser")
                {
                    Console.Write("Введите Id пользователя:");
                    if (Int32.TryParse(Console.ReadLine(), out var _userid))
                    {
                        User? _user = DBUserExec.GetById(_db, _userid);
                        if (_user != null)
                        {
                            Book[]? _books = DBBookExec.GetByUser(_db, _user);
                            if(_books != null)
                            {
                                Console.WriteLine(string.Format("У пользователя {0} книг", _books.Count()));
                            }
                            else
                            {
                                Console.WriteLine("У пользователя 0 книг");
                            }
                        }
                    }
                }
                else if (_command == "getLastBook")
                {
                    var _book = DBBookExec.GetLast(_db);
                    if (_book != null)
                    {
                        BooksPrinter(_book);
                    }
                }
                else if (_command == "getBooksSortedByName")
                {
                    var _book = DBBookExec.GetAllSortedByNameAsc(_db);
                    if (_book != null)
                    {
                        BooksPrinter(_book);
                    }
                }
                else if (_command == "getBooksSortedByPubDateDesc")
                {
                    var _book = DBBookExec.GetAllSortedByPubDateDesc(_db);
                    if (_book != null)
                    {
                        BooksPrinter(_book);
                    }
                }
                else if(_command == "exit")
                {
                    onAir = false;
                }
            }
        }
    }
}