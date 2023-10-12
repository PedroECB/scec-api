using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;
using static SCEC.API.Settings;

namespace SCEC.API.Services
{
    public class MailjetService
    {
        public static async Task<object> SendEmail(string subject, string htmlPart, byte[] file = null, bool hasFile = false, params string[] recipients)
        {
            MailjetClient client = new MailjetClient(KEYS.KEY_PUBLIC_MAILJET_API, KEYS.KEY_PRIVATE_MAILJET_API);

            //Tratando destinatários
            var listEmailRecipients = new List<JObject>();
            foreach (var recipientEmail in recipients)
            {
                listEmailRecipients.Add(new JObject { { "Email", recipientEmail } });
            }

            //Verificando se há anexo de arquivos
            var Files = new List<JObject>();
            if (hasFile)
            {
                if (file == null)
                    throw new ApplicationException("Anexo não encontrado!");

                var fileName = Guid.NewGuid().ToString().ToUpper().Replace("-", "").Substring(0, 15) + ".pdf";
                Files.Add(new JObject { { "ContentType", "application/pdf" }, { "Filename", fileName }, { "Base64Content", file } });
            }

            //Disparando envio
            MailjetRequest request = new MailjetRequest { Resource = Send.Resource }
            .Property(Send.FromEmail, CONSTANTS.EMAIL_SENDER_ADDRESS)
            .Property(Send.FromName, CONSTANTS.EMAIL_SENDER_NAME)
            .Property(Send.Subject, subject)
            .Property(Send.HtmlPart, htmlPart)
            .Property(Send.Recipients, new JArray { listEmailRecipients })
            .Property(Send.Attachments, new JArray { Files });

            MailjetResponse response = await client.PostAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return new { Success = true, Message = "Email enviado com sucesso" };
            }
            else
            {
                return new { Success = false, StatusCode = response.StatusCode, ErrorInfo = response.GetErrorInfo(), ErrorMessage = response.GetErrorMessage() };
            }
        }
    }
}
