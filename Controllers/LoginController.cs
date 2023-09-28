using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SCEC.API.Data;
using SCEC.API.Models;
using SCEC.API.Models.DTO;
using SCEC.API.Services;
using SimpleCrypto;
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
        // POST api/<LoginController>
        [HttpPost]
        [Route("auth")]
        [AllowAnonymous]
        public async Task<ActionResult<object>> Post([FromServices] DataContext dbContext, [FromBody] LoginDTO loginDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { Message = "Parâmetros de login inválidos!" });

                User user = await dbContext.Users.Where(x => x.Email.Equals(loginDTO.Email) && x.Enabled.Equals("S"))
                            .AsNoTracking()
                            .Select(x => new User { Id = x.Id, Name = x.Name, Email = x.Email, Password = x.Password, Salt = x.Salt })
                            .FirstOrDefaultAsync();

                if (user == null)
                    return BadRequest(new { Message = Settings.codeEnum.LoginError.ToDescriptionString() });

                var crypt = new PBKDF2();

                if (string.Compare(user.Password, crypt.Compute(loginDTO.Senha, user.Salt)) != 0)
                    return BadRequest(new { Message = Settings.codeEnum.LoginError.ToDescriptionString() });

                var usersRoles = dbContext.UsersRoles.Include("Role")
                                .Where(x => x.IdUser == user.Id && x.Enabled.Equals("S") && x.Role.Enabled.Equals("S"))
                                .Select(x=> x.Role.RoleDescription)
                                .ToList();

                if(usersRoles.Count() == 0)
                    return BadRequest(new { Message = Settings.codeEnum.RolesNotFoundError.ToDescriptionString() });

                user.Roles = string.Join(",", usersRoles.ToArray());
                
                string token = TokenService.GenerateToken(user);

                return Ok(new { user.Name, user.Email, user.Id, Token = token, Roles = usersRoles });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Falha ao autenticar usuário! Erro: { ex.Message + ex.InnerException?.Message }" });
            }
        }

    }
}
