using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;


//Requires newtonsoft.json package to be installed 
// use in NuGet package console =>> Install-Package Newtonsoft.Json

namespace WGT_BankingApplication
{
    //This class should only be used to generate and store placeholder user data for the purpose of testing the applications main functionality

    //Users are generated ("Randomly") using a list of possible details, they are then stored in a json file.

    //for security given the nature of the app , all user information is encrypted proir to storage
    
    //TODO create suitable folder for json output

    //TOD create additional user info

    //TODO add user data encryptyion 
    
    //Depracated -> used for testing before integrating with customer class 
    //struct userStruct(uint _customerID,string _firstName,string _secondName)
    //{
    //    public uint CustomerID = _customerID;
    //    public string FirstName = _firstName;
    //    public string SecondName = _secondName;
    //}
        
    class CustomerGenerator
    {
        //json file will be stored in project directory
        static string path = Directory.GetCurrentDirectory();
        static string userDetails = "Users.JSON";

        const int NUMBER_OF_USERS = 10;
        static Customer[] userList = new Customer[NUMBER_OF_USERS];


        static string[] FirstNames = { "Trevor" , "Susan" , "Dave" , "Lucy" , "Vincent" , "Ria" , "Efosa" , "Kushi" , "Kenneth"};
        static string[] SecondNames = { "Smith" , "Winters" , "Stokes" , "Singh" , "Sen" , "Wu" , "Hope" , "Kane" , "Waltz" };


        string? ISANumber;
        string[]? PersonalAccountNumbers;
        string[]? BusinessAccountNumbers;

        public static Customer[] GenerateNewUserData()
        {
            //selects random names from array
            Random randomPick = new Random();
            for (int i=0; i < NUMBER_OF_USERS; i++)
            {
                // Generates customer Data
                string firstName = FirstNames[randomPick.Next(FirstNames.Length)];
                string secondName = SecondNames[randomPick.Next(SecondNames.Length)];
                string customerNumber = GenerateCustomerNumber(firstName, secondName);

                userList[i] = new Customer(i, firstName, secondName, "password", customerNumber);
            }
            string json = JsonConvert.SerializeObject(userList, Formatting.Indented);
            File.WriteAllText("Users.json", json);
            Console.WriteLine("users prented");

            return userList;
        }

        public static string GenerateCustomerNumber(string firstName, string lastName)
        {
            // Generate a unique customer number using the first four letters of the customer's first and last names
            string prefix = (firstName.Substring(0, Math.Min(4, firstName.Length)) + lastName.Substring(0, Math.Min(4, lastName.Length))).ToUpper();
            int number = GetNextCustomerNumber(prefix);
            return prefix + number.ToString("D6");
        }

        private static int GetNextCustomerNumber(string prefix)
        {
            // For simplicity, use the count of the existing users in the JSON file and ensure uniqueness per prefix
            if (File.Exists(userDetails))
            {
                var existingUsers = JsonConvert.DeserializeObject<Customer[]>(File.ReadAllText(userDetails));
                int maxNumber = 0;
                foreach (var user in existingUsers)
                {
                    if (user.CustomerNumber.StartsWith(prefix))
                    {
                        int userNumber = int.Parse(user.CustomerNumber.Substring(prefix.Length));
                        if (userNumber > maxNumber)
                        {
                            maxNumber = userNumber;
                        }
                    }
                }
                return maxNumber + 1;
            }
            return 1;
        }
    }
}

    

