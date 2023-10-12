using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCEC.API.Models;
using SCEC.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SCEC.API.Services;

namespace SCEC.API.Repository
{
    public class EmailModelRepository : IRepository<EmailModel>
    {
        private DataContext _context;

        public EmailModelRepository(DataContext context)
        {
            _context = context;
        }

        Task<IEnumerable<EmailModel>> IRepository<EmailModel>.GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<EmailModel> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<EmailModel> Add(EmailModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<EmailModel> Update(EmailModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<EmailModel> Delete(EmailModel entity)
        {
            throw new NotImplementedException();
        }


        public async Task<EmailModel> GetByFlag(string flag)
        {
            EmailModel emailModel = await _context.EmailModels.Where(x => x.Flag == flag)
                        .AsNoTracking()
                        .FirstOrDefaultAsync();

            return emailModel;
        }

        public async Task<object> SendAcessAccountEmail(string username, string emailRecipient)
        {
            try
            {
                EmailModel emailNewAcess = await GetByFlag("NOVO_ACESSO");
                emailNewAcess.Text = emailNewAcess.Text.Replace("{{nome_usuario}}", username);

                await MailjetService.SendEmail(emailNewAcess.Subject, emailNewAcess.Text, null, false, emailRecipient);

                return new { Success = true, Message = "E-mail enviado com sucesso" };
            }
            catch (Exception ex)
            {
                return new { Success = false, Message = $"Falha ao enviar e-mail de acesso { ex.Message }" };
            }
        }

        public async Task<object> SendNewAccountUserEmail(string username, string emailRecipient)
        {
            try
            {
                EmailModel emailNewAcess = await GetByFlag("EMAIL_ACESSO_DISPONIVEL");
                emailNewAcess.Text = emailNewAcess.Text.Replace("{{nome_usuario}}", username);

                await MailjetService.SendEmail(emailNewAcess.Subject, emailNewAcess.Text, null, false, emailRecipient);

                return new { Success = true, Message = "E-mail enviado com sucesso" };
            }
            catch (Exception ex)
            {
                return new { Success = false, Message = $"Falha ao enviar e-mail de acesso { ex.Message }" };
            }
        }
    }
}
