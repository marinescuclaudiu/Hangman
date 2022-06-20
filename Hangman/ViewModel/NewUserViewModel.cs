using Hangman.Commands;
using Hangman.Model;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;


namespace Hangman.ViewModel
{
    public class NewUserViewModel : BaseViewModel
    {
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

        private void SelectImage(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\Images\\ProfileImages");
            openFileDialog.InitialDirectory = Path.GetFullPath(folderPath);

            if (openFileDialog.ShowDialog() == true)
                ImagePath = "../Images/ProfileImages/" + Path.GetFileName(openFileDialog.FileName);
        }

        private ICommand m_selectImage;

        public ICommand SelectImageCommand
        {
            get
            {
                if (m_selectImage == null)
                    m_selectImage = new RelayCommand(SelectImage);
                return m_selectImage;
            }
        }


        private void AddUser(object parameter)
        {
            StartPageViewModel startPageViewModel = (StartPageViewModel)parameter;
            UserModel user = new UserModel(Username, ImagePath);
            startPageViewModel.Users.Add(user);

            string userJSON = JsonConvert.SerializeObject(user);

            if (!File.Exists("..\\..\\..\\Resources\\Users.json"))
            {
                using (StreamWriter streamWriter = File.CreateText("..\\..\\..\\Resources\\Users.json"))
                {
                    streamWriter.WriteLine(userJSON);
                }
                return;
            }

            using (StreamWriter streamWriter = File.AppendText("..\\..\\..\\Resources\\Users.json"))
            {
                streamWriter.WriteLine(userJSON);
            }


            UserStatisticModel userStats = new UserStatisticModel(user.Username);
            string userStatsJSON = JsonConvert.SerializeObject(userStats);

            if (!File.Exists("..\\..\\..\\Resources\\Statistics.json"))
            {
                using (StreamWriter streamWriter = File.CreateText("..\\..\\..\\Resources\\Statistics.json"))
                {
                    streamWriter.WriteLine(userStatsJSON);
                }
                return;
            }
            using (StreamWriter streamWriter = File.AppendText("..\\..\\..\\Resources\\Statistics.json"))
            {
                streamWriter.WriteLine(userStatsJSON);
            }
        }

        private ICommand m_addUser;

        public ICommand AddUserCommand
        {
            get
            {
                if (m_addUser == null)
                    m_addUser = new RelayCommand(AddUser);
                return m_addUser;
            }
        }
    }
}
