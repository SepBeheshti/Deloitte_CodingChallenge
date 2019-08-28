using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;


namespace Deloitte_CodingChallenge
{
    public class Program
    {
        static void Main(string[] args)
        {
            string text;
            do
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
                    if (character.Key != ConsoleKey.Backspace && character.Key != ConsoleKey.Enter &&
                        character.Key != ConsoleKey.Spacebar)
                    {
                        password += character.KeyChar;
                        Console.Write("*");
                    }
                    else
                    {
                        if (character.Key == ConsoleKey.Enter)
                        {
                            Console.WriteLine("\n");
                            break;
                        }
                    }
                } while (true);

                User user = new User(username, password);
                PasswordCheck.PasswordValidator(user.Username, user.Password);

                Console.WriteLine("\nThe password strength is: " +
                                  PasswordCheck.PasswordStrength(user.Username, user.Password) + "\n");
                Console.WriteLine("The provided password has appeared in a data breach " + 0 + " times! \n");
                Console.Write("\nWould you like to calculate one more time? (Y/N) ");
                text = Console.ReadLine();
                Console.Write("\n");
            } while (text.ToLower() == "y");
        }
    }
}
