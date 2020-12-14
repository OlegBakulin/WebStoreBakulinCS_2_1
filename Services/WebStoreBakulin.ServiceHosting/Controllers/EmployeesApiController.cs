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
        public IEnumerable<Employee> Get() => _EmployeesData.Get();


        [HttpGet("{id}")]
        public Employee GetById(int id)
        {
            return _EmployeesData.GetById(id);
        }

        [HttpPost]
        public int Add([FromBody] Employee employee)
        {
            var id = _EmployeesData.Add(employee);
            SaveChanges();
            return id;
        }
        [NonAction]
        public void SaveChanges() => _EmployeesData.SaveChanges();

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var result = _EmployeesData.Delete(id);
            SaveChanges();
            return result;
        }

        [HttpPut /*("{id}")*/]
        public void Edit( /*int id, */ Employee employee)
        {
            _EmployeesData.Edit(employee);
            SaveChanges();
        }


    }
}
