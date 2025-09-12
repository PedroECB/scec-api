using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SCEC.API.Data;
using SCEC.API.Models;
using SCEC.API.Models.DTO;
using SCEC.API.Repository;
using SCEC.API.Services;
using SimpleCrypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SCEC.API.Settings;

using SCEC.API.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SCEC.API.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginDomain _loginDomain;

        public LoginController(LoginDomain loginDomain)
        {
            _loginDomain = loginDomain;
        }

        // POST api/<LoginController>
        [HttpPost]
        [Route("auth")]
        [AllowAnonymous]
        public async Task<ActionResult<object>> Login([FromServices] DataContext dbContext, [FromBody] LoginDTO loginDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { Message = "Parâmetros de login inválidos!" });

                var user = await _loginDomain.Login(loginDTO, Utils.GetUserIP(HttpContext), Utils.GetUserAgent(HttpContext));
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"{ ex.Message + ex.InnerException?.Message }" });
            }
        }

    }
}
