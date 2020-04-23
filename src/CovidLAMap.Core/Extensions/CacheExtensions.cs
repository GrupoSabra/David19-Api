using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ProtoBuf;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using NetTopologySuite.Geometries;

namespace CovidLAMap.Core.Extensions
{
    public static class CacheExtensions
    {
        public static async Task<T> GetOrSetAsync<T>(this IDistributedCache cache, string key, Func<Task<T>> func, 
            DistributedCacheEntryOptions options) where T : class
        {
            var cacheJson = await cache.GetStringAsync(key);
            if (!string.IsNullOrEmpty(cacheJson))
            {
                T obj = JsonConvert.DeserializeObject<T>(cacheJson);
                if (obj != null) return obj;
            }

            T newObj = await func();
            if (newObj == null) return null;

            using var memStream = new MemoryStream();
            var serialized = JsonConvert.SerializeObject(newObj, JsonSettings());
            await cache.SetStringAsync(key, serialized, options);
            return newObj;
        }

        public static async Task<T> GetOrSetAsync<T>(this IDistributedCache cache, string key, Func<Task<T>> func,
            TimeSpan timeSpan) where T : class
        {
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = timeSpan
            };
            
            return await CacheExtensions.GetOrSetAsync<T>(cache, key, func, options);
        }

        private static JsonSerializerSettings JsonSettings()
        {
            var jsonSettngs = new JsonSerializerSettings();
            foreach (var converter in NetTopologySuite.IO.GeoJsonSerializer.Create(new GeometryFactory(new PrecisionModel(), 4326)).Converters)
            {
                jsonSettngs.Converters.Add(converter);
            }

            return jsonSettngs;
        }

    }
}
