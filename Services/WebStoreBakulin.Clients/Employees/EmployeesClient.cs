using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WebStoreBakulin.Clients.Base;
using WebStoreBakulin.Interfaces.Services;
using WebStoreCoreApplication.Domain.ViewModels;
using WebStoreCoreApplication.Domain;

namespace WebStoreBakulin.Clients.Employees
{
    public class EmployeesClient : BaseClient, IEmployeeService
    {
        protected readonly string ServiceAddress;
        protected readonly HttpClient Client;
        protected EmployeesClient(IConfiguration Configuration, string ServiceAddress) : base(Configuration, WebApiAdress.EmployeesAdress)
        {
        }

        public void SaveChanges() { }





        public IEnumerable<EmployeeViewModel> GetAll() => Get<IEnumerable<EmployeeViewModel>>(ServiceAddress);

        public EmployeeViewModel GetByID(int id) => Get<EmployeeViewModel>($"{ServiceAddress}/{id}");

        public int AddNew(EmployeeViewModel employee)
        {
            return Post(ServiceAddress, employee).Content.ReadAsAsync<int>().Result;   
        }

        public bool Edit(EmployeeViewModel employee)
        {
            HttpResponseMessage message;
            message = Put(ServiceAddress, employee);
            return message.IsSuccessStatusCode;
        }

        //public void Edit(int id, Employee employee) => Put($"{_ServiceAddress}/{id}", employee);
        bool IEmployeeService.Delete(int id)
        {
            return Delete($"{_ServiceAddress}/{id}").IsSuccessStatusCode;
        }

        public void Commit()
        {
        }

    }
}
