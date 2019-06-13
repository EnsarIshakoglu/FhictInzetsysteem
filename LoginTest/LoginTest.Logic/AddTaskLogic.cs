using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Models;

namespace Logic
{
    public class AddTaskLogic
    {
        private readonly AddTaskRepo _repo = new AddTaskRepo();

        public void AddTask(EducationObject toAddTask)
        {
            _repo.AddTask(toAddTask);
        }

        public IEnumerable<EducationObject> GetUnitTermExecutions(int unitId)
        {
            return _repo.GetUnitTermExecutions(unitId);
        }
    }
}
