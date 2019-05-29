using System;
using System.Collections.Generic;
using System.Text;
using DAL.Contexts;
using FHICTDeploymentSystem.Models;

namespace DAL
{
    public class AddTaskRepo
    {
        private readonly IAddTaskContext _context;

        public AddTaskRepo()
        {
            _context = new AddTaskContext();
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
    }
}
