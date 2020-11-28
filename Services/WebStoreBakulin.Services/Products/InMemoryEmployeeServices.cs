using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreBakulin.Interfaces.Services;
using WebStoreCoreApplication.Domain.ViewModels;

namespace WebStoreCoreApplication.Controllers.Infrastructure.Services
{
    public class InMemoryEmployeeServices : IEmployeeService
    {
        private readonly List<EmployeeViewModel> _employees = new List<EmployeeViewModel>
        {
            new EmployeeViewModel
            {
                Id = 1,
                IName = "Петр",
                FName = "Петров",
                OName = "Петрович",
                Age = 33,
                Position ="BOSS"
            },
            new EmployeeViewModel
            {
                Id = 2,
                IName = "Фёдр",
                FName = "Фёдоров",
                OName = "Фёдорович",
                Age = 25,
                Position ="Программист"}
        };

        int IEmployeeService.AddNew(EmployeeViewModel newmodel)
        {
            try
            {
                newmodel.Id = _employees.Max(e => e.Id) + 1;
                _employees.Add(newmodel);
                return 200;
            }
            catch
            {
                return 404;
            }
        }

        public void Commit()
        {
            //
        }

        bool IEmployeeService.Delete(int id)
        {
            var employee = GetByID(id);
            if (employee is null) return false;
            _employees.Remove(employee);
            return true;
        }

        public IEnumerable<EmployeeViewModel> GetAll()
        {
            return _employees;
        }

        public EmployeeViewModel GetByID(int id)
        {
            return _employees.FirstOrDefault(e => e.Id.Equals(id));
        }

        
    }
}
