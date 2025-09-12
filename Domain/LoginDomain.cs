using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SCEC.API.Data;
using SCEC.API.Models;
using SCEC.API.Models.DTO;
using SCEC.API.Repository;
using SCEC.API.Services;
using SimpleCrypto;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using static SCEC.API.Settings;


namespace SCEC.API.Domain
{
    public class LoginDomain
    {
        private ModuleRepository _moduleRepository;
        private LogAcessRepository _logAcessRepository;
        private UserRepository _userRepository;
        private EmailModelRepository _emailModelRepository;
        private DataContext _dbContext;

        public LoginDomain(DataContext dbContext, ModuleRepository moduleRepository, LogAcessRepository logAcessRepository, UserRepository userRepository, EmailModelRepository emailModelRepository)
        {
            _moduleRepository = moduleRepository;
            _logAcessRepository = logAcessRepository;
            _userRepository = userRepository;
            _emailModelRepository = emailModelRepository;
            _dbContext = dbContext;
        }

        public async Task<object> Login(LoginDTO loginDTO, string IP, string UserAgent)
        {
            User user = await _userRepository.GetByEmail(loginDTO.Email);

            if (user == null)
                throw new ApplicationException(Settings.CodeEnum.LoginError.ToDescriptionString());

            //Password validation
            var crypt = new PBKDF2();

            if (string.Compare(user.Password, crypt.Compute(loginDTO.Password, user.Salt)) != 0)
                throw new ApplicationException(Settings.CodeEnum.LoginError.ToDescriptionString());


            //Getting roles
            var usersRoles = _dbContext.UsersRoles.Include("Role")
                            .Where(x => x.IdUser == user.Id && x.Enabled.Equals(CONSTANTS.FLAG_YES) && x.Role.Enabled.Equals(CONSTANTS.FLAG_YES))
                            .Select(x => new { x.IdRole, x.Role.RoleDescription })
                            .ToList();

            if (usersRoles.Count() == 0)
                throw new ApplicationException(Settings.CodeEnum.LoginError.ToDescriptionString());

            //Getting modules by roles
            var modules = await _moduleRepository.GetModulesByRole(usersRoles.Select(x => x.IdRole).ToList());
            user.Roles = string.Join(",", usersRoles.Select(x => x.RoleDescription).ToArray());

            string token = TokenService.GenerateToken(user);

            //Register acess log
            await _logAcessRepository.Add(new LogAcess(user.Id, IP, UserAgent));

            //Notify Account access by e-mail
            //await _emailModelRepository.SendAcessAccountEmail(user.Name, user.Email);

            return new { user.Name, user.Email, user.Id, Token = token, Roles = user.Roles, Modules = modules };
            //return null;
        }
    }
}
