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
    [Route(WebApiAdress.Employees)]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeeService
    {
        private readonly IEmployeeService employeeService;
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return employeeService.Get();
        }
        [HttpGet("{id}")]
        public Employee GetById(int id)
        {
            return employeeService.GetById(id);
        }
        [HttpPost("{id?}")]
        public int Add(Employee newmodel)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            employeeService.Add(newmodel);
            RedirectToAction(nameof(Index));
            return (int)message.StatusCode;
        }
        
        public void Edit(Employee employee)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            employeeService.Edit(employee);
            RedirectToAction(nameof(Index));
            
        }
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            employeeService.Delete(id);
            RedirectToAction(nameof(Index));
            return true;
        }

        
        

        

        [NonAction]
        public void SaveChanges()
        {
            employeeService.SaveChanges();
        }
    }
    
}
