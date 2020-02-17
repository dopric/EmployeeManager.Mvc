﻿using System;
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
    }
}