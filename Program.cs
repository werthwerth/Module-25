using Microsoft.EntityFrameworkCore;
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
            public string? Author { get; set; }
            public string? Genre { get; set; }
        }

        static void Main()
        {
            DB _db = new DB();
            Console.WriteLine("Started");
            bool onAir = true;
            string? _command;
            string? _username;
            int _userid;
            string? _bookname;
            string? _genre;
            string? _author;
            int _bookid;
            string? _email;
            DateOnly _date;
            while (onAir)
            {
                Console.WriteLine("Command (addUser, addBook, bookToUser, getBooks, getUsers, delBook, changePubDate, changeAuthor, changeGenre, exit):");
                _command = Console.ReadLine();
                if(_command == "addUser")
                {

                    Console.Write("Enter fullname:");
                    _username = Console.ReadLine();
                    Console.Write("Enter email:");
                    _email = Console.ReadLine();
                    if(!String.IsNullOrEmpty(_username) && !String.IsNullOrEmpty(_email))
                    {
                        DBUserExec.DBUserAdd(_db, _username, _email);
                    }
                }
                else if(_command == "addBook")
                {
                    Console.Write("Enter bookname:");
                    _bookname = Console.ReadLine();
                    Console.Write("Enter published date (yyyy-mm-dd):");
                    DateOnly.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", out _date);
                    if(!String.IsNullOrEmpty(_bookname))
                    {
                        DBBookExec.DBBookAdd(_db, _bookname, _date);
                    }
                }
                else if(_command == "bookToUser")
                {
                    Console.Write("Enter user id:");
                    if (Int32.TryParse(Console.ReadLine(), out _userid))
                    {
                        Console.Write("Enter book id:");
                        if (Int32.TryParse(Console.ReadLine(), out _bookid))
                        {
                            User? _user = DBUserExec.DBUserGetById(_db, _userid);
                            Book? _book = DBBookExec.DBBookGetById(_db, _bookid);
                            if (_user != null && _book != null)
                            {
                                DBBookExec.DBBookAddToUser(_db, _book, _user);
                            }
                        }   
                    }

                }
                else if (_command == "getBooks")
                {
                    Book[]? _books = DBBookExec.DBBookGetAll(_db);
                    if (_books != null)
                    {
                        foreach (var _book in _books)
                        {
                            if (_book.User != null)
                            {
                                Console.WriteLine("ID: \"" + _book.Id + "\" Bookname: \"" + _book.Name + "\" publish date: \"" + _book.PublishedDate + "\" current reader: \"" + _book.User.Name + "\" genre: \"" + _book.Genre + "\" author: \"" + _book.Author + "\"");
                            }
                            else
                            {
                                Console.WriteLine("ID: \"" + _book.Id + "\" Bookname: \"" + _book.Name + "\" publish date: \"" + _book.PublishedDate + "\" current reader: \"none\" genre: \"" + _book.Genre + "\" author: \"" + _book.Author + "\"");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("empty");
                    }
                }
                else if(_command == "getUsers")
                {
                    User[]? _userList = DBUserExec.DBUserGetAll(_db);
                    if (_userList != null)
                    {
                        foreach(var _user in _userList)
                        {
                            Console.WriteLine("ID: \"" + _user.Id + "\" Username: \"" + _user.Name + "\" email: \"" + _user.Email + "\"");
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
                    if (Int32.TryParse(Console.ReadLine(), out _bookid))
                    {
                        DBBookExec.DBBookDelById(_db, _bookid);
                    }

                }
                else if (_command == "changePubDate")
                {
                    Console.Write("Enter book id: ");
                    if(Int32.TryParse(Console.ReadLine(), out _bookid))
                    {
                        Console.Write("Enter published date (yyyy-mm-dd):");
                        if (DateOnly.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", out _date))
                        {
                            Book? _book = DBBookExec.DBBookGetById(_db, _bookid);
                            if (_book != null)
                            {
                                DBBookExec.DBBookChangePubDate(_db, _book, _date);
                            }
                        }
                    }  
                }
                else if (_command == "changeGenre")
                {
                    Console.Write("Enter book id: ");
                    if (Int32.TryParse(Console.ReadLine(), out _bookid))
                    {
                        Console.Write("Enter genre:");
                        _genre = Console.ReadLine();
                        if (!String.IsNullOrEmpty(_genre))
                        {
                            Book? _book = DBBookExec.DBBookGetById(_db, _bookid);
                            if (_book != null)
                            {
                                DBBookExec.DBBookGenreUpdate(_db, _book, _genre);
                            }
                        }
                    }
                }
                else if (_command == "changeAuthor")
                {
                    Console.Write("Enter book id: ");
                    if (Int32.TryParse(Console.ReadLine(), out _bookid))
                    {
                        Console.Write("Enter author:");
                        _author = Console.ReadLine();
                        if (!String.IsNullOrEmpty(_author))
                        {
                            Book? _book = DBBookExec.DBBookGetById(_db, _bookid);
                            if (_book != null)
                            {
                                DBBookExec.DBBookAuthorUpdate(_db, _book, _author);
                            }
                        }
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