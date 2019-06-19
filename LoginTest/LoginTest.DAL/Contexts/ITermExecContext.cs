using System.Collections.Generic;
using Models;

namespace DAL.Contexts
{
    public interface ITermExecContext
    {
        void AddTermExec(EducationObject termExec);
        void DeleteTermExec(EducationObject termExec);
        void EditTermExec(EducationObject termExec);
        IEnumerable<EducationObject> GetAllTermExecs();
    }
}