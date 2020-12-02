using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreBakulin.Interfaces.Services;
using WebStoreBakulin.Services.Data;
using WebStoreCoreApplication.Domain.Entities;
using WebStoreCoreApplication.Domain.ViewModels;

namespace WebStoreCoreApplication.Controllers.Infrastructure.Services
{
    public class InMemoryEmployeeServices : IEmployeeService
    {
        private readonly List<Employee> _employees = TestData.Employees;
        /*
        new List<Employee>
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
        */

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
                
            if (_employees.Contains(newmodel)) return newmodel.Id;

            newmodel.Id = _employees.Max(e => e.Id) + 1;
                _employees.Add(newmodel);
                return newmodel.Id;
        }

        public bool Delete(int id)
        {
            return _employees.RemoveAll(e => e.Id == id) > 0;
            /*
            var employee = GetByID(id);
            if (employee is null) return false;
            _employees.Remove(employee);
            return true;
            */
        }

        public void Edit(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            if (_employees.Contains(employee)) return;

            var db_employee = GetById(employee.Id);
            if (db_employee is null) return;

            db_employee.Name = employee.Name;
            db_employee.Surname = employee.Surname;
            db_employee.Patronymic = employee.Patronymic;
            db_employee.Age = employee.Age;
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
