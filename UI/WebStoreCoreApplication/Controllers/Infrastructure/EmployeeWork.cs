using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStoreCoreApplication.Controllers.Infrastructure
{
    public class EmployeeWork
    {
        public RequestDelegate Next { get; }

        public EmployeeWork(RequestDelegate next)
        {
            Next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

        }
    }
}
