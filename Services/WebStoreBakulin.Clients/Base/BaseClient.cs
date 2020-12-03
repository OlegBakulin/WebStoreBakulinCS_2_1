using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebStoreBakulin.Clients.Base
{
    public class BaseClient
    {
        protected readonly string _ServiceAddress;
        protected readonly HttpClient _Client;

        protected BaseClient(IConfiguration Configuration, string ServiceAddress)
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

        public T Get<T>(string url) => GetAsync<T>(url).Result;

        public async Task<T> GetAsync<T>(string url)
        {
            var response = await _Client.GetAsync(url);
            return await response.EnsureSuccessStatusCode().Content.ReadAsAsync<T>();
        }

        public HttpResponseMessage Post<T>(string url, T item) => PostAsync(url, item).Result;

        public async Task<HttpResponseMessage> PostAsync<T>(string url, T item)
        {
            var response = await _Client.PutAsJsonAsync<T>(url, item);
            return response.EnsureSuccessStatusCode();
        }
        public HttpResponseMessage Put<T>(string url, T item) => PutAsync(url, item).Result;
        public async Task<HttpResponseMessage> PutAsync<T>(string url, T item)
        {
            var response = await _Client.PutAsJsonAsync(url, item);
            return response.EnsureSuccessStatusCode();
        }

        public HttpResponseMessage Delete(string url) => DeleteAsync(url).Result;
        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            var response = await _Client.DeleteAsync(url);
            return response;
        }

        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            //_Client.Dispose();
        }
    }
}
