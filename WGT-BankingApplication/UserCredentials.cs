using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGT_BankingApplication
{
    class UserCredentials
    {
        //Temp will store in JSON
        public static Dictionary<string, string> employeeCredentials = new Dictionary<string, string>();
        public static void initUserCredentials()
        {
            UserCredentials.employeeCredentials.Add(
                Encryption.encryptString("admin"), 
                Encryption.encryptString("password"));
        }
    }
}
