using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebStoreBakulin.Interfaces.Services;
using WebStoreCoreApplication.Domain.ViewModels;

namespace WebStoreCoreApplication.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeeService
    {
        private readonly IEmployeeService _EmployeesData;

        public EmployeesApiController(IEmployeeService EmployeesData) => _EmployeesData = EmployeesData;
        
        ////
        public IEnumerable<EmployeeViewModel> GetAll()
        {
            return _EmployeesData.GetAll();
        }

        ////
        public EmployeeViewModel GetByID(int id)
        {
            return _EmployeesData.GetByID(id);
        }

        ////
        public int AddNew(EmployeeViewModel newmodel)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            _EmployeesData.AddNew(newmodel);
            return (int)message.StatusCode;
        }

        public void Commit()
        {
            _EmployeesData.Commit();
        }

        bool IEmployeeService.Delete(int id)
        {
            return _EmployeesData.Delete(id);
        }
    }
}
