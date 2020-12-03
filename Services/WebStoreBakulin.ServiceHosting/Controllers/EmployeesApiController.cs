using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebStoreBakulin.Interfaces.Services;
using WebStoreCoreApplication.Domain.ViewModels;
using WebStoreCoreApplication.Domain;
using WebStoreCoreApplication.Domain.Entities;

namespace WebStoreCoreApplication.Controllers
{
    [Route(WebApiAdress.Employees)]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeeService
    {
        private readonly IEmployeeService _EmployeesData;

        public EmployeesApiController(IEmployeeService EmployeesData) => _EmployeesData = EmployeesData;
        
        [HttpGet]
        IEnumerable<Employee> IEmployeeService.Get()
        {
            return _EmployeesData.Get();
        }

        [HttpGet("{id}")]
        public Employee GetById(int id)
        {
            return _EmployeesData.GetById(id);
        }

        [HttpPost]
        public int Add(Employee newmodel)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            _EmployeesData.Add(newmodel);
            return (int)message.StatusCode;
        }
        [NonAction]
        void IEmployeeService.SaveChanges()
        {
            _EmployeesData.SaveChanges();
        }

        [HttpDelete("{id}")]
        bool IEmployeeService.Delete(int id)
        {
            return _EmployeesData.Delete(id);
        }

        void IEmployeeService.Edit(Employee employee)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            _EmployeesData.Edit(employee);
        }

        
    }
}
