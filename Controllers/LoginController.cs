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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SCEC.API.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ModuleRepository _moduleRepository;
        private LogAcessRepository _logAcessRepository;
        private UserRepository _userRepository;

        public LoginController(ModuleRepository moduleRepository, LogAcessRepository logAcessRepository, UserRepository userRepository)
        {
            _moduleRepository = moduleRepository;
            _logAcessRepository = logAcessRepository;
            _userRepository = userRepository;
        }

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

                User user = await _userRepository.GetByEmail(loginDTO.Email);

                if (user == null)
                    return BadRequest(new { Message = Settings.CodeEnum.LoginError.ToDescriptionString() });

                //Password validation
                var crypt = new PBKDF2();

                if (string.Compare(user.Password, crypt.Compute(loginDTO.Password, user.Salt)) != 0)
                    return BadRequest(new { Message = Settings.CodeEnum.LoginError.ToDescriptionString() });

                //Getting roles
                var usersRoles = dbContext.UsersRoles.Include("Role")
                                .Where(x => x.IdUser == user.Id && x.Enabled.Equals(CONSTANTS.FLAG_YES) && x.Role.Enabled.Equals(CONSTANTS.FLAG_YES))
                                .Select(x=> new { x.IdRole, x.Role.RoleDescription})
                                .ToList();

                if(usersRoles.Count() == 0)
                    return BadRequest(new { Message = Settings.CodeEnum.UsersRolesNotFoundError.ToDescriptionString() });

                //Getting modules by roles
                var modules = await _moduleRepository.GetModulesByRole(usersRoles.Select(x => x.IdRole).ToList());
                user.Roles = string.Join(",", usersRoles.Select(x=> x.RoleDescription).ToArray());
                
                string token = TokenService.GenerateToken(user);
                await _logAcessRepository.Add(new LogAcess(user.Id, Utils.GetUserIP(HttpContext), Utils.GetUserAgent(HttpContext)));

                await MailjetService.SendEmail();
                
                return Ok(new { user.Name, user.Email, user.Id, Token = token, Roles = user.Roles, Modules  = modules });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Falha ao autenticar usuário! Erro: { ex.Message + ex.InnerException?.Message }" });
            }
        }

    }
}
