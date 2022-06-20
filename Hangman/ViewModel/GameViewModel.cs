using Hangman.Commands;
using Hangman.Model;
using Hangman.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using Newtonsoft.Json;
using System.Windows.Threading;

namespace Hangman.ViewModel
{
    public class GameViewModel : BaseViewModel
    {
        public GameViewModel()
        {
            AboutDialog = new AboutViewModel();
            GameModel = new GameModel();
            CreateLettersDictionary();
            LoadHangmanImage();
            LoadHealthBarImage();
            LoadWord();
        }

        public AboutViewModel AboutDialog { get; set; }

        public GameModel GameModel { get; set; }

        public Dictionary<string, bool> LetterEnable { get; set; }

        private string _timerOn;
        public string TimerOn
        {
            get { return _timerOn; }
            set { _timerOn = value; OnPropertyChanged("TimerOn"); }
        }

        private string _timeLeft;

        public string TimeLeft
        {
            get { return _timeLeft; }
            set
            {
                _timeLeft = value;
                OnPropertyChanged("TimeLeft");
                if (_timeLeft == "0")
                {
                    LostGame();
                }
            }
        }

        private void CreateLettersDictionary()
        {
            LetterEnable = new Dictionary<string, bool>();

            LetterEnable.Add("Q", true);
            LetterEnable.Add("W", true);
            LetterEnable.Add("E", true);
            LetterEnable.Add("R", true);
            LetterEnable.Add("T", true);
            LetterEnable.Add("Y", true);
            LetterEnable.Add("U", true);
            LetterEnable.Add("I", true);
            LetterEnable.Add("O", true);
            LetterEnable.Add("P", true);

            LetterEnable.Add("A", true);
            LetterEnable.Add("S", true);
            LetterEnable.Add("D", true);
            LetterEnable.Add("F", true);
            LetterEnable.Add("G", true);
            LetterEnable.Add("H", true);
            LetterEnable.Add("J", true);
            LetterEnable.Add("K", true);
            LetterEnable.Add("L", true);

            LetterEnable.Add("Z", true);
            LetterEnable.Add("X", true);
            LetterEnable.Add("C", true);
            LetterEnable.Add("V", true);
            LetterEnable.Add("B", true);
            LetterEnable.Add("N", true);
            LetterEnable.Add("M", true);
        }

        private void EnableAllLetters()
        {
            List<string> keys = new List<string>();

            foreach (var item in LetterEnable)
            {
                keys.Add(item.Key);
            }

            foreach (var key in keys)
            {
                LetterEnable[key] = true;
            }
            OnPropertyChanged("LetterEnable");

            GameModel.LetterUsed = new List<string>();
        }

        private void DisableAllLetters()
        {
            List<string> keys = new List<string>();

            foreach (var item in LetterEnable)
            {
                keys.Add(item.Key);
            }

            foreach (var key in keys)
            {
                LetterEnable[key] = false;
            }
            OnPropertyChanged("LetterEnable");

            GameModel.LetterUsed = keys;
        }

        private void LoadLettersFromSalvation()
        {
            List<string> keys = new List<string>();

            foreach (var item in LetterEnable)
            {
                keys.Add(item.Key);
            }

            foreach (var key in keys)
            {
                LetterEnable[key] = true;
            }

            foreach (var letter in GameModel.LetterUsed)
            {
                LetterEnable[letter] = false;
            }
            OnPropertyChanged("LetterEnable");
        }

        private void LoadHangmanImage()
        {
            switch (GameModel.LifeCounter)
            {
                case 0:
                    GameModel.HangmanImage = "../Images/HangmanImages/hangman_stage_6.jpg";
                    break;
                case 1:
                    GameModel.HangmanImage = "../Images/HangmanImages/hangman_stage_5.jpg";
                    break;
                case 2:
                    GameModel.HangmanImage = "../Images/HangmanImages/hangman_stage_4.jpg";
                    break;
                case 3:
                    GameModel.HangmanImage = "../Images/HangmanImages/hangman_stage_3.jpg";
                    break;
                case 4:
                    GameModel.HangmanImage = "../Images/HangmanImages/hangman_stage_2.jpg";
                    break;
                case 5:
                    GameModel.HangmanImage = "../Images/HangmanImages/hangman_stage_1.jpg";
                    break;
                case 6:
                    GameModel.HangmanImage = "../Images/HangmanImages/hangman_stage_0.jpg";
                    break;
            }
        }

