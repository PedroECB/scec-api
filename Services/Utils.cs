using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Web;

namespace SCEC.API.Services
{
    public class Utils
    {
        #region HTTPHEADERS
        public static string GetUserIP(HttpContext httpContext)
        {
            string ipaddress = (!String.IsNullOrEmpty(httpContext.Request.Headers["X-Forwarded-For"].ToString())) ? httpContext.Request.Headers["X-Forwarded-For"].ToString() : httpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();

            if (!string.IsNullOrEmpty(ipaddress))
            {
                string[] addresses = ipaddress.Split(',');

                if (addresses.Length != 0)
                    ipaddress = addresses[0];

                //Alterando endereço local de ipv6 para ipv4
                ipaddress = (ipaddress == "::1" || ipaddress == "0:0:1" || ipaddress == "0.0.0.1" || ipaddress == "127.0.0.1") ? "187.26.75.200" : ipaddress; 
                return ipaddress;
            }
            else
                return null;
        }

        public static string GetUserAgent(HttpContext httpContext)
        {
            string userAgent = !string.IsNullOrEmpty(httpContext.Request.Headers["User-Agent"].ToString()) ? httpContext.Request.Headers["User-Agent"].ToString() : null;
            return userAgent;
        }

        #endregion
    }
}
