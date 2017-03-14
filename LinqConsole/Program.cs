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


            Console.WriteLine();

            Console.WriteLine("GroupBy Mulitple Keys");

            var MultipleKeygroups = Worker.GetAllWorkers().GroupBy(x => new { x.Department, x.Gender }).OrderBy(g => g.Key.Department)
                                    .ThenBy(g => g.Key.Gender)
                                    .Select(individualgrp => new { Department = individualgrp.Key.Department ,
                                                        Gender = individualgrp.Key.Gender,
                                                        Employees = individualgrp.OrderBy(x=>x.Name)
            
                                                   });

            //the above query can also be written in sql 

            //var MultipleKeyGrps = from employee in Worker.GetAllWorkers()
            //                      group employee by new { employee.Department, employee.Gender }
            //                      into eGrps
            //                      orderby eGrps.Key.Department, eGrps.Key.Department
            //                      select
            //                      (new
            //                      {
            //                          Gender = eGrps.Key.Gender,
            //                          Department = eGrps.Key.Department,
            //                          Employees = eGrps.OrderBy(x=>x.Name)
            //                      }
            //                      );
                                 
                                  
                                  

            foreach(var grp in MultipleKeygroups)
            {
                Console.WriteLine("====================================================");
                Console.WriteLine("Department {0}\t|Gender : {1}\t|TotalEmployees:{2}", grp.Department,grp.Gender,grp.Employees.Count());
                Console.WriteLine("====================================================");
                foreach (var emp in grp.Employees)
                {
                    Console.WriteLine(emp.Department+ "\t"+emp.Gender+"\t" +emp.Name);
                }
            }


        }

    }
}

