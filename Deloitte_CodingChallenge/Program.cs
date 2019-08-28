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

                string hashPassword = PasswordDataBreach.HashPassword(user.Password);
                string partialHash = PasswordDataBreach.PartialHash(hashPassword);
                string hashSuffix = PasswordDataBreach.HashSuffix(hashPassword);
                string passwordBreachCount = PasswordDataBreach.GetBreachCount(partialHash, hashSuffix);

               // Console.WriteLine(PasswordDataBreach.PartialHash(PasswordDataBreach.HashPassword(user.Password)));
               // Console.WriteLine(PasswordDataBreach.HashSuffix(PasswordDataBreach.HashPassword(user.Password)));
               // Console.WriteLine(PasswordDataBreach.GetBreachCount(PasswordDataBreach.PartialHash(PasswordDataBreach.HashPassword(user.Password)), (PasswordDataBreach.HashSuffix(PasswordDataBreach.HashPassword(user.Password)))));

                Console.WriteLine("\nThe password strength is: " +
                                  PasswordCheck.PasswordStrength(user.Username, user.Password) + "\n");
                if (passwordBreachCount.Length > 0)
                {
                    Console.WriteLine("The provided password has appeared in a data breach " + passwordBreachCount +
                                      " times! \n");
                }

                Console.Write("\nWould you like to calculate one more time? (Y/N) ");
                text = Console.ReadLine();
                Console.Write("\n");
            } while (text.ToLower() == "y");
        }
    }
}
