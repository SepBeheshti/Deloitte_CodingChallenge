using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Deloitte_CodingChallenge
{
    class PasswordCheck
    {

        public static bool PasswordValidator(string username, string password)
        {
            bool result = true;
            List<bool> allConditions = new List<bool>();
            string setPassword = password;
            var specialCharacterCheck = new Regex("^[a-zA-Z0-9 ]*$");

            if (password.Length < 8)
            {
                Console.WriteLine("  The provided password is too short. Please provide a password with at least 8 characters!");
                allConditions.Add(false);
            }

            if (setPassword.Contains(username))
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

            result = allConditions.Contains(false) ? false : true;

            return result;
        }


        public static double PasswordStrength(string username, string password)
        {
            char[] passwordChars = password.ToCharArray();
            double totalStrength;
            int lengthPassword = password.Length;
            int uniqueCharPool = 0;

            if (password.Any(Char.IsLower))
            {
                uniqueCharPool += (int)PasswordEntropy.lowercase;
            }

            if (password.Any(Char.IsLower) && password.Any(Char.IsUpper))
            {
                uniqueCharPool += (int)PasswordEntropy.lowerAndUpperCase;
            }

            if (password.Any(Char.IsLetterOrDigit))
            {
                uniqueCharPool += (int)PasswordEntropy.alphanumeric;
            }

            if (password.Any(Char.IsLetterOrDigit) && password.Any(Char.IsUpper))
            {
                uniqueCharPool += (int)PasswordEntropy.alphanumericAndUpperCase;
            }

            totalStrength = Math.Ceiling(Math.Log(Math.Pow(uniqueCharPool, lengthPassword), 2));

            return totalStrength;
        }

    }
}
