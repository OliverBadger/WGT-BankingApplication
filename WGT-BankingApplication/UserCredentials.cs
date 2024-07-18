using Newtonsoft.Json;
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

            if (File.Exists("employeeCredentials.json"))
            {
                Console.WriteLine("sdasdasda"); //dict = deseraialize data 
                string json = File.ReadAllText("employeeCredentials.json");
                employeeCredentials = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
            else
            {
                UserCredentials.employeeCredentials.Add(
                    Encryption.encryptString("admin"),
                    Encryption.encryptString("password"));
                UserCredentials.employeeCredentials.Add(
                    Encryption.encryptString("Ameer"),
                    Encryption.encryptString("abc"));
                UserCredentials.employeeCredentials.Add(
                    Encryption.encryptString("Naomi"),
                    Encryption.encryptString("123"));
                UserCredentials.employeeCredentials.Add(
                    Encryption.encryptString("Oliver"),
                    Encryption.encryptString("qwerty"));
                UserCredentials.employeeCredentials.Add(
                    Encryption.encryptString("Sam"),
                    Encryption.encryptString("password"));

                //We serialize the encrypted version of all our details 
                string json = JsonConvert.SerializeObject(employeeCredentials, Formatting.Indented);
                File.WriteAllText("EmployeeCredentials.json", json);
            }


        }
    }
}
