using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeDataContext db = new EmployeeDataContext();

            var result= db.Employees
                        .Where(x => x.Gender == "Male")
                        .OrderByDescending(x => x.Salary)
                        .Take(15);
            foreach (var e in result)
            {
                Console.WriteLine(e.FirstName + " " + e.LastName + "\t" + e.Salary);
            }
        }
    }
}
