using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Deloitte_CodingChallenge
{
    public class User
    {
        private string username;
        private string password;

        private User() { }

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public string Username => username;
        
        public string Password => password;
        
    }
}
