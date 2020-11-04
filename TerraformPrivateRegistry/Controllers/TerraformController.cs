using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TerraformPrivateRegistry.Controllers
{
    [Route(".well-known")]
    public class TerraformController : Controller
    {
        // GET: api/values
        [HttpGet("terraform.json")]
        public IActionResult Get()
        {
            return this.Json(new
            {
                ProvidersV1 = "v1/providers"
            });
        }

    }
}
