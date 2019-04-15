using System;
using System.Collections.Generic;
using System.Text;
using Inzetsysteem.Models;

namespace Inzetsysteem.DAL.Contexts
{
    public interface IOnderwijsEenheidContext
    {
        List<OnderwijsEenheid> GetAllEenheden();
        List<OnderwijsEenheid> GetAllEenhedenByTraject(OnderwijsTraject traject);
    }
}
