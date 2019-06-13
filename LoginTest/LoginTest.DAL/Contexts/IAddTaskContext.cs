using System.Collections.Generic;
using Models;

namespace DAL.Contexts
{
    public interface IAddTaskContext
    {
        IEnumerable<EducationObject> GetUnitTermExecutions(int unitId);
        void AddTask(EducationObject toAddTask);
    }
}