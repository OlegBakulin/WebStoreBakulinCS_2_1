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
        public EmployeesClient(IConfiguration Configuration) : base(Configuration, WebApiAdress.EmployeesAdress) { }

        public IEnumerable<Employee> Get() => Get<IEnumerable<Employee>>(_ServiceAddress);

        public Employee GetById(int id) => Get<Employee>($"{_ServiceAddress}/{id}");

        public int Add(Employee employee) => Post(_ServiceAddress, employee).Content.ReadAsAsync<int>().Result;

        //public void Edit(int id, Employee employee) => Put($"{_ServiceAddress}/{id}", employee);
        public void Edit(Employee employee) => Put(_ServiceAddress, employee);

        public bool Delete(int id) => Delete($"{_ServiceAddress}/{id}").IsSuccessStatusCode;

        public void SaveChanges() { }
    }
}
