using Microsoft.Extensions.Caching.Memory;

namespace DualTracking.Api.Extensions
{
    public class InMemoryCacheEntryOptions : MemoryCacheEntryOptions
    {
        public InMemoryCacheEntryOptions()
        {
            this.SetSlidingExpiration(TimeSpan.FromHours(1));
            this.SetAbsoluteExpiration(TimeSpan.FromHours(3));
            //this.SetSize(1);
        }
    }
}
