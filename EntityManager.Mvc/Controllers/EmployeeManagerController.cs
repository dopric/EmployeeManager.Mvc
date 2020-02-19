using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityManager.Mvc.Data;
using EntityManager.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EntityManager.Mvc.Controllers
{
    public class EmployeeManagerController : Controller
    {

        private AppDbContext _db;
        public EmployeeManagerController(AppDbContext db)
        {
            _db = db;
        }

        public void FillCountries()
        {
            List<SelectListItem> countries = (from c in _db.Countries
                                              orderby c.Name ascending
                                              select new SelectListItem()
                                              {
                                                  Text = c.Name,
                                                  Value = c.Name
                                              }).ToList();
            ViewBag.Countries = countries;
        }
        public IActionResult List()
        {
            List<Employee> employees = _db.Employees.OrderBy(e=>e.EmployeeID).ToList();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            FillCountries();
            return View();
        }

        [HttpPost]
        public IActionResult Insert(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _db.Employees.Add(employee);
                _db.SaveChanges();
                ViewBag.Message = "Employee inserted sucessfully";
                return RedirectToAction(nameof(List));
            }
            return View(employee);
        }

        [HttpGet]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            var employee = _db.Employees.Find(id);
            return View(employee);
        }

        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
            _db.Employees.Remove(employee);
            _db.SaveChanges();
            return RedirectToAction(nameof(List));
        }

        public IActionResult Update(int id)
        {
            FillCountries();
            var employee = _db.Employees.Find(id);
            return View(employee);
        }

        [HttpPost]
        public IActionResult Update(Employee model)
        {
            FillCountries();
            if (ModelState.IsValid)
            {
                _db.Employees.Update(model);
                _db.SaveChanges();
                return RedirectToAction(nameof(List));
            }
            return View(model);
        }
    }
}