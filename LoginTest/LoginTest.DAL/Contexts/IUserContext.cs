using System;
using System.Collections.Generic;
using System.Text;
using FHICTDeploymentSystem.Models;

namespace FHICTDeploymentSystem.DAL
{
    public interface IUserContext
    {
        bool Login(User user);
        IEnumerable<string> GetUserRoles(User user);
        int GetUserId(User user);
    }
}
