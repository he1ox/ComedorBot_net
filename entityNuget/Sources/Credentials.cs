using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entityNuget.Sources
{
    class Credentials
    {
        private static string Token = "tu_token";

        public static string getToken()
        {
            return Token;
        }
    }
}
