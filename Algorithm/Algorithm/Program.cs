using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    class Program
    {
        private static Logic _logic = new Logic();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to out algorithm program!");

            var tasks = GetAllTasks();

            foreach (var task in tasks)
            {

            }

            Console.ReadKey();
        }

        static IEnumerable<EducationObject> GetAllTasks()
        {
            return _logic.GetAllTasks(new EducationObject
            {
                Id = 3
            });
        }
    }
}
