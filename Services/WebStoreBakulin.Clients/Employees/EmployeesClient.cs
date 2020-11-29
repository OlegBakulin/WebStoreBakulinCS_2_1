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
using WebStoreCoreApplication.Domain.Entities;

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





        public IEnumerable<Employee> GetAll() => Get<IEnumerable<Employee>>(ServiceAddress);

        public Employee GetByID(int id) => Get<Employee>($"{ServiceAddress}/{id}");

        public int AddNew(Employee employee)
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
        public bool Delete(int id) => Delete($"{_ServiceAddress}/{id}").IsSuccessStatusCode;

        public void Commit()
        {
        }

    }
}
