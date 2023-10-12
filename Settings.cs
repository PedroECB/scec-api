using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SCEC.API
{
    public class Settings
    {
        public string Secret { get; } = CONSTANTS.SECRET;

        public class CONSTANTS
        {
            public const string SECRET = "d7a789e1409c41c5806f90ffb3b20501";
            public const string SECRET_INTEGRATION = "####";
            public const string FLAG_YES = "S";
            public const string FLAG_NO = "N";
            public const string EMAIL_SENDER_NAME = "SCEC - Sistema de Consulta de Entradas Cadavéricas";
            public const string EMAIL_SENDER_ADDRESS = "pedrophbc@live.com";
        }

        public class KEYS
        {
            public const string KEY_EXTERNAL_API = "12$BASC2$#";
            public const string KEY_PUBLIC_MAILJET_API = "3dbe8ecf0294eed9f2eaa1abdcaa4407";
            public const string KEY_PRIVATE_MAILJET_API = "8d115ece2b8737900ffd197b115b32b6";

        }

        public class PATH
        {
            public const string FILES_DIRECTORY = @"C://localhost//";
        }

        public class URL
        {
            public const string URL_VIACEP_API = "https://viacep.com.br";
        }

        public class ROLES
        {
            public const string ADMIN = "ADMIN";
            public const string COLLABORATOR = "COLLABORATOR";
            public const string SUPER_ADMIN = "SUPER_ADMIN";
            public const string ADMIN_AND_SUPERADMIN = ADMIN + "," + SUPER_ADMIN;
        }

        public enum CodeEnum
        {
            [Description("Erro crítico!")]
            CriticalError = -1,

            [Description("Ok!")]
            Ok = 0,

            [Description("Usuário inexistente ou senha inválida!")]
            LoginError = 1,

            [Description("Usuário sem permissões ativas!")]
            UsersRolesNotFoundError = 2,

            [Description("Usuário não encontrado!")]
            UserNotFoundError = 3
        }
    }

    public static class codeEnumExtensions
    {
        public static string ToDescriptionString(this Settings.CodeEnum val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }

}
