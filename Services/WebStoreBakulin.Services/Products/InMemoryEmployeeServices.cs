using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreBakulin.Interfaces.Services;
using WebStoreCoreApplication.Domain.Entities;
using WebStoreCoreApplication.Domain.ViewModels;

namespace WebStoreCoreApplication.Controllers.Infrastructure.Services
{
    public class InMemoryEmployeeServices : IEmployeeService
    {
        private readonly List<Employee> _employees = new List<Employee>
        {
            new Employee
            {
                Id = 1,
                Name = "Петр",
                Surname = "Петров",
                Patronymic = "Петрович",
                Age = 33,
                EmployementDate = DateTime.Now.Subtract(TimeSpan.FromDays(300 * 7))
            },
            new Employee
            {
                Id = 2,
                Name = "Фёдр",
                Surname = "Фёдоров",
                Patronymic = "Фёдорович",
                Age = 25,
                EmployementDate = DateTime.Now.Subtract(TimeSpan.FromDays(300 * 7))
            }
        }; 
        
        public IEnumerable<Employee> GetAll()
        {
            return _employees;
        }

        public Employee GetByID(int id)
        {
            return _employees.FirstOrDefault(e => e.Id.Equals(id));
        }
        
        public int AddNew(Employee newmodel)
        {
            if (newmodel is null)
                    throw new ArgumentNullException(nameof(newmodel));
                
            if (_employees.Contains(newmodel)) return newmodel.Id;

            newmodel.Id = _employees.Max(e => e.Id) + 1;
                _employees.Add(newmodel);
                return newmodel.Id;
        }

        public void Commit() { }

        public bool Delete(int id)
        {
            var employee = GetByID(id);
            if (employee is null) return false;
            _employees.Remove(employee);
            return true;
        }
    }
}
