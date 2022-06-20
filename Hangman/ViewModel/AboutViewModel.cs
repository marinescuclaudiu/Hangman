using Hangman.Commands;
using Hangman.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Hangman.ViewModel
{
    public class AboutViewModel : BaseViewModel
    {
        private void ContactDeveloper(object parameter)
        {
            string destinationurl = "https://student.unitbv.ro/";
            System.Diagnostics.ProcessStartInfo sInfo = new System.Diagnostics.ProcessStartInfo(destinationurl)
            {
                UseShellExecute = true,
            };
            System.Diagnostics.Process.Start(sInfo);
        }

        private void About(object parameter)
        {
            AboutView aboutView = new AboutView();
            aboutView.ShowDialog();
        }

        private ICommand m_about;
        public ICommand ShowAboutWindow
        {
            get
            {
                if (m_about == null)
                    m_about = new RelayCommand(About);
                return m_about;
            }
        }


        private ICommand m_contact;
        public ICommand Contact
        {
            get
            {
                if (m_contact == null)
                    m_contact = new RelayCommand(ContactDeveloper);
                return m_contact;
            }
        }
    }
}

