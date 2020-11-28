using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreBakulin.Interfaces.Services;
using WebStoreCoreApplication.Domain.ViewModels;

namespace WebStoreCoreApplication.Controllers
{
    [Route("api/[controller]")]
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
        void IEmployeeService.AddNew(EmployeeViewModel newmodel)
        {
            employeeService.AddNew(newmodel);
            RedirectToAction(nameof(Index));
        }
        [HttpDelete("{id}")]
        void IEmployeeService.Delete(int id)
        {
            employeeService.Delete(id);
            RedirectToAction(nameof(Index));
        }
        [NonAction]
        void IEmployeeService.Commit()
        {
            employeeService.Commit();
        }
    }
}
