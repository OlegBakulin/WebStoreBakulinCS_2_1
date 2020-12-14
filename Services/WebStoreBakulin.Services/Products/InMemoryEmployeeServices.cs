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
        
        public IEnumerable<Employee> Get()
        {
            return _employees;
        }

        public Employee GetById(int id)
        {
            return _employees.FirstOrDefault(e => e.Id.Equals(id));
        }
        
        public int Add(Employee newmodel)
        {
            if (newmodel is null)
                    throw new ArgumentNullException(nameof(newmodel));
                
            newmodel.Id = _employees.Max(e => e.Id) + 1;
                _employees.Add(newmodel);
                return newmodel.Id;
        }

        

        public bool Delete(int id)
        {
            var employee = GetById(id);
            if (employee is null) return false;
            _employees.Remove(employee);
            return true;
        }

        public void Edit(Employee employee)
        {
            _employees.Contains(employee);
            
        }

        public void SaveChanges()
        {
            
        }
    }
}
