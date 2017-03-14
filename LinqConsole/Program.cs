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

            var result = db.Employees
                        .Where(x => x.Gender == "Male")
                        .OrderByDescending(x => x.Salary)
                        .Take(15);
            foreach (var e in result)
            {
                Console.WriteLine(e.FirstName + " " + e.LastName + "\t" + e.Salary);

            }

           var gps = Worker.GetAllWorkers().GroupBy(wrk => wrk.Department).OrderBy(grp => grp.Key).Select(g => new { Key = g.Key, Worker = g.OrderBy(x=>x.Name) });
            
            //it can also be written as follow
             var groups = from wrker in Worker.GetAllWorkers()
                         group wrker by wrker.Department into egroup
                         orderby egroup.Key
                         select new
                         {
                             key = egroup.Key,
                             Worker = egroup.OrderBy(x => x.Name)
                         };
            foreach (var grp in gps)
            {
                Console.WriteLine("====================================================");
                Console.WriteLine(grp.Key + "\t"+grp.Worker.Count());
                Console.WriteLine("====================================================");

                foreach (var emp in grp.Worker)
                {
                    Console.WriteLine(emp.Name +"\t" + emp.Salary);
                }
            }

        }

    }
}

