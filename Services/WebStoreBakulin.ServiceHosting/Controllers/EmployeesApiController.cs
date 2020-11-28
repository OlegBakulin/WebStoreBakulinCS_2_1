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
        public void AddNew(EmployeeViewModel newmodel)
        {
            _EmployeesData.AddNew(newmodel);
        }

        public void Delete(int id)
        {
            _EmployeesData.Delete(id);
        }

        public void Commit()
        {
            _EmployeesData.Commit();
        }
    }
}
