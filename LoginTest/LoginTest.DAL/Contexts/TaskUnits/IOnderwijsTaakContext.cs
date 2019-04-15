using System;
using System.Collections.Generic;
using System.Text;
using Inzetsysteem.Models;

namespace Inzetsysteem.DAL.Contexts
{
    interface IOnderwijsTaakContext
    {
        List<OnderwijsTaak> GetAllTaken();
        List<OnderwijsTaak> GetAllTakenByOnderdeel(OnderwijsOnderdeel onderdeel);
    }
}
