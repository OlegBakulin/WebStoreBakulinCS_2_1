using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace WebStoreBakulin.Clients.Base
{
    public class BaseClient
    {
        protected readonly string _ServiceAddress;
        protected readonly HttpClient _Client;

        public BaseClient(IConfiguration Configuration, string ServiceAddress)
        {
            _ServiceAddress = ServiceAddress;
            _Client = new HttpClient
            {
                BaseAddress = new Uri(Configuration["WebApiURL"]),
                DefaultRequestHeaders =
                {
                    Accept = { new MediaTypeWithQualityHeaderValue("application/json")}
                }
            };
        }

    }
}
