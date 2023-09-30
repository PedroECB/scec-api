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
                var crypto = new PBKDF2();
                User user = new User();
                user.Email = userDTO.Email;
                user.Name = userDTO.Name;
                user.Password = crypto.Compute(userDTO.Senha);
                user.Salt = crypto.Salt;
                user.Enabled = "S";

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
                    return BadRequest(new { Message = "Usuário não encontrado!" });

                var crypto = new PBKDF2();
                user.Password = crypto.Compute(user.Email);
                user.Salt = crypto.Salt;
                user.LastUpdate = DateTime.Now;

                await _userRepository.Update(user);

                return Ok(new { Message = "Senha resetada com sucesso!" }); ;
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.InnerException?.Message != null ? ex.InnerException.Message : ex.Message });
            }
        }


    }
}
