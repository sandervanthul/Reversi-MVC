using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ReversiMvcApp.ApiClientLib
{
    public class ApiClient
    {
        private readonly HttpClient _client;

        public ApiClient(IHttpClientFactory factory, string baseaddress)
        {
            var _factory = factory;
            var _baseaddress = new Uri(baseaddress);

            _client = _factory.CreateClient();
            _client.BaseAddress = _baseaddress;
        }

        public async Task<HttpResponseMessage> Get(string path, string key)
        {
            if (key != "") path += "/" + key;

            var response = await _client.GetAsync(path);
            return response;
        }

        public async Task<HttpResponseMessage> Post<T>(string path, T entity)
        {
            var response = await _client.PostAsJsonAsync(path, entity);
            return response;
        }

        public async Task<HttpResponseMessage> Put<T>(string path, string key, T entity)
        {
            if (key != "") path += "/" + key;

            var response = await _client.PutAsJsonAsync(path, entity);
            return response;
        }

        public async Task<HttpResponseMessage> Delete(string path, string key)
        {
            path += "/" + key;

            var response = await _client.DeleteAsync(path);
            return response;
        }
    }
}
