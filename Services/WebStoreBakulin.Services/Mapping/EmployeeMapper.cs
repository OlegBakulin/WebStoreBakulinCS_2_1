using System.Collections.Generic;
using System.Linq;
using WebStoreCoreApplication.Domain.Entities;
using WebStoreCoreApplication.Domain.ViewModels;

namespace WebStoreBakulin.Services.Mapping
{
    public static class EmployeeMapper
    {
        public static EmployeeViewModel ToView(this Employee employee) => new EmployeeViewModel
        {
            Id = employee.Id,
            IName = employee.Name,
            FName = employee.Surname,
            OName = employee.Patronymic,
            Age = employee.Age,
            EmployementDate = employee.EmployementDate
        };

        public static IEnumerable<EmployeeViewModel> ToView(this IEnumerable<Employee> employees) => employees.Select(ToView);

        public static Employee FromView(this EmployeeViewModel Model) => new Employee
        {
            Id = Model.Id,
            Surname = Model.FName,
            Name = Model.IName,
            Patronymic = Model.OName,
            Age = Model.Age,
            EmployementDate = Model.EmployementDate
        };
    }
}
