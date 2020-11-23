using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreCoreApplication.Domain.ViewModels;

namespace WebStoreBakulin.Interfaces.Services
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeViewModel> GetAll();

        EmployeeViewModel GetByID(int id);

        void Commit();

        void AddNew(EmployeeViewModel newmodel);

        void Delete(int id);
    }
}
