﻿using System.Collections.Generic;
using Models;

namespace DAL.Contexts
{
    public interface IPreferenceContext
    {
        IEnumerable<EducationObject> GetAllSections();
        void AddTaskPreference(EducationObject task, int priority, int userId);
        void UpdateTaskPreference(EducationObject task, int priority, int userId);
        Preference CheckTaskPreference(EducationObject task, int userId);
        IEnumerable<EducationObject> GetAllUnits(int SectionId);
        IEnumerable<EducationObject> GetAllTasks(int unitExecId);
        IEnumerable<EducationObject> GetTasksFromSection(EducationObject Section);

        IEnumerable<EducationObject> GetTasksFromUnit(int unitId);
        IEnumerable<EducationObject> GetTasksFromUnitExecution(int unitExecutionId);
    }
}