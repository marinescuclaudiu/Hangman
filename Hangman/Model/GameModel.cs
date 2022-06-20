using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.Model
{
    public class GameModel : BaseModel
    {
        public GameModel()
        {
            LifeCounter = 6;
            CurrentLevel = 1;
            CurrentCategory = "All categories:";
            LetterUsed = new List<string>();
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged("Username"); }
        }

        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; OnPropertyChanged("ImagePath"); }
        }

        private string _healthBarImage;
        public string HealthBarImage
        {
            get { return _healthBarImage; }
            set { _healthBarImage = value; OnPropertyChanged("HealthBarImage"); }
        }

        private int _lifeCounter;
        public int LifeCounter
        {
            get { return _lifeCounter; }
            set { _lifeCounter = value; OnPropertyChanged("LifeCounter"); }
        }

        private string _hangmanImage;
        public string HangmanImage
        {
            get { return _hangmanImage; }
            set { _hangmanImage = value; OnPropertyChanged("HangmanImage"); }
        }

        private int _currentLevel;
        public int CurrentLevel
        {
            get { return _currentLevel; }
            set { _currentLevel = value; CurrentLevelString = "Level " + _currentLevel.ToString(); OnPropertyChanged("CurrentLevel"); }
        }

        private string _currentLevelString;
        public string CurrentLevelString
        {
            get { return _currentLevelString; }
            set { _currentLevelString = value; OnPropertyChanged("CurrentLevelString"); }
        }

        private string _currentWord;
        public string CurrentWord
        {
            get { return _currentWord; }
            set { _currentWord = value; OnPropertyChanged("CurrentWord"); }
        }

        private string _currentHiddenWord;
        public string CurrentHiddenWord
        {
            get { return _currentHiddenWord; }
            set { _currentHiddenWord = value; OnPropertyChanged("CurrentHiddenWord"); }
        }

        private string _currentCategory;
        public string CurrentCategory
        {
            get { return _currentCategory; }
            set { _currentCategory = value; OnPropertyChanged("CurrentCategory"); }
        }

        private List<string> _lettersUsed;
        public List<string> LetterUsed
        {
            get { return _lettersUsed; }
            set { _lettersUsed = value; OnPropertyChanged("LetterUsed"); }
        }

    }
}
