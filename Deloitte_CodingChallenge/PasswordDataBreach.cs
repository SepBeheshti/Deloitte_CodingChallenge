using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace Deloitte_CodingChallenge
{
    public class PasswordDataBreach
    {
        public static string HashPassword(string password)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hashedPassword = sha1.ComputeHash(Encoding.UTF8.GetBytes(password));
                var sb = new StringBuilder(hashedPassword.Length * 2);

                foreach (byte b in hashedPassword)
                {
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        public static string PartialHash(string hashedPassword)
        {
            return hashedPassword.Substring(0, 5);
        }

        public static string HashSuffix(string hashedPassword)
        {
            return hashedPassword.Substring(5);
        }

        public static string GetBreachCount(string hashedPrefix, string hashedSuffix, string password)
        {
            string matchedPassword = "";
            HttpClient httpClient = new HttpClient();
            string result = httpClient.GetStringAsync("https://api.pwnedpasswords.com/range/" + hashedPrefix).Result;
            string path = Directory.GetCurrentDirectory() + @"passwords.txt";

            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(result);
                }
            }

            var lines = File.ReadAllLines(path);

            try
            {
                foreach (var line in lines)
                {
                    if (line.Contains(hashedSuffix))
                    {
                        matchedPassword = line;
                        break;
                    }
                }

                String[] passwordCount = matchedPassword.Split(':');
                File.Delete(path);

                return passwordCount[1];
            }
            catch (IndexOutOfRangeException)
            {
                if (password.Length > 0)
                {
                    Console.WriteLine("\nThis Password has not been pwned!");
                }
            }

            
            return "";
        }
    }
}
