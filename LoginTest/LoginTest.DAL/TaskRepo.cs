using System;
using System.Collections.Generic;
using System.Text;
using DAL.Contexts;
using Models;

namespace DAL
{
    public class TaskRepo
    {
        private readonly ITaskContext _context;

        public TaskRepo()
        {
            _context = new TaskContext();
        }

        public void AddTask(EducationObject toAddTask)
        {
            _context.AddTask(toAddTask);
        }

        public void RemoveTask(EducationObject toRemoveTask)
        {
            _context.RemoveTask(toRemoveTask);
        }
        public IEnumerable<EducationObject> GetUnitTermExecutions(int unitId)
        {
            return _context.GetUnitTermExecutions(unitId);
        }
        public IEnumerable<EducationObject> GetAllLeftOverUnitTermExecsFromUnit(int unitId)
        {
            return _context.GetAllLeftOverUnitTermExecsFromUnit(unitId);
        }

        public IEnumerable<EducationObject> GetAllLeftOverSections()
        {
            return _context.GetAllLeftOverSections();
        }

        public IEnumerable<EducationObject> GetAllLeftOverTasksFromUnitExecId(int unitExecId)
        {
            return _context.GetAllLeftOverTasksFromUnitExecId(unitExecId);
        }

        public IEnumerable<EducationObject> GetAllLeftOverUnitsFromSection(int sectionId)
        {
            return _context.GetAllLeftOverUnitsFromSection(sectionId);
        }

        public void UpdateTask(EducationObject task)
        {
            _context.UpdateTask(task);
        }

        public void FixateTask(int taskId, int empId)
        {
            _context.FixateTask(taskId, empId);
        }

        public IEnumerable<User> GetAllEmployeesWithCompetenceForTask(int taskId)
        {
            return _context.GetAllEmployeesWithCompetenceForTask(taskId);
        }

        public EducationObject GetTaskById(EducationObject task)
        {
            return _context.GetTaskById(task);
        }

        public IEnumerable<EducationObject> GetEmployeeAssignedTasks(int empId)
        {
            return _context.GetEmployeeAssignedTasks(empId);
        }
    }
}
