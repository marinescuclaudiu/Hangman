using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.Model
{
    public class UserModel
    {
        public string Username { get; set; }
        public string ImagePath { get; set; }
        public UserModel(string username, string imagePath)
        {
            Username = username;
            ImagePath = imagePath;
        }
    }
}
