using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace WebStoreBakulin.Clients.Base
{
    class BaseClient
    {
        protected readonly string _ServiceAddress;
        protected readonly HttpClient _Client;

        public BaseClient(IConfiguration configuration, string ServiceAddress)
        {
            _ServiceAddress = ServiceAddress;
            _Client = new HttpClient
            {
                BaseAddress = new Uri(configuration["WebApiURL"]),
                DefaultRequestHeaders =
                {
                    Accept = { new MediaTypeWithQualityHeaderValue("application/json")}
                }
            };
        }

    }
}
