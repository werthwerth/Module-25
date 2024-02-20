using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
                Id = System.Guid.NewGuid();
                Name = _Name;
                Email = _Email;

            }
            [Key]
            public System.Guid Id { get; set; }
            public string? Name { get; set; }
            public string? Email { get; set; }
        }

        public class Book
        {
            protected internal void Var(string _Name, DateOnly _PublishedDate, User? _User)
            {
                Id = System.Guid.NewGuid();
                Name = _Name;
                PublishedDate = _PublishedDate;
                User = _User;
            }
            protected internal void Var(string _Name, DateOnly _PublishedDate)
            {
                Id = System.Guid.NewGuid();
                Name = _Name;
                PublishedDate = _PublishedDate;
            }
            [Key]
            public System.Guid Id { get; set; }
            public string? Name { get; set; }
            public DateOnly? PublishedDate { get; set; }
            public User? User { get; set; }
        }

        static void Main()
        {
            DB _db = new DB();
            Console.WriteLine("Started");
            bool onAir = true;
            string? _command;
            string? _username;
            string? _bookname;
            string? _email;
            string? _rawDate;
            DateOnly _date;
            while (onAir)
            {
                Console.WriteLine("Command (addUser, addBook, bookToUser, getBooks, getUsers, exit):");
                _command = Console.ReadLine();
                if(_command == "addUser")
                {

                    Console.WriteLine("Enter fullname:");
                    _username = Console.ReadLine();
                    Console.WriteLine("Enter email:");
                    _email = Console.ReadLine();
                    if(!String.IsNullOrEmpty(_username) && !String.IsNullOrEmpty(_email))
                    {
                        DBUserExec.DBUserAdd(_db, _username, _email);
                    }
                }
                else if(_command == "addBook")
                {
                    Console.WriteLine("Enter bookname:");
                    _bookname = Console.ReadLine();
                    Console.WriteLine("Enter published date (yyyy-mm-dd):");
                    _rawDate = Console.ReadLine();
                    DateOnly.TryParseExact(_rawDate, "yyyy-MM-dd", out _date);
                    if(!String.IsNullOrEmpty(_bookname))
                    {
                        DBBookExec.DBBookAdd(_db, _bookname, _date);
                    }
                }
                else if(_command == "bookToUser")
                {
                    Console.WriteLine("Enter user fullname:");
                    _username = Console.ReadLine();
                    Console.WriteLine("Enter bookname:");
                    _bookname = Console.ReadLine();
                    if (!String.IsNullOrEmpty(_username) && !String.IsNullOrEmpty(_bookname))
                    {
                        User? _user = DBUserExec.DBUserGetByName(_db, _username);
                        Book[]? _bookList = DBBookExec.DBUserGetByName(_db, _bookname);
                        if (_user != null && _bookList != null && _bookList.Count() > 0)
                        {
                            foreach (var _book in _bookList)
                            {
                                DBBookExec.DBBookAddToUser(_db, _book, _user);
                            }
                        }
                    }
                }
                else if (_command == "getBooks")
                {
                    Book[]? _books = DBBookExec.DBUserGetAll(_db);
                    if (_books != null)
                    {
                        foreach (var _book in _books)
                        {
                            if (_book.User != null)
                            {
                                Console.WriteLine("Bookname: " + _book.Name + " publish date: " + _book.PublishedDate + " current reader: " + _book.User.Name);
                            }
                            else
                            {
                                Console.WriteLine("Bookname: " + _book.Name + " publish date: " + _book.PublishedDate + " current reader: none");
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
                            Console.WriteLine("Username: " + _user.Name + " email: " + _user.Email);
                        }
                    }
                    else
                    {
                        Console.WriteLine("none");
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