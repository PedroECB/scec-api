using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCEC.API.Models;
using SCEC.API.Repository;
using SCEC.API.Models.DTO;
using System.Security.Cryptography;
using SimpleCrypto;
using Microsoft.AspNetCore.Authorization;
using SCEC.API;
using static SCEC.API.Settings;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SCEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/<UserController>/all
        [HttpGet]
        [Route("All")]
        [Authorize(Roles = ROLES.ADMIN_AND_SUPERADMIN)]
        public async Task<ActionResult<IEnumerable<User>>> ListAll()
        {
            try
            {
                var users = await _userRepository.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = ROLES.ADMIN_AND_SUPERADMIN)]
        public async Task<ActionResult<User>> Get(int id)
        {
            try
            {
                User user = await _userRepository.GetById(id);

                if (user != null)
                    return Ok(user);
                else
                    return Ok(new { });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // POST api/<UserController>
        [HttpPost]
        [Route("create")]
        [Authorize(Roles = ROLES.ADMIN_AND_SUPERADMIN)]
        public async Task<ActionResult<object>> Post([FromBody] AddUserDTO userDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                User user = new User();
                user.Email = userDTO.Email;
                user.Name = userDTO.Name;
                user.Enabled = "S";

                _userRepository.SetPassword(ref user, userDTO.Password);
                await _userRepository.Add(user);

                return Ok(new { message = "Usuário cadastrado com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Falha ao criar usuário: {ex.Message + ex.InnerException}" });
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = ROLES.ADMIN_AND_SUPERADMIN)]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = ROLES.SUPER_ADMIN)]
        public async Task<ActionResult<object>> Delete(int id)
        {
            try
            {
                User user = await _userRepository.GetById(id);

                if (user == null || user.Enabled == CONSTANTS.FLAG_NO)
                    return BadRequest(new { Message = "Falha ao inativar usuário! Usuário não encontrado." });

                await _userRepository.Delete(user);

                return Ok(new { Message = "Usuário inativado com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.InnerException?.Message != null ? ex.InnerException.Message : ex.Message });
            }
        }

        // GET api/<UserController>/resetpassword/5
        [HttpGet("resetpassword/{idUser:int}")]
        [Authorize(Roles = ROLES.ADMIN_AND_SUPERADMIN)]
        public async Task<ActionResult<object>> ResetPassword(int idUser)
        {
            try
            {
                User user = await _userRepository.GetById(idUser);

                if (user == null)
                    return BadRequest(new { Message = codeEnum.UserNotFoundError.ToDescriptionString() });

                _userRepository.SetPassword(ref user, user.Email);
                await _userRepository.Update(user);

                return Ok(new { Message = "Senha resetada com sucesso!" }); ;
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.InnerException?.Message != null ? ex.InnerException.Message : ex.Message });
            }
        }

        // POST api/<UserController>/alterpassword
        [HttpPost("alterpassword")]
        [Authorize]
        public async Task<object> AlterPassword(AlterPasswordDTO alterPassword)
        {
            try
            {
                int idUser = int.Parse(User.FindFirstValue(ClaimTypes.Sid));
                User user = await _userRepository.GetUserByIdFullColumns(idUser);

                if (user == null)
                    return BadRequest(new { Message = codeEnum.UserNotFoundError.ToDescriptionString() });

                PBKDF2 crypto = new PBKDF2();

                if (string.Compare(user.Password, crypto.Compute(alterPassword.CurrentPassword, user.Salt)) != 0)
                    return BadRequest(new { Message = "A senha atual informada é inválida!" });

                _userRepository.SetPassword(ref user, alterPassword.NewPassword);
                await _userRepository.Update(user);

                return Ok(new { Message = "Senha alterada com sucesso!" }); ;
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.InnerException?.Message != null ? ex.InnerException.Message : ex.Message });
            }
        }


        [HttpGet]
        public string GetUserIP()
        {
            string ipaddress = (!String.IsNullOrEmpty(HttpContext.Request.Headers["X-Forwarded-For"].ToString())) ? HttpContext.Request.Headers["X-Forwarded-For"].ToString() : HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();

            if (!string.IsNullOrEmpty(ipaddress))
            {
                string[] addresses = ipaddress.Split(',');

                if (addresses.Length != 0)
                {
                    ipaddress = addresses[0];
                }

                ipaddress = (ipaddress == "::1" || ipaddress == "0:0:1" || ipaddress == "0.0.0.1" || ipaddress == "127.0.0.1") ? "187.26.75.200" : ipaddress; // corrige o endereco local de ipv6 para ipv4

                return ipaddress;
            }
            else
            {
                return null;
            }
        }

    }
}
