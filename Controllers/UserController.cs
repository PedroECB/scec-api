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
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserRepository _userRepository;
        private EmailModelRepository _emailModelRepository;

        public UserController(UserRepository userRepository, EmailModelRepository emailModelRepository)
        {
            _userRepository = userRepository;
            _emailModelRepository = emailModelRepository;
        }

        // GET: api/<UserController>/all
        [HttpGet]
        [Route("All")]
        [Authorize(Roles = ROLES.ADMIN_AND_SUPERADMIN)]
        [ResponseCache(VaryByHeader = "User-Agent", Location = ResponseCacheLocation.Any, Duration = 1200)] //Configurando cache pra 20 minutos
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] //Desfazendo cache padrão, caso o cahe da aplicação esteja configurado no startup
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
                    return Ok(new ResponseDataDTO(0, null, user));
                else
                    return Ok(new ResponseDataDTO(0, "Usuário não encontrado"));
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
                User user = new User(userDTO.Name, userDTO.Email);
                _userRepository.SetPassword(ref user, userDTO.Password);
                await _userRepository.Add(user);

                //Disparo de email de acesso à conta disponível
                await _emailModelRepository.SendNewAccountUserEmail(user.Name, user.Email);

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

                user.Enabled = CONSTANTS.FLAG_NO;
                user.LastUpdate = DateTime.Now;

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
                    return BadRequest(new { Message = CodeEnum.UserNotFoundError.ToDescriptionString() });

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
                    return BadRequest(new { Message = CodeEnum.UserNotFoundError.ToDescriptionString() });

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

    }
}
