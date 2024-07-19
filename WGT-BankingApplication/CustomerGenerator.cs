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
    /*  This class should only be used to generate and store placeholder user data for the purpose of testing the applications main functionality
     *  Users are generated ("Randomly") using a list of possible details, they are then stored in a json file.
     *  for security given the nature of the app , all user information is encrypted proir to storage
     */

    //Depracated -> used for testing before integrating with customer class 
    //struct userStruct(uint _customerID,string _firstName,string _secondName)
    //{
    //    public uint CustomerID = _customerID;
    //    public string FirstName = _firstName;
    //    public string SecondName = _secondName;
    //}

    class CustomerGenerator
    {
        // Path to store the JSON file in the project directory
        private string _path = Directory.GetCurrentDirectory();
        private string _userDetails = "Users.JSON";
        
        // Number of users to generate
        private const int NUMBER_OF_USERS = 10;
        
        // Array to store generated user data
        private Customer[] _userList = new Customer[NUMBER_OF_USERS];
        
        // Arrays of possible first and last names for users
        private string[] _firstNames = { "Trevor", "Susan", "Dave", "Lucy", "Vincent", "Ria", "Efosa", "Kushi", "Kenneth" };
        private string[] _secondNames = { "Smith", "Winters", "Stokes", "Singh", "Sen", "Wu", "Hope", "Kane", "Waltz" };
        
        // Optional account-related fields
        //private string? _ISANumber;
        //private string[]? _personalAccountNumbers;
        //private string[]? _businessAccountNumbers;

        // Constructor to ensure functionality
        public CustomerGenerator() { }

        // Generates new user data and returns an array of Customer objects
        public Customer[] GenerateNewUserData()
        {
            // Selects random names from array
            Random randomPick = new Random();
            for (int i = 0; i < NUMBER_OF_USERS; i++)
            {
                // Generates customer Data
                string firstName = _firstNames[randomPick.Next(_firstNames.Length)];
                string secondName = _secondNames[randomPick.Next(_secondNames.Length)];
                string customerNumber = GenerateCustomerNumber(firstName, secondName);

                // Create a new Customer object and adds it to the userList array
                _userList[i] = new Customer(i, firstName, secondName, "password", customerNumber);
            }
            // Serializes the userList array to JSON and writes it to a file called Users.json
            string json = JsonConvert.SerializeObject(_userList, Formatting.Indented);
            File.WriteAllText("Users.json", json);
            Console.WriteLine("users printed");

            return _userList;
        }

        // Generates a unique customer number based on the customer's first and last names
        public string GenerateCustomerNumber(string firstName, string lastName)
        {
            // Generate a prefix using the first four letters of the customer's first and last names
            string prefix = (firstName.Substring(0, Math.Min(4, firstName.Length)) + lastName.Substring(0, Math.Min(4, lastName.Length))).ToUpper();
            // Generate the next unique customer number for the given prefix
            int number = GetNextCustomerNumber(prefix);
            // Return the full customer number
            return prefix + number.ToString("D6");
        }

        // Gets the next unique customer number for a given prefix
        private int GetNextCustomerNumber(string prefix)
        {
            // For simplicity, use the count of the existing users in the JSON file and ensure uniqueness per prefix
            if (File.Exists(_userDetails))
            {
                // Deserialize the existing users from the JSON file
                var existingUsers = JsonConvert.DeserializeObject<Customer[]>(File.ReadAllText(_userDetails));
                int maxNumber = 0;

                // Iterate through existing users to find the maximum customer number with the same prefix
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
                // Return the next customer number
                return maxNumber + 1;
            }
            // Returns 1 if no existing users
            return 1;
        }
    }
}