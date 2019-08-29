using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Deloitte_CodingChallenge;

namespace PasswordStrengthTest
{
    [TestClass]
    public class PasswordTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NoUsernameProvided()
        {
            string username = "";
            string password = "sep";
            User testUser = new User(username, password);

            PasswordCheck.UsernameNullCheck(testUser.Username);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NoPasswordProvided()
        {
            string username = "Sep";
            string password = "";
            User testUser = new User(username, password);

            PasswordCheck.PasswordNullCheck(testUser.Password);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NoUsernameOrPasswordProvided()
        {
            string username = "";
            string password = "";
            User testUser = new User(username, password);

            PasswordCheck.UsernameNullCheck(testUser.Username);
            PasswordCheck.PasswordNullCheck(testUser.Password);
        }
        [TestMethod]
        public void FailsWhenPasswordIsTooShort()
        {
            string username = "Sep";
            string password = "test";
            bool expected = false;
            User testUser = new User(username, password);

            bool actual = PasswordCheck.isPasswordValid(testUser.Username, testUser.Password);
            Assert.AreEqual(expected, actual, "The password meets the length requirement");
        }
        [TestMethod]
        public void FailsWhenNoDigits_Password()
        {
            string username = "Sep";
            string password = "Testtest!";
            bool expected = false;
            User testUser = new User(username, password);

            bool actual = PasswordCheck.isPasswordValid(testUser.Username, testUser.Password);
            Assert.AreEqual(expected, actual, "The password has a digit provided");
        }
        [TestMethod]
        public void FailsWhenNoSpecialChar_Password()
        {
            string username = "Sep";
            string password = "Testtest12";
            bool expected = false;
            User testUser = new User(username, password);

            bool actual = PasswordCheck.isPasswordValid(testUser.Username, testUser.Password);
            Assert.AreEqual(expected, actual, "The password contains a special character");
        }
        [TestMethod]
        public void FailsWhenNoUpperCaseChar_Password()
        {
            string username = "Sep";
            string password = "testtest12!";
            bool expected = false;
            User testUser = new User(username, password);

            bool actual = PasswordCheck.isPasswordValid(testUser.Username, testUser.Password);
            Assert.AreEqual(expected, actual, "The password contains an upper case character");
        }
        [TestMethod]
        public void FailsWhenUsernameIsSameAsPassword()
        {
            string username = "Sep";
            string password = "Sepistesting123!";
            bool expected = false;
            User testUser = new User(username, password);

            bool actual = PasswordCheck.isPasswordValid(testUser.Username, testUser.Password.ToLower());
            Assert.AreEqual(expected, actual, "The password does not contain the username");
        }
        [TestMethod]
        public void PasswordRequirementsMet()
        {
            string username = "Sep";
            string password = "Thisisatest123!";
            bool expected = true;
            User testUser = new User(username, password);

            bool actual = PasswordCheck.isPasswordValid(testUser.Username, testUser.Password);
            Assert.AreEqual(expected, actual, "The password does not meet set requirements");
        }
        [TestMethod]
        public void VeryLongPassword()
        {
            string username = "Sep";
            string password = "Thisisatest1234Thisisarepeat1234!!";
            bool expected = true;
            User testUser = new User(username, password);

            bool actual = PasswordCheck.isPasswordValid(testUser.Username, testUser.Password);
            Assert.AreEqual(expected, actual, "The password does not meet set requirements");
        }
        [TestMethod]
        public void PasswordStrengthCheck_OnlyLowerCase()
        {
            string username = "Sep";
            string password = "testingpass";
            int lengthPassword = password.Length;
            int entropy = (int)PasswordEntropy.lowercase;

            double expected = Math.Ceiling(Math.Log(Math.Pow(entropy, lengthPassword), 2));
            User testUser = new User(username, password);

            double actual = PasswordCheck.PasswordStrength(testUser.Username, testUser.Password);
            Assert.AreEqual(expected, actual, "The password entropy does not match");
        }
        [TestMethod]
        public void PasswordStrengthCheck_LowerAndUpperCase_NoNumbers()
        {
            string username = "Sep";
            string password = "Testingthispassword";
            int lengthPassword = password.Length;
            int entropy = (int)PasswordEntropy.lowerAndUpperCase;

            double expected = Math.Ceiling(Math.Log(Math.Pow(entropy, lengthPassword), 2));
            User testUser = new User(username, password);

            double actual = PasswordCheck.PasswordStrength(testUser.Username, testUser.Password);
            Assert.AreEqual(expected, actual, "The password entropy does not match");
        }
        [TestMethod]
        public void PasswordStrengthCheck_AlphaNumericNoUpperCase()
        {
            string username = "Sep";
            string password = "thisisatest1";
            int lengthPassword = password.Length;
            int entropy = (int)PasswordEntropy.alphanumeric;

            double expected = Math.Ceiling(Math.Log(Math.Pow(entropy, lengthPassword), 2));
            User testUser = new User(username, password);

            double actual = PasswordCheck.PasswordStrength(testUser.Username, testUser.Password);
            Assert.AreEqual(expected, actual, "The password entropy does not match");
        }
        [TestMethod]
        public void PasswordStrengthCheck()
        {
            string username = "Sep";
            string password = "Thisisatest1!";
            int lengthPassword = password.Length;
            int entropy = (int)PasswordEntropy.alphanumericAndUpperCase;

            double expected = Math.Ceiling(Math.Log(Math.Pow(entropy, lengthPassword), 2));
            User testUser = new User(username, password);

            double actual = PasswordCheck.PasswordStrength(testUser.Username, testUser.Password);
            Assert.AreEqual(expected, actual, "The password entropy does not match");
        }
        [TestMethod]
        public void HashPassword_Alphanumeric()
        {
            string username = "Sep";
            string password = "test1234";

            string expected = "9BC34549D565D9505B287DE0CD20AC77BE1D3F2C";
            User testUser = new User(username, password);

            string actual = PasswordDataBreach.HashPassword(testUser.Password);
            Assert.AreEqual(expected, actual, "The password is not hashed correctly");
        }
        [TestMethod]
        public void HashPassword_OnlyWords()
        {
            string username = "Sep";
            string password = "Thisisatest";

            string expected = "0B6BCB20E39F1E70A0A3F87D2710D59B0200803C";
            User testUser = new User(username, password);

            string actual = PasswordDataBreach.HashPassword(testUser.Password);
            Assert.AreEqual(expected, actual, "The password is not hashed correctly");
        }
        [TestMethod]
        public void HashPassword_WrongPassword()
        {
            string username = "Sep";
            string password = "Thisisatest";

            string otherPassword = "ThisISatest";

            string expected = PasswordDataBreach.HashPassword(otherPassword);
            User testUser = new User(username, password);

            string actual = PasswordDataBreach.HashPassword(testUser.Password);
            Assert.AreNotEqual(expected, actual, "The password is not hashed correctly");
        }
        [TestMethod]
        public void HashPassword_GeneratedUpperCase()
        {
            string username = "Sep";
            string password = "Thisisatest";
            
            string expected = "0B6BCB20E39F1E70A0A3F87D2710D59B0200803C".ToLower();
            User testUser = new User(username, password);

            string actual = PasswordDataBreach.HashPassword(testUser.Password);
            Assert.AreNotEqual(expected, actual, "The password is not hashed correctly");
        }
        [TestMethod]
        public void PrefixHash_OnlyFiveCharacters()
        {
            string username = "Sep";
            string password = "Thisisatest";
            User testUser = new User(username, password);
            string passwordHash = PasswordDataBreach.HashPassword(testUser.Password);
            string passwordPrefix = PasswordDataBreach.PartialHash(passwordHash);
            int expected = 5;
            

            int actual = passwordPrefix.Length;
            Assert.AreEqual(expected, actual, "The password is not hashed correctly");
        }
        [TestMethod]
        public void PrefixHashMatching()
        {
            string username = "Sep";
            string password = "Thisisatest";
            User testUser = new User(username, password);
            string passwordHash = PasswordDataBreach.HashPassword(testUser.Password);
            string passwordPrefix = PasswordDataBreach.PartialHash(passwordHash);
            string expected = "0B6BC";


            string actual = passwordPrefix;
            Assert.AreEqual(expected, actual, "The password is not hashed correctly");
        }
        [TestMethod]
        public void SuffixHash()
        {
            string username = "Sep";
            string password = "Thisisatest";
            User testUser = new User(username, password);
            string passwordHash = PasswordDataBreach.HashPassword(testUser.Password);
            string passwordSuffix = PasswordDataBreach.HashSuffix(passwordHash);
            string expected = "B20E39F1E70A0A3F87D2710D59B0200803C";


            string actual = passwordSuffix;
            Assert.AreEqual(expected, actual, "The password is not hashed correctly");
        }
        [TestMethod]
        public void SuffixHash_NotContainingFiveCharacters()
        {
            string username = "Sep";
            string password = "Thisisatest";
            User testUser = new User(username, password);
            string passwordHash = PasswordDataBreach.HashPassword(testUser.Password);
            int passwordSuffix = PasswordDataBreach.HashSuffix(passwordHash).Length;
            int expected = 35;


            int actual = passwordSuffix;
            Assert.AreEqual(expected, actual, "The password is not hashed correctly");
        }
        [TestMethod]
        public async Task APIStatusCode()
        {
            string username = "Sep";
            string password = "Thisisatest";
            User testUser = new User(username, password);
            string passwordHash = PasswordDataBreach.HashPassword(testUser.Password);
            string hashedPrefix = PasswordDataBreach.PartialHash(passwordHash);

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync("https://api.pwnedpasswords.com/range/" + hashedPrefix);
            string statusCode = response.StatusCode.ToString();
            string expected = "OK";


            string actual = statusCode;
            Assert.AreEqual(expected, actual, "The status is not 200");
        }
        [TestMethod]
        public async Task APIMethod()
        {
            string username = "Sep";
            string password = "Thisisatest";
            User testUser = new User(username, password);
            string passwordHash = PasswordDataBreach.HashPassword(testUser.Password);
            string hashedPrefix = PasswordDataBreach.PartialHash(passwordHash);

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync("https://api.pwnedpasswords.com/range/" + hashedPrefix);
            string responseMethod = response.RequestMessage.Method.ToString();
            string expected = "GET";


            string actual = responseMethod;
            Assert.AreEqual(expected, actual, "The request must be a GET request");
        }
        [TestMethod]
        public async Task APIContentType()
        {
            string username = "Sep";
            string password = "Thisisatest";
            User testUser = new User(username, password);
            string passwordHash = PasswordDataBreach.HashPassword(testUser.Password);
            string hashedPrefix = PasswordDataBreach.PartialHash(passwordHash);

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync("https://api.pwnedpasswords.com/range/" + hashedPrefix);
            string responseMethod = response.Content.Headers.ContentType.ToString();
            string expected = "text/plain";


            string actual = responseMethod;
            Assert.AreEqual(expected, actual, "The wrong type is being returned by API");
        }
        [TestMethod]
        public void PasswordBreach_ReturnsResults()
        {
            string username = "Sep";
            string password = "Thisisatest";
            User testUser = new User(username, password);
            string passwordHash = PasswordDataBreach.HashPassword(testUser.Password);
            string hashedPrefix = PasswordDataBreach.PartialHash(passwordHash);
            string hashedSuffix = PasswordDataBreach.HashSuffix(passwordHash);

            string result = PasswordDataBreach.GetBreachCount(hashedPrefix, hashedSuffix, testUser.Password);
            
            bool expected = true;
            
            bool actual = result.Length > 0;
            Assert.AreEqual(expected, actual, "The password has not been leaked");
        }
        [TestMethod]
        public void PasswordBreach_FoundCount()
        {
            string username = "Sep";
            string password = "test1234";
            User testUser = new User(username, password);
            string passwordHash = PasswordDataBreach.HashPassword(testUser.Password);
            string hashedPrefix = PasswordDataBreach.PartialHash(passwordHash);
            string hashedSuffix = PasswordDataBreach.HashSuffix(passwordHash);

            string result = PasswordDataBreach.GetBreachCount(hashedPrefix, hashedSuffix, testUser.Password);

            string expected = "45011";

            string actual = result;
            Assert.AreEqual(expected, actual, "The password has not been leaked");
        }
        [TestMethod]
        public void PasswordBreach_NotPwned()
        {
            string username = "Sep";
            string password = "ThispasswordIsProb4blyn0tpwned!";
            User testUser = new User(username, password);
            string passwordHash = PasswordDataBreach.HashPassword(testUser.Password);
            string hashedPrefix = PasswordDataBreach.PartialHash(passwordHash);
            string hashedSuffix = PasswordDataBreach.HashSuffix(passwordHash);

            string result = PasswordDataBreach.GetBreachCount(hashedPrefix, hashedSuffix, testUser.Password);

            string expected = "";

            string actual = result;
            Assert.AreEqual(expected, actual, "The password has been leaked");
        }
        [TestMethod]
        public void VeryWeakPasswordCheck()
        {
            string username = "Sep";
            string password = "test";
            User testUser = new User(username, password);
            double passwordStrength = PasswordCheck.PasswordStrength(testUser.Username,testUser.Password);
            string passwordStrengthCategory = PasswordCheck.PasswordStrengthCategory(passwordStrength);

            string expected = "Very Weak Password";

            Assert.AreEqual(expected, passwordStrengthCategory, "The password is not very weak");
        }
        [TestMethod]
        public void WeakPasswordCheck()
        {
            string username = "Sep";
            string password = "test12";
            User testUser = new User(username, password);
            double passwordStrength = PasswordCheck.PasswordStrength(testUser.Username, testUser.Password);
            string passwordStrengthCategory = PasswordCheck.PasswordStrengthCategory(passwordStrength);

            string expected = "Weak Password";

            Assert.AreEqual(expected, passwordStrengthCategory, "The password is not weak");
        }
        [TestMethod]
        public void ReasonablePasswordCheck()
        {
            string username = "Sep";
            string password = "test12!";
            User testUser = new User(username, password);
            double passwordStrength = PasswordCheck.PasswordStrength(testUser.Username, testUser.Password);
            string passwordStrengthCategory = PasswordCheck.PasswordStrengthCategory(passwordStrength);

            string expected = "Reasonable Password";

            Assert.AreEqual(expected, passwordStrengthCategory, "The password is not reasonable");
        }
        [TestMethod]
        public void StrongPasswordCheck()
        {
            string username = "Sep";
            string password = "Thisisatest1234!";
            User testUser = new User(username, password);
            double passwordStrength = PasswordCheck.PasswordStrength(testUser.Username, testUser.Password);
            string passwordStrengthCategory = PasswordCheck.PasswordStrengthCategory(passwordStrength);

            string expected = "Strong Password";

            Assert.AreEqual(expected, passwordStrengthCategory, "The password is not strong");
        }
        [TestMethod]
        public void VeryStrongPasswordCheck()
        {
            string username = "Sep";
            string password = "AVery!C0mpL3xP4ssW0rd$!159874";
            User testUser = new User(username, password);
            double passwordStrength = PasswordCheck.PasswordStrength(testUser.Username, testUser.Password);
            string passwordStrengthCategory = PasswordCheck.PasswordStrengthCategory(passwordStrength);

            string expected = "Very Strong Password";

            Assert.AreEqual(expected, passwordStrengthCategory, "The password is not very strong");
        }


    }
}
