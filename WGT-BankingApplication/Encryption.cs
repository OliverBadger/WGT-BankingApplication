using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGT_BankingApplication
{
    // Provides methods for encrypting and decrypting sensitive data such as employee credentials and customer details.
    internal class Encryption
    {
        // Encrypts a string by converting each character to its ASCII value and joining them with an asterisk separator.
        public static string EncryptString(string s)
        {
            // List to store the ASCII values of characters as strings.
            List<string> asciiAsString = new List<string>();

            // Iterate through each character in the input string.
            foreach (char c in s)
            {
                // Convert the character to its ASCII value (uint) and add it to the list as a string.
                asciiAsString.Add(((uint)c).ToString());
            }

            // Join the list of ASCII values into a single string, with each value separated by an asterisk '*'.
            string encryptedString = string.Join("*", asciiAsString);

            // Return the encrypted string.
            return encryptedString;
        }

        // Decodes an encrypted string by converting ASCII values back to characters.
        public static string Decode(string encryptedStringAsString)
        {
            // Initialize an empty string to build the decoded output.
            string decodedString = "";

            // Split the input encrypted string by the asterisk '*' separator into an array of ASCII value strings.
            string[] split = encryptedStringAsString.Split("*", StringSplitOptions.RemoveEmptyEntries);

            // Iterate through each ASCII value string in the array.
            foreach (string asciiValue in split)
            {
                // Parse the ASCII value string to a UInt32 (unsigned integer).
                // Convert the UInt32 value to its corresponding character and append it to the decoded string.
                decodedString += (char)UInt32.Parse(asciiValue);
            }

            // Return the fully decoded string.
            return decodedString;
        }
    }
}