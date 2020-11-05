using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TerraformPrivateRegistry.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TerraformPrivateRegistry.Controllers
{
    [Route(".well-known")]
    public class TerraformController : Controller
    {
        private readonly TerraformIndex terraformIndex;

        public TerraformController(TerraformIndex terraformIndex)
        {
            this.terraformIndex = terraformIndex;
        }

        // GET: api/values
        [HttpGet("terraform.json")]
        public IActionResult Get()
        {
            return this.Json(this.terraformIndex);
        }

    }
}
