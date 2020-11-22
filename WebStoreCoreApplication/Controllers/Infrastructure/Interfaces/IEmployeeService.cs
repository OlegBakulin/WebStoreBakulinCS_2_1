using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreCoreApplication.ViewModels;

namespace WebStoreCoreApplication.Controllers.Infrastructure.Interfaces
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
