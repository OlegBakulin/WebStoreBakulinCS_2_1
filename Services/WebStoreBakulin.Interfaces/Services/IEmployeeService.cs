using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreCoreApplication.Domain.Entities;
using WebStoreCoreApplication.Domain.ViewModels;

namespace WebStoreBakulin.Interfaces.Services
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> Get();

        Employee GetById(int id);

        int Add(Employee employee);

        void Edit(Employee employee);

        bool Delete(int id);

        void SaveChanges();
    }
}
