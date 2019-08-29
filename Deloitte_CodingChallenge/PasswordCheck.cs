using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Deloitte_CodingChallenge
{
    public class PasswordCheck
    {
        public static string UsernameNullCheck(string username)
        {
            if (String.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("Username", "A username must be provided!");
            }

            return username;
        }

        public static string PasswordNullCheck(string password)
        {
            if (String.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("Password", "The password must be filled in!");
            }

            return password;
        }


        public static bool isPasswordValid(string username, string password)
        {
            bool result;
            List<bool> allConditions = new List<bool>();
            var specialCharacterCheck = new Regex("^[a-zA-Z0-9 ]*$");

            try
            {
                 UsernameNullCheck(username);
                 PasswordNullCheck(password);

                if (password.Length < 8)
                 {
                     Console.WriteLine(
                         " The provided password is too short. Please provide a password with at least 8 characters!");
                     allConditions.Add(false);
                 }

                 if (password.ToLower().Contains(username))
                 {
                     Console.WriteLine(" Your password cannot contain your username.");
                     allConditions.Add(false);
                 }

                 if (specialCharacterCheck.IsMatch(password))
                 {
                     Console.WriteLine(" Your password must contain at least one special character!");
                     allConditions.Add(false);
                 }

                 if (!password.Any(Char.IsUpper))
                 {
                     Console.WriteLine(" Your password must contain at least one upper case character!");
                     allConditions.Add(false);
                 }

                 if (!password.Any(Char.IsNumber))
                 {
                     Console.WriteLine(" Your password must contain at least one number!");
                     allConditions.Add(false);
                 }
            }
            catch (ArgumentNullException e)
            {
                 Console.WriteLine(e.Message);
            }

            result = allConditions.Contains(false) ? false : true;

            return result;
        }


        public static double PasswordStrength(string username, string password)
        {
            double totalStrength;
            int lengthPassword = password.Length;
            int uniqueCharPool = 0;

            if (password.All(Char.IsLower))
            {
                uniqueCharPool += (int)PasswordEntropy.lowercase;
            }

            if ((password.Any(Char.IsLower) && password.Any(Char.IsUpper)) && !password.Any(Char.IsNumber))
            {
                uniqueCharPool += (int)PasswordEntropy.lowerAndUpperCase;
            }

            if (password.Any(Char.IsNumber) && password.Any(Char.IsLower) && !password.Any(Char.IsUpper))
            {
                uniqueCharPool += (int)PasswordEntropy.alphanumeric;
            }

            if ((password.Any(Char.IsLower) && password.Any(Char.IsUpper)) && password.Any(Char.IsNumber))
            {
                uniqueCharPool += (int)PasswordEntropy.alphanumericAndUpperCase;
            }

            totalStrength = Math.Ceiling(Math.Log(Math.Pow(uniqueCharPool, lengthPassword), 2));

            return totalStrength;
        }

    }
}
