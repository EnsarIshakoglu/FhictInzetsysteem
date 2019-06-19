using System.Collections.Generic;
using Models;

namespace DAL.Contexts
{
    public interface ITaskContext
    {
        IEnumerable<EducationObject> GetUnitTermExecutions(int unitId);
        void AddTask(EducationObject toAddTask);
        void RemoveTask(EducationObject toRemoveTask);
        void UpdateTask(EducationObject task);
        EducationObject GetTaskById(EducationObject task);
        IEnumerable<EducationObject> GetEmployeeAssignedTasks(int empId);

    }
}