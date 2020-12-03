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
    [Route(WebApiAdress.EmployeesAdress)]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeeService
    {
        private readonly IEmployeeService _EmployeesData;

        public EmployeesApiController(IEmployeeService EmployeesData) => _EmployeesData = EmployeesData;
        
        [HttpGet]
        public IEnumerable<Employee> GetAll()
        {
            return _EmployeesData.GetAll();
        }

        [HttpGet("{id}")]
        public Employee GetByID(int id)
        {
            return _EmployeesData.GetByID(id);
        }

        [HttpPost]
        public int AddNew(Employee newmodel)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            _EmployeesData.AddNew(newmodel);
            return (int)message.StatusCode;
        }
        [NonAction]
        public void Commit()
        {
            _EmployeesData.Commit();
        }
        [HttpDelete("{id}")]
        bool IEmployeeService.Delete(int id)
        {
            return _EmployeesData.Delete(id);
        }
    }
}
