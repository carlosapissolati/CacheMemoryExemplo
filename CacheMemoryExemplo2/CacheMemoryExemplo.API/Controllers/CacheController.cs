using CacheMemoryExemplo.API.CacheManager;
using CacheMemoryExemplo.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CacheMemoryExemplo.API.Controllers
{
    public class CacheController : Controller
    {
        private readonly Settings _settings;
        private readonly ICacheManager _cacheManager;

        public CacheController(IOptions<Settings> options, ICacheManager cacheManager)
        {
            _settings = options.Value;
            _cacheManager = cacheManager;
        }

        [HttpGet]
        [Route("api/v1/TesteCache")]
        public async Task<IActionResult> TesteCache()
        {
            var teste = await _cacheManager.ProcessarCacheAsync(async () =>
            {
                var retorno = "Teste";
                return retorno;
            }, "Teste|{0}", new[] { "1" }, null);

            return Ok(teste);
        }
    }
}
