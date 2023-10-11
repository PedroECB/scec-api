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

        public static async Task<object> SendEmail()
        {
            MailjetClient client = new MailjetClient(KEYS.KEY_PPUBLIC_MAILJET_API);

            MailjetRequest request = new MailjetRequest { Resource = Send.Resource }
            .Property(Send.FromEmail, "pedrophbc@live.com")
            .Property(Send.FromName, "Mailjet Pilot")
            .Property(Send.Subject, "Your email flight plan!")
            .Property(Send.TextPart, "Dear passenger, welcome to Mailjet! May the delivery force be with you!")
            .Property(Send.HtmlPart, "<h3>Dear passenger, welcome to <a href=\"https://www.mailjet.com/\">Mailjet</a>!<br />May the delivery force be with you!")
            .Property(Send.Recipients, new JArray {
                new JObject {{"Email", "pedrosophbc@gmail.com"}}
            });

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
