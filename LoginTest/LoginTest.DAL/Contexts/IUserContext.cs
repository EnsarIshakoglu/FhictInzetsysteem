using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace DAL.Contexts
{
    public interface IUserContext
    {
        bool Login(User user);
        IEnumerable<string> GetUserRoles(User user);
        int GetUserId(User user);
        User GetAllUserData(int userId);
    }
}
