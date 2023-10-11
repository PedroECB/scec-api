using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCEC.API.Models;
using SCEC.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SCEC.API.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private RoleRepository _roleRepository;

        public RoleController(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        // GET: api/<RoleController>
        [HttpGet]
        [Route("All")]
        [Authorize(Roles = Settings.ROLES.ADMIN)]
        public async Task<ActionResult<IEnumerable<Role>>> Get()
        {
            try
            {
                var roles = await _roleRepository.GetAll();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Falha ao listar roles: Error: {ex.Message}" });
            }
        }

        // GET api/<RoleController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RoleController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RoleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RoleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
