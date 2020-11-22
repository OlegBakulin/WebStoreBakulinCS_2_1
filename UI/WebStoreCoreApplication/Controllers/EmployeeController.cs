using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStoreCoreApplication.Controllers.Infrastructure.Interfaces;
using WebStoreCoreApplication.ViewModels;

namespace WebStoreCoreApplication.Controllers
{
    [Route("peoples")]
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeSSService)
        {
            employeeService = employeeSSService;
        }
        [AllowAnonymous]
        [Route("idx")]
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [Route("all")]
        public IActionResult Employees()
        {
            return View(employeeService.GetAll());
        }

        [Route("{id}")]
        [Authorize(Roles = "Boss, Admin, Manager")]
        public IActionResult EmployeeDetails(int id)
        {
            var employeeviewmodel = employeeService.GetByID(id);
            if (employeeviewmodel == null) return NotFound();
            return View(employeeviewmodel);
        }

        [HttpGet]
        [Authorize(Roles = "Boss, Admin")]
        [Route("NewPeople")]
        public IActionResult NewUser()
        {
            int maxid = 0;
            foreach (var idempl in employeeService.GetAll())
            {
                maxid = idempl.Id;
            }
            return View(new EmployeeViewModel {
                Id = maxid + 1,
                IName = "Имя",
                FName = "Фамилия",
                Age = 18,
                OName = null,
                Position = "Должность"
            });

        }

        [HttpPost]
        [Authorize(Roles = "Boss, Admin")]
        [Route("NewPeople")]
        public IActionResult NewUser(EmployeeViewModel model)
        {
            var dbItem = new EmployeeViewModel();// employeeService.GetByID(model.Id);
            dbItem.Id = model.Id;
            dbItem.IName = model.IName;
            dbItem.FName = model.FName;
            dbItem.Age = model.Age;
            dbItem.OName = model.OName;
            dbItem.Position = model.Position;
            employeeService.AddNew(dbItem);
            employeeService.Commit();
            return RedirectToAction(nameof(Employees));

        }

        [HttpGet]
        [Route("edit/{id?}")]
        [Authorize(Roles = "Boss, Admin")]
        public IActionResult Edit (int? id)
        {
            if (!id.HasValue)
                return View(new EmployeeViewModel());

            var model = employeeService.GetByID(id.Value);
            if (model == null)
                return NotFound(); //404

            return View(model);
        }

        [Route("list2")]
        public IActionResult List2 ()
        {
            return View();
        }

        [Route("delete/{id}")]
        [Authorize(Roles = "Boss, Admin")]
        public IActionResult Delete (int id)
        {
            employeeService.Delete(id);
            return RedirectToAction(nameof(Employees));
        }

        [HttpPost]
        [Authorize(Roles = "Boss, Admin")]
        [Route("edit/{id?}")]
        public IActionResult Edit(EmployeeViewModel model)
        {
            if (model.Age < 18 || model.Age > 100)
            {
                ModelState.AddModelError("Age", "Ошибка возраста!");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Id > 0)
            {
                var dbItem = employeeService.GetByID(model.Id);

                //if (ReferenceEquals(dbItem, null)) return NotFound();// 404

                dbItem.IName = model.IName;
                dbItem.FName = model.FName;
                dbItem.Age = model.Age;
                dbItem.OName = model.OName;
                dbItem.Position = model.Position;
            }
            else 
            {
                employeeService.AddNew(model);
            }

            employeeService.Commit();

            return RedirectToAction(nameof(Employees));
        }
    }
}