        private void LoadHealthBarImage()
        {
            switch (GameModel.LifeCounter)
            {
                case 0:
                    GameModel.HealthBarImage = "../Images/HealthBarImages/health_bar_0.png";
                    break;
                case 1:
                    GameModel.HealthBarImage = "../Images/HealthBarImages/health_bar_1.png";
                    break;
                case 2:
                    GameModel.HealthBarImage = "../Images/HealthBarImages/health_bar_2.png";
                    break;
                case 3:
                    GameModel.HealthBarImage = "../Images/HealthBarImages/health_bar_3.png";
                    break;
                case 4:
                    GameModel.HealthBarImage = "../Images/HealthBarImages/health_bar_4.png";
                    break;
                case 5:
                    GameModel.HealthBarImage = "../Images/HealthBarImages/health_bar_5.png";
                    break;
                case 6:
                    GameModel.HealthBarImage = "../Images/HealthBarImages/health_bar_6.png";
                    break;
            }
        }

        private void LoadWord()
        {
            List<string> wordsList = new List<string>();
            if (GameModel.CurrentCategory == "All categories:")
            {
                using (StreamReader streamReader = File.OpenText("..\\..\\..\\Resources\\Categories\\All.txt"))
                {
                    string line = null;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        wordsList.Add(line);
                    }
                }
            }
            else if (GameModel.CurrentCategory == "Animals:")
            {
                using (StreamReader streamReader = File.OpenText("..\\..\\..\\Resources\\Categories\\Animals.txt"))
                {
                    string line = null;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        wordsList.Add(line);
                    }
                }
            }
            else if (GameModel.CurrentCategory == "Cars:")
            {
                using (StreamReader streamReader = File.OpenText("..\\..\\..\\Resources\\Categories\\Cars.txt"))
                {
                    string line = null;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        wordsList.Add(line);
                    }
                }
            }
            else if (GameModel.CurrentCategory == "Countries:")
            {
                using (StreamReader streamReader = File.OpenText("..\\..\\..\\Resources\\Categories\\Countries.txt"))
                {
                    string line = null;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        wordsList.Add(line);
                    }
                }
            }
            else if (GameModel.CurrentCategory == "Movies:")
            {
                using (StreamReader streamReader = File.OpenText("..\\..\\..\\Resources\\Categories\\Movies.txt"))
                {
                    string line = null;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        wordsList.Add(line);
                    }
                }
            }

            Random random = new Random();
            int indexWord = random.Next(0, wordsList.Count - 1);
            GameModel.CurrentWord = wordsList[indexWord];
            string hiddenWord = "";

            for (int indexLetter = 0; indexLetter < GameModel.CurrentWord.Length; indexLetter++)
            {
                if (Char.IsLetter(GameModel.CurrentWord[indexLetter]))
                {
                    hiddenWord += '_';
                    hiddenWord += ' ';
                }
                else hiddenWord += GameModel.CurrentWord[indexLetter];
            }
            GameModel.CurrentHiddenWord = hiddenWord;
        }

        private void NextLevel()
        {
            GameModel.CurrentLevel++;
            GameModel.LifeCounter = 6;
            EnableAllLetters();
            LoadHangmanImage();
            LoadHealthBarImage();
            LoadWord();
            TimeLeft = "30";
        }

        private void WonGame()
        {
            TimerOn = "False";
            WonGameView wonGameView = new WonGameView();
            wonGameView.ShowDialog();
            DisableAllLetters();

            List<string> userStatisticsJSON = new List<string>();
            using (StreamReader streamReader = File.OpenText("..\\..\\..\\Resources\\Statistics.json"))
            {
                string line = null;
                while ((line = streamReader.ReadLine()) != null)
                {
                    userStatisticsJSON.Add(line);
                }
            }
            using (StreamWriter streamWriter = File.CreateText("..\\..\\..\\Resources\\Statistics.json"))
            {
                for (int indexUserJSON = 0; indexUserJSON < userStatisticsJSON.Count; indexUserJSON++)
                {
                    if (!userStatisticsJSON[indexUserJSON].Contains(GameModel.Username))
                        streamWriter.WriteLine(userStatisticsJSON[indexUserJSON]);
                    else
                    {
                        UserStatisticModel currentUser = JsonConvert.DeserializeObject<UserStatisticModel>(userStatisticsJSON[indexUserJSON]);
                        if (GameModel.CurrentCategory == "All categories:")
                            currentUser.AllCategoriesWins++;

                        if (GameModel.CurrentCategory == "Animals:")
                            currentUser.AnimalsWins++;

                        if (GameModel.CurrentCategory == "Cars:")
                            currentUser.CarsWins++;

                        if (GameModel.CurrentCategory == "Countries:")
                            currentUser.CountriesWins++;

                        if (GameModel.CurrentCategory == "Movies:")
                            currentUser.MoviesWins++;

                        currentUser.GamesPlayed++;

                        string newUserStatisticsJSON = JsonConvert.SerializeObject(currentUser);

                        streamWriter.WriteLine(newUserStatisticsJSON);
                    }
                }
            }

        }

        private void LostGame()
        {
            TimerOn = "False";
            LostGameView lostGameView = new LostGameView();
            lostGameView.ShowDialog();
            DisableAllLetters();

            List<string> userStatisticsJSON = new List<string>();
            using (StreamReader streamReader = File.OpenText("..\\..\\..\\Resources\\Statistics.json"))
            {
                string line = null;
                while ((line = streamReader.ReadLine()) != null)
                {
                    userStatisticsJSON.Add(line);
                }
            }
            using (StreamWriter streamWriter = File.CreateText("..\\..\\..\\Resources\\Statistics.json"))
            {
                for (int indexUserJSON = 0; indexUserJSON < userStatisticsJSON.Count; indexUserJSON++)
                {
                    if (!userStatisticsJSON[indexUserJSON].Contains(GameModel.Username))
                        streamWriter.WriteLine(userStatisticsJSON[indexUserJSON]);
                    else
                    {
                        UserStatisticModel currentUser = JsonConvert.DeserializeObject<UserStatisticModel>(userStatisticsJSON[indexUserJSON]);
                        currentUser.GamesPlayed++;
                        string newUserStatisticsJSON = JsonConvert.SerializeObject(currentUser);
                        streamWriter.WriteLine(newUserStatisticsJSON);
                    }
                }
            }
        }

        private void PressLetter(object parameter)
        {
            string letterPressed = (string)parameter;
            GameModel.LetterUsed.Add(letterPressed);

            string newCurrentHiddenWord = "";
            if (GameModel.CurrentWord.Contains(letterPressed))
            {
                for (int indexLetter = 0; indexLetter < GameModel.CurrentWord.Length; indexLetter++)
                {
                    if (GameModel.CurrentWord[indexLetter].ToString() == letterPressed)
                    {
                        newCurrentHiddenWord += letterPressed;
                    }
                    else
                    {
                        newCurrentHiddenWord += GameModel.CurrentHiddenWord[indexLetter * 2];
                    }
                    newCurrentHiddenWord += ' ';
                }
                GameModel.CurrentHiddenWord = newCurrentHiddenWord;
                if (!GameModel.CurrentHiddenWord.Contains("_"))
                {
                    if (GameModel.CurrentLevel == 5)
                    {
                        WonGame();
                        return;
                    }

                    NextLevel();
                    return;
                }
            }
            else
            {
                GameModel.LifeCounter--;
                LoadHangmanImage();
                LoadHealthBarImage();
                if (GameModel.LifeCounter == 0)
                {
                    LostGame();
                    return;
                }
            }
            LetterEnable[letterPressed] = false;
            OnPropertyChanged("LetterEnable");
        }

        private ICommand m_pressLetter;

        public ICommand PressLetterComand
        {
            get
            {
                if (m_pressLetter == null)
                    m_pressLetter = new RelayCommand(PressLetter);
                return m_pressLetter;
            }
        }

        private void ChangeCategory(object parameter)
        {
            string category = (string)parameter;
            category += ':';

            GameModel.CurrentCategory = category;
            GameModel.CurrentLevel = 1;
            GameModel.LifeCounter = 6;
            EnableAllLetters();
            LoadHangmanImage();
            LoadHealthBarImage();
            LoadWord();
        }

        private ICommand m_changeCategory;

        public ICommand ChangeCategoryCommand
        {
            get
            {
                if (m_changeCategory == null)
                    m_changeCategory = new RelayCommand(ChangeCategory);
                return m_changeCategory;
            }
        }

        private void NewGame(object parameter)
        {
            GameModel.CurrentLevel = 1;
            GameModel.LifeCounter = 6;
            EnableAllLetters();
            LoadHangmanImage();
            LoadHealthBarImage();
            LoadWord();
        }

        private ICommand m_newGame;

        public ICommand NewGameCommand
        {
            get
            {
                if (m_newGame == null)
                    m_newGame = new RelayCommand(NewGame);
                return m_newGame;
            }
        }

        private void OpenGame(object paramater)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON Files (*.json)|*.json|All files (*.*)|*.*";

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\Resources\\SavedGames");
            openFileDialog.InitialDirectory = Path.GetFullPath(folderPath);

            if (openFileDialog.ShowDialog() == true)
            {
                using (StreamReader streamReader = File.OpenText(openFileDialog.FileName))
                {
                    string savedGameJSON = streamReader.ReadLine();

                    SavedGameModel savedGame = JsonConvert.DeserializeObject<SavedGameModel>(savedGameJSON);

                    if (savedGame.Username != GameModel.Username)
                    {
                        AccessDeniedView accessDeniedView = new AccessDeniedView();
                        accessDeniedView.ShowDialog();
                    }
                    else
                    {
                        TimeLeft = savedGame.TimeLeft;
                        GameModel.LifeCounter = savedGame.LifeCounter;
                        GameModel.CurrentLevel = savedGame.CurrentLevel;
                        GameModel.CurrentWord = savedGame.CurrentWord;
                        GameModel.CurrentHiddenWord = savedGame.CurrentHiddenWord;
                        GameModel.CurrentCategory = savedGame.CurrentCategory;
                        GameModel.LetterUsed = savedGame.LetterUsed;
                        LoadLettersFromSalvation();
                        LoadHangmanImage();
                        LoadHealthBarImage();
                    }
                }
            }
        }

        private ICommand m_openGame;

        public ICommand OpenGameCommand
        {
            get
            {
                if (m_openGame == null)
                    m_openGame = new RelayCommand(OpenGame);
                return m_openGame;
            }
        }

        private void SaveGame(object parameter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON Files (*.json)|*.json|All files (*.*)|*.*";

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\Resources\\SavedGames");
            saveFileDialog.InitialDirectory = Path.GetFullPath(folderPath);

            if (saveFileDialog.ShowDialog() == true)
            {
                SavedGameModel userToSave = new SavedGameModel(GameModel.Username, GameModel.ImagePath,
                    GameModel.LifeCounter, GameModel.CurrentLevel, GameModel.CurrentWord, GameModel.CurrentHiddenWord, GameModel.CurrentCategory, GameModel.LetterUsed, TimeLeft);

                string currentGame = JsonConvert.SerializeObject(userToSave);

                using (StreamWriter streamWriter = File.CreateText(saveFileDialog.FileName))
                {
                    streamWriter.Write(currentGame);
                }
            }
        }

        private ICommand m_saveGame;

        public ICommand SaveGameCommand
        {
            get
            {
                if (m_saveGame == null)
                    m_saveGame = new RelayCommand(SaveGame);
                return m_saveGame;
            }
        }

        private void ShowStatistics(object parameter)
        {
            StatisticsView statisticsView = new StatisticsView();
            statisticsView.ShowDialog();
        }

        private ICommand m_showStatisticsCommand;

        public ICommand ShowStatisticsCommand
        {
            get
            {
                if (m_showStatisticsCommand == null)
                    m_showStatisticsCommand = new RelayCommand(ShowStatistics);
                return m_showStatisticsCommand;
            }
        }
    }
}