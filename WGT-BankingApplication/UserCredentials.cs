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
        //static dict to store employee creds
        public static Dictionary<string, string> employeeCredentials = new Dictionary<string, string>();
        //method to nitialise user creds from JSON file
        public static void initUserCredentials()
        {
            //checks if the employee creds exists
            if (File.Exists("employeeCredentials.json"))
            {
                string json = File.ReadAllText("employeeCredentials.json");
                employeeCredentials = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
            else
            {
                UserCredentials.employeeCredentials.Add(
                    Encryption.EncryptString("admin"),
                    Encryption.EncryptString("password"));
                UserCredentials.employeeCredentials.Add(
                    Encryption.EncryptString("Ameer"),
                    Encryption.EncryptString("abc"));
                UserCredentials.employeeCredentials.Add(
                    Encryption.EncryptString("Naomi"),
                    Encryption.EncryptString("123"));
                UserCredentials.employeeCredentials.Add(
                    Encryption.EncryptString("Oliver"),
                    Encryption.EncryptString("qwerty"));
                UserCredentials.employeeCredentials.Add(
                    Encryption.EncryptString("Sam"),
                    Encryption.EncryptString("password"));

                //We serialize the encrypted version of all our details 
                string json = JsonConvert.SerializeObject(employeeCredentials, Formatting.Indented);
                File.WriteAllText("EmployeeCredentials.json", json);
            }


        }
    }
}
