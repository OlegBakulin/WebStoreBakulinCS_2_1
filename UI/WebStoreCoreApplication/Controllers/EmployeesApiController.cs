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

namespace WebStoreCoreApplication.Controllers
{
    [Route(WebApiAdress.EmployeesAdress)]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeeService
    {
        private readonly IEmployeeService employeeService;
        [HttpGet]
        IEnumerable<EmployeeViewModel> IEmployeeService.GetAll()
        {
           return employeeService.GetAll();
        }
        [HttpGet("{id}")]
        EmployeeViewModel IEmployeeService.GetByID(int id)
        {
            return employeeService.GetByID(id);
        }
        [HttpPost("{id?}")]
        int IEmployeeService.AddNew(EmployeeViewModel newmodel)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            employeeService.AddNew(newmodel);
            RedirectToAction(nameof(Index));
            return (int)message.StatusCode;
        }
        [HttpDelete("{id}")]
        bool IEmployeeService.Delete(int id)
        {
            employeeService.Delete(id);
            RedirectToAction(nameof(Index));
            return true;
        }

        [NonAction]
        void IEmployeeService.Commit()
        {
            employeeService.Commit();
        }

        
    }
}
