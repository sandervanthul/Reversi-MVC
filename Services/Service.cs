using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ReversiMvcApp.ApiClientLib;
using ReversiMvcApp.Models;

namespace ReversiMvcApp.Services
{
    public class Service<T> : IService<T> where T : new() 
    {
        private readonly ApiClient _apiClient;

        public Service(IConfiguration configuration, IHttpClientFactory factory)
        {
            var _configuration = configuration;
            var _connectionstring = _configuration.GetValue<string>("ApiUri");

            var _factory = factory;

            _apiClient = new ApiClient(_factory, _connectionstring);
        }

        public async Task<string> AddAsync(T item, string path)
        {
            var response = await _apiClient.Post(path, item);
            if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();
            return "";
        }

        public async Task<string> JoinAsync(SpelViewModel item, string path)
        {
            var response = await _apiClient.Post(path, item);
            if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();
            return "";
        }

        public async Task<bool> DeleteAsync(int id, string path)
        {
            var response = await _apiClient.Delete(path, id.ToString());
            if (response.IsSuccessStatusCode) return true;
            return false;
        }

        public async Task<List<T>> GetAsync(string path)
        {
            var items = new List<T>();
            var response = await _apiClient.Get(path, "");
            if (response.IsSuccessStatusCode) items = await response.Content.ReadAsAsync<List<T>>(); // Microsoft.AspNet.WebApi.Client (nuget package)
            return items;
        }

        public async Task<T> GetAsync(string id, string path)
        {
            T item = new();
            var response = await _apiClient.Get(path, id);
            if (response.IsSuccessStatusCode) item = await response.Content.ReadAsAsync<T>();
            return item;
        }

        public async Task<bool> UpdateAsync(int id, T item, string path)
        {
            var response = await _apiClient.Put(path, id.ToString(), item);
            if (response.IsSuccessStatusCode) return true;
            return false;
        }

        public async Task<T> UpdateSpecialAsync(string id, object item, string path)
        {
            T newState = new();
            var response = await _apiClient.Put(path, id, item);
            if (response.IsSuccessStatusCode) newState = await response.Content.ReadAsAsync<T>();
            return newState;
        }
    }
}
