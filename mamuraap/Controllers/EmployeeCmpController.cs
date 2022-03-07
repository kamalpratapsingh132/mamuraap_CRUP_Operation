using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mamuraap.Models;
using mamuraap.Models.ViewModel;
using Microsoft.AspNetCore.Http;

namespace mamuraap.Controllers
{
    public class EmployeeCmpController : Controller
    {
        private readonly ApplicationContext context;

        public EmployeeCmpController(ApplicationContext _Context)
        {
            context = _Context;
        }

        public IActionResult EIndex()
        {
            var emps = context.Employees.ToList();
            var depts = context.Departments.ToList();

            EmployeeDepartmentModel model = new EmployeeDepartmentModel();
            model.Employee = emps;
            model.Departments = depts;
            return View(model);
        }

        public IActionResult Combinedata()
        {
            var emps = (from e in context.Employees
                        join
                        d in context.Departments
                        on e.Department.Id equals d.Id
                        select new EmployeeViewModel()
                        {
                            Id = e.Id,
                            Name = e.Name,
                            Age = e.Age,
                            Email = e.Email,
                            salary = e.salary,
                            Department = d.name
                        }).ToList();

                   return View(emps);
        }


        public IActionResult Create()
        {
            var depts = context.Departments.ToList();
            ViewBag.depts = depts;
            return View();
        }

        [HttpPost]

        public IActionResult Create(EmployeeCreateViewModel model)

        {
            int Id = model.Department;
            var dep = context.Departments.SingleOrDefault(e => e.Id == Id);

            Employee emp = new Employee()
            {
                Name = model.Name,
                Age = model.Age,
                Email = model.Email,
                salary = model.salary,
                Department = dep

            };
            context.Employees.Add(emp);
            context.SaveChanges();
            return RedirectToAction("Combinedata");

        }

        //public IActionResult Create(string Name, string Age, string Email, string Salary, string Department)

        //{
        //    int Id = Convert.ToInt32(Department);
        //    var dep = context.Departments.SingleOrDefault(e => e.Id == Id);

        //    Employee emp = new Employee()
        //    {
        //        Name = Name,
        //        Age = Convert.ToInt32(Age),
        //        Email = Email,
        //        salary = Convert.ToInt32(Salary),
        //        Department = dep

        //    };
        //    context.Employees.Add(emp);
        //    context.SaveChanges();
        //    return RedirectToAction("Combinedata");

        //}

        //public IActionResult Create(IFormCollection collection)
        //{
        //    int Id = Convert.ToInt32(collection["Department"]);
        //    var dep = context.Departments.SingleOrDefault(e => e.Id == Id);

        //     Employee emp = new Employee()
        //     {
        //         Name = collection["Name"],
        //         Age = Convert.ToInt32(collection["Age"]),
        //         Email = collection["Email"],
        //        salary = Convert.ToInt16(collection["Salary"]),
        //         Department = dep

        //     };
        //     context.Employees.Add(emp);
        //     context.SaveChanges();
        //      return RedirectToAction("Combinedata"); 

        //}

        public IActionResult Delete(int id)
        {
            var emp = context.Employees.SingleOrDefault(e => e.Id == id);

            if (emp != null)
            {
                context.Employees.Remove(emp);
                context.SaveChanges();
                TempData["error"] = "Employee deleted !";
                return RedirectToAction("EIndex");

            }
            else
            {
                TempData["error"] = "Employee Not Found !..";
                return RedirectToAction("EIndex");
            }

        }

        public IActionResult Update(int id)
        {
            var emp = context.Employees.SingleOrDefault(e => e.Id == id);
            if(emp!=null)
            {
                var depts = context.Departments.ToList();
                ViewBag.depts = depts;
                var model = new EmployeeCreateViewModel()
                {
                    Id = emp.Id,
                    Name = emp.Name,
                    Age = emp.Age,
                    Email = emp.Email,
                    salary = emp.salary,
                    Department = emp.Department.Id
                };
                return View(model);
            }
            else
            {
                TempData["error"] = "Employee Not Found !..";
                return RedirectToAction("EIndex");
            }
        }

        [HttpPost]

        public IActionResult Update(EmployeeCreateViewModel model)
        {
            int Id = model.Department;
            var dep = context.Departments.SingleOrDefault(e => e.Id == Id);

            Employee emp = new Employee()
            {
                Id= model.Id,
                Name = model.Name,
                Age = model.Age,
                Email = model.Email,
                salary = model.salary,
                Department = dep

            };
            context.Employees.Update(emp);
            context.SaveChanges();
            return RedirectToAction("Combinedata");
        }

        public IActionResult CreateDepartment()
        {
            return View();
        }

        [HttpPost]

        public IActionResult CreateDepartment(Department department)
        {
            context.Departments.Add(department);
            context.SaveChanges();
            return RedirectToAction("ShowDepartments ");
        }

        public IActionResult ShowDepartments()
        { 
            return View(context.Departments.ToList());
        }

        public IActionResult DeleteDepartment(int id)
        {
            var emp = context.Departments.SingleOrDefault(e => e.Id == id);

            if (emp != null)
            {
                context.Departments.Remove(emp);
                context.SaveChanges();
                TempData["error"] = "Employee deleted !";
                return RedirectToAction("ShowDepartments");

            }
            else
            {
                TempData["error"] = "Employee Not Found !..";
                return RedirectToAction("ShowDepartments");
            }

        }
    } 


}
