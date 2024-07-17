using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGT_BankingApplication
{

    //use these methods to encrypt all sensitve data (employee credentials and customer details e.t.c.)
    internal class Encryption
    {
        //Returns ascii values as string 
        public static string encryptString(string s)
        {
            //List<uint> CharValuesACSII = new List<uint>();
            List<string> ACSIIasSting = new List<string>();
            //gets ascii code of each character
            foreach (char c in s)
            {
                ACSIIasSting.Add(((uint)c).ToString());
                
            }
            string a = string.Join("*", ACSIIasSting);
            return a;
        }

        //decodeds encryped string
        public static string Decode(string EncryptedPasswordAsString)
        {
            string decodedString = "";
            string[] split = EncryptedPasswordAsString.Split("*", StringSplitOptions.RemoveEmptyEntries); //remove empty needed 
            foreach (string c in split)
            {
                decodedString += (char)UInt32.Parse(c);
            }
            return decodedString;
        }



    }
}
