using AzRedisCache.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;

namespace AzRedisCache.Controllers
{
    [ApiController]
    [Route("api/cache")]
    public class CacheController : ControllerBase
    {
        private readonly IDistributedCache _cache;

        public CacheController(IDistributedCache cache)
        {
            this._cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        [HttpPost("")]
        public async Task<IActionResult> Set(CacheInputModel model)
        {
            await this._cache.SetStringAsync(model.Key, model.Value, new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(2)
            });

            return Ok();
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> Get(string key)
        {
            var value = await this._cache.GetStringAsync(key);

            return Ok(new
            {
                Key = key,
                Value = value
            });
        }
    }
}
