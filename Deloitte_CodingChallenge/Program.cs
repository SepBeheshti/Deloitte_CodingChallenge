using System;


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
                        character.Key != ConsoleKey.Spacebar && (char.IsLetterOrDigit(character.KeyChar)
                        || char.IsPunctuation(character.KeyChar) || char.IsSymbol(character.KeyChar)))
                    {
                        password += character.KeyChar;
                        Console.Write("*");
                    }
                    else
                    {
                        if (character.Key == ConsoleKey.Backspace && password.Length > 0)
                        {
                            password = password.Substring(0, (password.Length - 1));
                            Console.Write("\b \b");
                        }
                        else if (character.Key == ConsoleKey.Enter)
                        {
                            Console.WriteLine("\n");
                            break;
                        }
                    }
                } while (true);

                User user = new User(username, password);
                PasswordCheck.isPasswordValid(user.Username, user.Password);
                if (user.Username != "" && user.Password != "")
                {
                    //PasswordCheck.isPasswordValid(user.Username, user.Password);

                    string hashPassword = PasswordDataBreach.HashPassword(user.Password);
                    string partialHash = PasswordDataBreach.PartialHash(hashPassword);
                    string hashSuffix = PasswordDataBreach.HashSuffix(hashPassword);
                    double passwordStrength = PasswordCheck.PasswordStrength(user.Username, user.Password);
                    string passwordStrengthCategory = PasswordCheck.PasswordStrengthCategory(passwordStrength);
                    string passwordBreachCount =
                        PasswordDataBreach.GetBreachCount(partialHash, hashSuffix, user.Password);

                    Console.WriteLine("\nThe password strength is: " + passwordStrength);
                    Console.Write("The entered password is a " + passwordStrengthCategory);
                    Console.WriteLine("\n");

                    if (passwordBreachCount.Length > 0)
                    {
                        Console.WriteLine("The provided password has appeared in a data breach " + passwordBreachCount +
                                          " times! \n");
                    }
                }

                Console.Write("\nWould you like to calculate one more time? (Y/N) ");
                text = Console.ReadLine();
                Console.Write("\n");

            } while (text.ToLower() == "y");
        }
    }
}
