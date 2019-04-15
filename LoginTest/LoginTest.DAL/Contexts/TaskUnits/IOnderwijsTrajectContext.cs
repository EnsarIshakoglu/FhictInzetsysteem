using System;
using System.Collections.Generic;
using System.Text;
using Inzetsysteem.Models;

    namespace Inzetsysteem.DAL.Contexts
{
    interface IOnderwijsTrajectContext
    {
        List<OnderwijsTraject> GetAllTrajects();
    }
}
