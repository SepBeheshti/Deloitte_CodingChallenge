using System;
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
        public void ShortPassword()
        {
            string username = "Sep";
            string password = "test";
            bool expected = false;
            User testUser = new User(username, password);

            bool actual = PasswordCheck.PasswordValidator(testUser.Username, testUser.Password);
            Assert.AreEqual(expected, actual, "The password meets the length requirement");
        }
        [TestMethod]
        public void NoDigits_Password()
        {
            string username = "Sep";
            string password = "Testtest!";
            bool expected = false;
            User testUser = new User(username, password);

            bool actual = PasswordCheck.PasswordValidator(testUser.Username, testUser.Password);
            Assert.AreEqual(expected, actual, "The password has a digit provided");
        }
        [TestMethod]
        public void NoSpecialChar_Password()
        {
            string username = "Sep";
            string password = "Testtest12";
            bool expected = false;
            User testUser = new User(username, password);

            bool actual = PasswordCheck.PasswordValidator(testUser.Username, testUser.Password);
            Assert.AreEqual(expected, actual, "The password contains a special character");
        }
        [TestMethod]
        public void NoUpperCaseChar_Password()
        {
            string username = "Sep";
            string password = "testtest12!";
            bool expected = false;
            User testUser = new User(username, password);

            bool actual = PasswordCheck.PasswordValidator(testUser.Username, testUser.Password);
            Assert.AreEqual(expected, actual, "The password contains an upper case character");
        }
        [TestMethod]
        public void UsernameSameAsPassword()
        {
            string username = "Sep";
            string password = "Sepistesting123!";
            bool expected = false;
            User testUser = new User(username, password);

            bool actual = PasswordCheck.PasswordValidator(testUser.Username, testUser.Password.ToLower());
            Assert.AreEqual(expected, actual, "The password does not contain the username");
        }
        [TestMethod]
        public void PasswordRequirementsMet()
        {
            string username = "Sep";
            string password = "Thisisatest123!";
            bool expected = true;
            User testUser = new User(username, password);

            bool actual = PasswordCheck.PasswordValidator(testUser.Username, testUser.Password);
            Assert.AreEqual(expected, actual, "The password does not meet set requirements");
        }
        [TestMethod]
        public void VeryLongPassword()
        {
            string username = "Sep";
            string password = "Thisisatest1234Thisisarepeat1234!!";
            bool expected = true;
            User testUser = new User(username, password);

            bool actual = PasswordCheck.PasswordValidator(testUser.Username, testUser.Password);
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
            User testUser1 = new User(username, password);

            double actual = PasswordCheck.PasswordStrength(testUser1.Username, testUser1.Password);
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
            User testUser2 = new User(username, password);

            double actual = PasswordCheck.PasswordStrength(testUser2.Username, testUser2.Password);
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
            User testUser2 = new User(username, password);

            double actual = PasswordCheck.PasswordStrength(testUser2.Username, testUser2.Password);
            Assert.AreEqual(expected, actual, "The password entropy does not match");
        }

    }
}
