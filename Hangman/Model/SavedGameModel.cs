using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.Model
{
    public class SavedGameModel
    {
        public SavedGameModel(string username, string imagePath, int lifeCounter, int currentLevel, string currentWord,
            string currentHiddenWord,  string currentCategory, List<string> letterUsed, string timeLeft)
        {
            Username = username;
            ImagePath = imagePath;
            LifeCounter = lifeCounter;
            CurrentLevel = currentLevel;
            CurrentWord = currentWord;
            CurrentHiddenWord = currentHiddenWord;
            CurrentCategory = currentCategory;
            LetterUsed = letterUsed;
            TimeLeft = timeLeft;
        }

        public string Username { get; set; }

        public string ImagePath { get; set; }

        public int LifeCounter { get; set; }

        public int CurrentLevel { get; set; }

        public string CurrentWord { get; set; }

        public string CurrentHiddenWord { get; set; }

        public string CurrentCategory { get; set; }

        public List<string> LetterUsed { get; set; }

        public string TimeLeft { get; set; }

    }
}
