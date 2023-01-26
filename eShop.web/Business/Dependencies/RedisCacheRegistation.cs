using EPiServer.ServiceLocation;
using eShop.web.Business.Services;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Core.Abstractions;
using StackExchange.Redis.Extensions.Core.Implementations;
using StackExchange.Redis.Extensions.LegacyConfiguration;
using StackExchange.Redis.Extensions.Protobuf;

namespace eShop.web.Business.Dependencies
{
    public static class RedisCacheRegistation
    {
        /// <summary>
        /// Add StackExchange.Redis with its serialization provider.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="redisConfiguration">The redis configration.</param>
        /// <typeparam name="T">The typof of serializer. <see cref="ISerializer" />.</typeparam>
        public static void AddStackExchangeRedisExtensions(this IServiceConfigurationProvider services)
        {
            var redisConfiguration = RedisCachingSectionHandler.GetConfig();
            services.AddSingleton(redisConfiguration);
            services.AddSingleton<IRedisConnectionPoolManager, RedisConnectionPoolManager>();
            services.AddSingleton<ISerializer, ProtobufSerializer>();

            services.AddSingleton<IRedisClient, RedisClient>();
            services.AddSingleton<ICacheManager, RedisCacheManager>();
        }
    }
    
}