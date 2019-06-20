using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Models;

namespace Logic
{
    public class TaskLogic
    {
        private readonly TaskRepo _repo = new TaskRepo();

        public void AddTask(EducationObject toAddTask)
        {
            _repo.AddTask(toAddTask);
        }

        public void RemoveTask(EducationObject toRemoveTask)
        {
            _repo.RemoveTask(toRemoveTask);
        }

        public IEnumerable<EducationObject> GetUnitTermExecutions(int unitId)
        {
            return _repo.GetUnitTermExecutions(unitId);
        }

        public void UpdateTask(EducationObject task)
        {
             _repo.UpdateTask(task);
        }

        public EducationObject GetTaskById(EducationObject task)
        {
            return _repo.GetTaskById(task);
        }

        public IEnumerable<User> GetAllEmployeesWithCompetenceForTask(int taskId)
        {
            return _repo.GetAllEmployeesWithCompetenceForTask(taskId);
        }
        public void FixateTask(int taskId, int empId)
        {
            _repo.FixateTask(taskId, empId);
        }

        public IEnumerable<EducationObject> GetAllLeftOverUnitTermExecsFromUnit(int unitId)
        {
            return _repo.GetAllLeftOverUnitTermExecsFromUnit(unitId);
        }

        public IEnumerable<EducationObject> GetAllLeftOverSections()
        {
            return _repo.GetAllLeftOverSections();
        }

        public IEnumerable<EducationObject> GetAllLeftOverTasksFromUnitExecId(int unitExecId)
        {
            return _repo.GetAllLeftOverTasksFromUnitExecId(unitExecId);
        }

        public IEnumerable<EducationObject> GetAllLeftOverUnitsFromSection(int sectionId)
        {
            return _repo.GetAllLeftOverUnitsFromSection(sectionId);
        }

        public IEnumerable<EducationObject> GetEmployeeAssignedTasks(int empId)
        {
            return _repo.GetEmployeeAssignedTasks(empId);
        }
    }
}
