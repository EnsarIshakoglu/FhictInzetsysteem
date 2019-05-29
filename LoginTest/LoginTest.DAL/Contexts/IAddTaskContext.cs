using System.Collections.Generic;
using FHICTDeploymentSystem.Models;

namespace DAL.Contexts
{
    public interface IAddTaskContext
    {
        IEnumerable<EducationObject> GetUnitTermExecutions(int unitId);
        void AddTask(EducationObject toAddTask);
        void RemoveTask(EducationObject toRemoveTask);
    }
}