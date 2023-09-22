using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCEC.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SCEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        // GET: api/<LoginController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<LoginController>
        [HttpPost]
        [Route("auth")]
        [AllowAnonymous]
        public IEnumerable<string> Post([FromServices] DataContext dbContext, [FromBody] object loginDto)
        {
            var teste = loginDto;
            return new string[] { "value1", "value2" };
        }

    }
}
