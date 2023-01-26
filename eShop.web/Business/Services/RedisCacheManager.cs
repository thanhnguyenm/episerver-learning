using EPiServer.Logging.Compatibility;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Threading.Tasks;

namespace eShop.web.Business.Services
{
    public class RedisCacheManager : ICacheManager
    {
        private readonly ILog Logger = LogManager.GetLogger(typeof(RedisCacheManager));
        private readonly IRedisClient redisClient;
        private readonly IDatabase database;

        public RedisCacheManager(IRedisClient redisClient)
        {
            this.redisClient = redisClient;
            database = this.redisClient.Db0.Database;
        }

        public async Task<T> GetValueAsync<T>(string key)
        {
            return await redisClient.GetDefaultDatabase().GetAsync<T>(key);
        }

        public async Task<bool> StoreValueAsync(string key, object obj)
        {
            return await redisClient.GetDefaultDatabase().AddAsync(key, obj);
        }
    }
}