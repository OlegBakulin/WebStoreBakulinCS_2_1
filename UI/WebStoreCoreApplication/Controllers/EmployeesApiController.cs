using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreBakulin.Interfaces.Services;
using WebStoreCoreApplication.Domain.ViewModels;
using WebStoreBakulin.Clients;
using System.Net.Http;
using WebStoreCoreApplication.Domain;
using WebStoreCoreApplication.Domain.Entities;

namespace WebStoreCoreApplication.Controllers
{
    [Route(WebApiAdress.EmployeesAdress)]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeeService
    {
        private readonly IEmployeeService employeeService;
        [HttpGet]
        public IEnumerable<Employee> GetAll()
        {
           return employeeService.GetAll();
        }
        [HttpGet("{id}")]
        public Employee GetByID(int id)
        {
            return employeeService.GetByID(id);
        }
        [HttpPost("{id?}")]
        public int AddNew(Employee newmodel)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            employeeService.AddNew(newmodel);
            RedirectToAction(nameof(Index));
            return (int)message.StatusCode;
        }
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            employeeService.Delete(id);
            RedirectToAction(nameof(Index));
            return true;
        }

        [NonAction]
        public void Commit()
        {
            employeeService.Commit();
        }

        
    }
}
