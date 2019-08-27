using System;
using System.Runtime.CompilerServices;


namespace Deloitte_CodingChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            string username = "";
            string password = "";

            Console.WriteLine("Hello and welcome to the Platform Engineering password strength checker tool!");

            Console.WriteLine("-----------------------------------------------------------------------------\n");
            
            Console.WriteLine("Please enter the following information:\n");

            Console.Write("Username: ");
            username = Console.ReadLine();

            Console.Write("Password: ");

            do
            {
                ConsoleKeyInfo character = Console.ReadKey(true);
                if (character.Key != ConsoleKey.Backspace && character.Key != ConsoleKey.Enter)
                {
                    password += character.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (character.Key == ConsoleKey.Enter)
                    {
                        //PasswordCheck.MinimumCharacterCheck(password);
                        //if (PasswordCheck.PasswordValidator(username, password))
                        //{
                            break;
                        //}
                        //password = "";
                        //Console.Write("Password: ");
                    }
                }
            } while (true);
            
            //while (PasswordCheck.MinimumCharacterCheck(password))


            Console.WriteLine("\n\nThe password strength is: " + PasswordCheck.PasswordStrength(username, password) + "\n");
            Console.WriteLine("The provided password has appeared in a data breach " + 0 + " times! \n");
        }
    }
}
