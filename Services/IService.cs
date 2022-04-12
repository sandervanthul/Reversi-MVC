using System.Collections.Generic;
using System.Threading.Tasks;
using ReversiMvcApp.Models;

namespace ReversiMvcApp.Services
{
    public interface IService<T>
    {
        Task<List<T>> GetAsync(string path);
        Task<T> GetAsync(string id, string path);
        Task<bool> GetSpecialAsync(string id, string path);
        Task<string> AddAsync(T item, string path);
        Task<string> AddAsync(SpelViewModel item, string path);
        Task<bool> UpdateAsync(int id, T item, string path);
        Task<T> UpdateSpecialAsync(string id, object item, string path);
        Task<bool> DeleteAsync(string id, string path);
        Task<string> JoinAsync(SpelViewModel item, string path);
    }
}
