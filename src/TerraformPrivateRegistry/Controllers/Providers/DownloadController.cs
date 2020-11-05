using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TerraformPrivateRegistry.Core;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TerraformPrivateRegistry.Controllers.Providers
{
    [Route("v1/providers")]
    public class DownloadController : Controller
    {
        private readonly ILogger<DownloadController> logger;
        private readonly IProviderRepository providerRepository;

        public DownloadController(IProviderRepository providerRepository, ILogger<DownloadController> logger)
        {
            this.logger = logger;
            this.providerRepository = providerRepository;
        }

        [HttpGet("{ns}/{type}/{version}/download/{os}/{arch}", Name = "download")]
        public async Task<IActionResult> Get(string ns, string type, string version, string os, string arch)
        {
            var downloadDetails = await this.providerRepository.GetDownloadDetailsAsync(ns, type, version, os, arch, this.HttpContext.RequestAborted);

            return this.Json(downloadDetails);
        }
    }
}
