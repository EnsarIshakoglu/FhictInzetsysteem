using System;
using System.Collections.Generic;
using System.Text;
using LoginTest.Models;

namespace LoginTest.DAL
{
    public interface IUserContext
    {
        bool Login(User user);
        List<string> GetUserRoles(User user);
    }
}
