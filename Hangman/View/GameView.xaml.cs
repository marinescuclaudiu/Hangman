using Hangman.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Hangman.View
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        public GameView(UserModel userModel)
        {
            InitializeComponent();
            Username.Text = userModel.Username;
            ProfileImage.Source = new BitmapImage(new Uri(userModel.ImagePath, UriKind.Relative));
            TimeLeft.Text = "30";
            TimerOn.Text = "True";
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Ticker;
            timer.Start();
        }

        private void Ticker(object sender, EventArgs e)
        {
            if (TimerOn.Text == "False")
            {
                timer.Stop();
                return;
            }

            int timeLeftInt = Int32.Parse(TimeLeft.Text);
            if (timeLeftInt > 0)
            {
                timeLeftInt--;
                TimeLeft.Text = timeLeftInt.ToString();
            }
            else
            {
                timer.Stop();
                TimerOn.Text = "False";
            }
        }

        private void ChangeCategory_Click(object sender, RoutedEventArgs e)
        {
            MenuItem itemChecked = (MenuItem)sender;
            MenuItem itemParent = (MenuItem)itemChecked.Parent;

            foreach (MenuItem item in itemParent.Items)
            {
                if (item == itemChecked) continue;

                item.IsChecked = false;
            }

            TimeLeft.Text = "30";
            if (TimerOn.Text == "False")
            {
                TimerOn.Text = "True";
                timer.Start();
            }

        }

        private void EnableTimerNewGame_Click(object sender, RoutedEventArgs e)
        {
            TimeLeft.Text = "30";
            if (TimerOn.Text == "False")
            {
                TimerOn.Text = "True";
                timer.Start();
            }
        }

        private void EnableTimerOpenGame_Click(object sender, RoutedEventArgs e)
        {
            if (TimerOn.Text == "False")
            {
                TimerOn.Text = "True";
                timer.Start();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            TimerOn.Text = "False";
            timer.Stop();
            Close();
        }
    }
}
