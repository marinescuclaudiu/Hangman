using Hangman.Commands;
using Hangman.Model;
using Hangman.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Input;

namespace Hangman.ViewModel
{
    public class StartPageViewModel : BaseViewModel
    {
        public StartPageViewModel()
        {
            Users = new ObservableCollection<UserModel>();
            LoadUsers();
        }

        private bool _isPlayButtonActive = false;
        public bool IsPlayButtonActive
        {
            get { return _isPlayButtonActive; }
            set { _isPlayButtonActive = value; OnPropertyChanged("IsPlayButtonActive"); }
        }

        private bool _isDeleteUserButtonActive = false;
        public bool IsDeleteUserButtonActive
        {
            get { return _isDeleteUserButtonActive; }
            set { _isDeleteUserButtonActive = value; OnPropertyChanged("IsDeleteUserButtonActive"); }
        }

        private UserModel _currentUser;
        public UserModel CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;

                if(value == null)
                {
                    IsPlayButtonActive = false;
                    IsDeleteUserButtonActive = false;
                    CurrentProfileImage = null;
                }
                else
                {
                    CurrentProfileImage = _currentUser.ImagePath;
                    IsPlayButtonActive = true;
                    IsDeleteUserButtonActive = true;
                }
                OnPropertyChanged("CurrentUser");
            }
        }

        private string _currentProfileImage;
        public string CurrentProfileImage
        {
            get { return _currentProfileImage; }
            set { _currentProfileImage = value; OnPropertyChanged("CurrentProfileImage"); }
        }

        public ObservableCollection<UserModel> Users { get; set; }

        private void LoadUsers()
        {
            if (!File.Exists("..\\..\\..\\Resources\\Users.json"))
            {
                File.Create("..\\..\\..\\Resources\\Users.json");
                return;
            }

            using (StreamReader streamReader = File.OpenText("..\\..\\..\\Resources\\Users.json"))
            {
                string line = null;
                while ((line = streamReader.ReadLine()) != null)
                {
                    UserModel currentUser = JsonConvert.DeserializeObject<UserModel>(line);
                    Users.Add(currentUser);
                }
            }
        }

        private void NewUser(object parameter)
        {
            NewUserView newUserView = new NewUserView(this);
            newUserView.ShowDialog();
        }

        private ICommand m_newUser;

        public ICommand NewUserCommand
        {
            get
            {
                if (m_newUser == null)
                    m_newUser = new RelayCommand(NewUser);
                return m_newUser;
            }
        }

        private void DeleteUser(object parameter)
        {
            List<string> userJSON = new List<string>();

            using (StreamReader streamReader = File.OpenText("..\\..\\..\\Resources\\Users.json"))
            {
                string line = null;
                while ((line = streamReader.ReadLine()) != null)
                {
                    userJSON.Add(line);
                }
            }

            using (StreamWriter streamWriter = File.CreateText("..\\..\\..\\Resources\\Users.json"))
            {
                for (int indexUserJSON = 0; indexUserJSON < userJSON.Count; indexUserJSON++)
                {
                    if (!userJSON[indexUserJSON].Contains(CurrentUser.Username))
                        streamWriter.WriteLine(userJSON[indexUserJSON]);
                }
            }

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
                    if (!userStatisticsJSON[indexUserJSON].Contains(CurrentUser.Username))
                        streamWriter.WriteLine(userStatisticsJSON[indexUserJSON]);
                }
            }

            foreach (string file in Directory.EnumerateFiles("..\\..\\..\\Resources\\SavedGames", "*.json"))
            {
                string content = File.ReadAllText(file);
                if (content.Contains(CurrentUser.Username))
                    File.Delete(file);
            }

            UserModel userToDelete = CurrentUser;
            CurrentUser = null;
            Users.Remove(userToDelete);
        }

        private ICommand m_deleteUser;

        public ICommand DeleteUserCommand
        {
            get
            {
                if (m_deleteUser == null)
                    m_deleteUser = new RelayCommand(DeleteUser);
                return m_deleteUser;
            }
        }


        private void Play(object parameter)
        {
            GameView gameView = new GameView(CurrentUser);
            gameView.ShowDialog();
        }

        private ICommand m_play;

        public ICommand PlayCommand
        {
            get
            {
                if (m_play == null)
                    m_play = new RelayCommand(Play);
                return m_play;
            }
        }
        

        private void Cancel(object parameter)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private ICommand m_cancel;

        public ICommand CancelCommand
        {
            get
            {
                if (m_cancel == null)
                    m_cancel = new RelayCommand(Cancel);
                return m_cancel;
            }
        }

    }
}
