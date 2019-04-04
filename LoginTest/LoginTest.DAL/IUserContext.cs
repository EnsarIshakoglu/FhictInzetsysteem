using System;
using System.Collections.Generic;
using System.Text;

namespace LoginTest.DAL
{
    public interface IUserContext
    {
        bool Login(string userName, string password);
    }
}
