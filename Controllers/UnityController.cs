using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCEC.API.Models;
using SCEC.API.Repository;
using System;
using System.Collections;
using System.Threading.Tasks;
using static SCEC.API.Settings;

namespace SCEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnityController : ControllerBase
    {
        GroupUnityRepository _groupUnityRepository;

        public UnityController(GroupUnityRepository groupUnityRepository)
        {
            _groupUnityRepository = groupUnityRepository;
        }

        // GET: api/<GroupUnityController>/all
        [HttpGet]
        [Route("All")]
        [AllowAnonymous]
        [ResponseCache(VaryByHeader = "User-Agent", Location = ResponseCacheLocation.Any, Duration = 1200)] //Configurando cache pra 20 minutos
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] //Desfazendo cache padrão, caso o cahe da aplicação esteja configurado no startup
        public async Task<object> ListAll()
        {
            try
            {
                IEnumerable unities = await _groupUnityRepository.GetAll();
                return Ok(unities);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
