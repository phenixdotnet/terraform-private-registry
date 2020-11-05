using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TerraformPrivateRegistry.Core;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TerraformPrivateRegistry.Controllers
{
    [Route("v1/providers")]
    public class VersionsController : Controller
    {
        private readonly ILogger<VersionsController> logger;
        private readonly IProviderRepository providerRepository;

        public VersionsController(IProviderRepository providerRepository, ILogger<VersionsController> logger)
        {
            this.logger = logger;
            this.providerRepository = providerRepository;
        }

        // GET: /<controller>/
        [HttpGet("{ns}/{type}/versions", Name = "versions")]
        public async Task<IActionResult> Get(string ns, string type)
        {
            this.logger.LogDebug("Getting provider version for {ns}/{type}", ns, type);
            var versions = await this.providerRepository.GetVersionsAsync(ns, type, this.HttpContext.RequestAborted);

            return this.Json(new
            {
                Versions = versions
            });
        }
    }
}

