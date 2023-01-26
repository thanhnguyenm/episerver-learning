using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.web.Business.Services
{
    public interface ICacheManager
    {
        Task<T> GetValueAsync<T>(string key);

        Task<bool> StoreValueAsync(string key, object obj);
    }
}
