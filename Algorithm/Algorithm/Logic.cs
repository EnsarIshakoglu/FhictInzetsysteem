using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class Logic
    {
        private readonly Context _context = new Context();
        public IEnumerable<EducationObject> GetAllTasks(EducationObject section)
        {
            return _context.GetAllTasks(section);
        }
    }
}
