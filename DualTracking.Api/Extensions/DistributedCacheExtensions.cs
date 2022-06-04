using Microsoft.Extensions.Caching.Distributed;

using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace DualTracking.Api.Extensions
{
	public static class DistributedCacheExtensions
	{
        public static async Task SetRecordAsync<T>(this IDistributedCache cache,
            string recordId,
            T data,
            TimeSpan? absoluteExpireTime = null,
            TimeSpan? unusedExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions();
			
            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromHours(3);
            options.SlidingExpiration = unusedExpireTime;

            var jsonData = JsonSerializer.Serialize(data);
            try
            {
                await cache.SetStringAsync(recordId, jsonData, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
        {
            string jsonData = String.Empty;

            try
            {
                jsonData = await cache.GetStringAsync(recordId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (string.IsNullOrEmpty(jsonData))
            {
                return default(T);
            }

            return JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}
