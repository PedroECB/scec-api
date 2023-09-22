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

        // GET: api/<UserController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<UserController>/5
        [HttpGet("{id}")]
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

            return new { Ok = 12 };
            }

            // PUT api/<UserController>/5
            [HttpPut("{id}")]
            public void Put(int id, [FromBody] string value)
            {
            }

            // DELETE api/<UserController>/5
            [HttpDelete("{id}")]
            public void Delete(int id)
            {
            }
        }
    }
