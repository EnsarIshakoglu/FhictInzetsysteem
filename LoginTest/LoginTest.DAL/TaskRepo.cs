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

        public void UpdateTask(EducationObject task)
        {
            _context.UpdateTask(task);
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
