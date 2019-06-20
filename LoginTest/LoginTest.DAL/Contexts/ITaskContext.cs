using System.Collections.Generic;
using Models;

namespace DAL.Contexts
{
    public interface ITaskContext
    {
        IEnumerable<EducationObject> GetUnitTermExecutions(int unitId);
        void AddTask(EducationObject toAddTask);
        void RemoveTask(EducationObject toRemoveTask);
        IEnumerable<User> GetAllEmployeesWithCompetenceForTask(int taskId);
        void UpdateTask(EducationObject task);
        void FixateTask(int taskId, int empId);
        EducationObject GetTaskById(EducationObject task);
        IEnumerable<EducationObject> GetEmployeeAssignedTasks(int empId);
        IEnumerable<EducationObject> GetAllLeftOverSections();
        IEnumerable<EducationObject> GetAllLeftOverUnitTermExecsFromUnit(int unitId);
        IEnumerable<EducationObject> GetAllLeftOverUnitsFromSection(int sectionId);
        IEnumerable<EducationObject> GetAllLeftOverTasksFromUnitExecId(int unitExecId);

    }
}