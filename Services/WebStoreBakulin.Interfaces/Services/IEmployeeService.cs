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
        IEnumerable<Employee> GetAll();

        Employee GetByID(int id);

        int AddNew(Employee newmodel);

        bool Delete(int id);
     
        void Commit();
    }
}
