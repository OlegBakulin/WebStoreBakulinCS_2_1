using System;
using System.Collections.Generic;
using WebStoreCoreApplication.Domain.Entities;
using WebStoreBakulin.Interfaces.Services;
using WebStoreCoreApplicatioc.DAL;
using WebStoreCoreApplication.Domain.ViewModels;

namespace WebStoreBakulin.Services.Products
{
    public class SqlEmployeesData : IEmployeeService
    {
        private readonly WebStoreContext _db;

        public SqlEmployeesData(WebStoreContext db) => _db = db;

        public IEnumerable<Employee> Get() => _db.Employees;

        public Employee GetById(int id) => _db.Employees.Find(id);

        public int Add(Employee employee)
        {
            if(employee is null) throw new ArgumentNullException(nameof(employee));

            
            //_db.Employees.Add(employee);

            return employee.Id;
        }

        public bool Delete(int id)
        {
            var employee = GetById(id);
            if (employee is null)
                return false;
            _db.Remove(employee);
            //_db.Employees.Remove(employee);
            return true;
        }
                
        void IEmployeeService.Edit(Employee employee)
        {
            _db.Add(employee);
        }

        void IEmployeeService.SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
