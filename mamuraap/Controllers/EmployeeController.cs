using mamuraap.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mamuraap.Controllers
{
    public class EmployeeController : Controller
    {
        private ApplicationContext context;
        public EmployeeController(ApplicationContext _context)
        {
            context = _context;
        }
        public IActionResult Index()
        {

            var emp = context.Employees.ToList();
            return View(emp);
        }

        public IActionResult depart()
        {
            var emps = context.Departments.ToList();
            return View(emps);
        }

        public IActionResult GetEmployee(int Id)
        {
            var smp = context.Employees.Where(e => e.Department.Id == Id);
                return View(smp);
        }

        public IActionResult GetDetails(int Id)
        {
            var det = context.Employees.SingleOrDefault(e => e.Id == Id);
            return View(det);
        }
    }
}
