using CacheMemory.Api.Helpers;
using CacheMemory.Api.Helpers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CacheMemory.Api.Controllers
{
    public class CacheController : Controller
    {
        private readonly ICacheManager _cacheManager;

        public CacheController(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        [HttpGet]
        [Route("v1/TesteCache")]
        public async Task<ActionResult> Adicionar()
        {
            var mensagem = "";
            try
            {
                mensagem = await _cacheManager.ProcessarCacheAsync(async () =>
                {
                    return "Teste Cache";

                }, "Cache|{0}", new[] { "Cache" }, TimeSpan.FromHours(1));


                return Ok(mensagem);
            }
            catch (Exception ex)
            {
                throw new Exception("Sistema indisponível.", ex);
            }
        }
    }
}
