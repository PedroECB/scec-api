using System;
using System.Collections.Generic;
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
        }

        public class KEYS
        {
            public const string KEY_EXTERNAL_API = "12$BASC2$#";
        }

        public class PATH
        {
            public const string FILES_DIRECTORY = @"C://localhost//";
        }

        public class URL
        {
            public const string URL_VIACEP_API = "https://viacep.com.br"; 
        }
    }


}
