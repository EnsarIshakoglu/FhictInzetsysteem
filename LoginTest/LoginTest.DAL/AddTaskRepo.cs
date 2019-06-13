﻿using System;
using System.Collections.Generic;
using System.Text;
using DAL.Contexts;
using Models;

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

        public IEnumerable<EducationObject> GetUnitTermExecutions(int unitId)
        {
            return _context.GetUnitTermExecutions(unitId);
        }
    }
}
