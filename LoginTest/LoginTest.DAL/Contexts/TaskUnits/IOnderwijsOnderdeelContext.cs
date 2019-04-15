using System;
using System.Collections.Generic;
using System.Text;
using Inzetsysteem.Models;

namespace Inzetsysteem.DAL.Contexts
{
    public interface IOnderwijsOnderdeelContext
    {
        List<OnderwijsOnderdeel> GetAllOnderdelen();
        List<OnderwijsOnderdeel> GetAllOnderdelenByEenheid(OnderwijsEenheid eenheid);
    }
}
