using System.IO.Compression;
using ErikLabb4.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApp.HelperClasses
{
    class GetUserInput
    {
        public static int ValidateInt(string prompt)
        {
            Console.WriteLine(prompt);
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int userNumber))
                {
                    return userNumber;
                }
                else
                {
                    Console.WriteLine("You must enter a valid integer");
                }
            }
        }
        public static string ValidateString(string prompt)
        {
            Console.WriteLine(prompt);
            while (true)
            {
                string? userString = Console.ReadLine();
                if (!string.IsNullOrEmpty(userString) && !string.IsNullOrWhiteSpace(userString))
                {
                    return userString;
                }
                else
                {
                    Console.WriteLine("You must enter a valid string");
                }

            }
        }
    }

}