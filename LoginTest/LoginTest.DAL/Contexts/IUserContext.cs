using System;
using System.Collections.Generic;
using System.Text;
using Inzetsysteem.Models;

namespace Inzetsysteem.DAL
{
    public interface IUserContext
    {
        bool Login(User user);
        IEnumerable<string> GetUserRoles(User user);
        int GetUserID(User user);
    }
}
