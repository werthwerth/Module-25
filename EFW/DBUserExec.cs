using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Module_25.EFW.Program;

namespace Module_25.EFW
{
    internal class DBUserExec
    {
        protected internal static void Add(DB _db, string _name, string _email)
        {
            User _user = new User();
            _user.Var(_name, _email);
            _db.context.Users.Add( _user );
            _db.context.SaveChanges();
        }
        protected internal static User? GetById(DB _db, int _id)
        {
            User? _user = _db.context.Users.FirstOrDefault(u => u.Id == _id);
            if (_user == null)
            {
                return null;
            }
            else
            {
                return _user;
            }
        }
        protected internal static User[]? GetAll(DB _db)
        {
            User[]? _users = _db.context.Users.ToArray();
            if (_users == null)
            {
                return null;
            }
            else
            {
                return _users;
            }
        }
    }
}
