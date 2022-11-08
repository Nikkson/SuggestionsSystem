using GalaSoft.MvvmLight.Command;
using SuggestionsSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestApp
{
    public class EnterDataVM : DependencyObject, INotifyPropertyChanged
    {
        private User _currentUser;
        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                PropChanged("UsernameTextBox");
                PropChanged("PasswordTextBox");
                PropChanged("EmailTextBox");
                PropChanged("CatIDTextBox");
            }
        }
        EnterSuggestions enterSuggestions = new EnterSuggestions("users.txt");
        string[] propertiesToHide = { "Password", "CatID" };

        public EnterDataVM()
        {
            CurrentUser = new User();
            AddUserCommand = new RelayCommand(AddUser);
            CheckUserCommand = new RelayCommand(printUser);
            FocusSuggestionsBoxCommand = new RelayCommand(FocusSuggestionsBox);
            SelectSuggestionCommand = new RelayCommand(SelectSuggestion);
            SuggestAllUsersCommand = new RelayCommand(SuggestAllUsers);
            enterSuggestions.SetSuggestionsProperty(Suggestions);
        }
        private string _usernameTextBox;
        public string UsernameTextBox
        {
            get
            {
                return CurrentUser.Username;
            }
            set
            {
                CurrentUser.Username = value;
                enterSuggestions.SwitchContext(this, CurrentUser, "Username", "UsernameIsFocused", Suggestions);
                enterSuggestions.Suggest(CurrentUser,propertiesToHide);
                PropChanged("UsernameTextBox");
            }
        }
        private string _passwordTextBox;
        public string PasswordTextBox
        {
            get
            {
                return CurrentUser.Password;
            }
            set
            {
                CurrentUser.Password = value;
                enterSuggestions.SwitchContext(this, CurrentUser, "Password", "PasswordIsFocused", Suggestions);
                enterSuggestions.Suggest(CurrentUser,propertiesToHide);
                PropChanged("PasswordTextBox");
            }
        }
        private string _emailTextBox;
        public string EmailTextBox
        {
            get
            {
                return CurrentUser.Email;
            }
            set
            {
                CurrentUser.Email = value;
                enterSuggestions.SwitchContext(this, CurrentUser, "Email", "EmailIsFocused", Suggestions);
                enterSuggestions.Suggest(CurrentUser, propertiesToHide);
                PropChanged("EmailTextBox");
            }
        }
        private string _catIDTextBox;
        public string CatIDTextBox
        {
            get
            {
                return CurrentUser.CatID.ToString();
            }
            set
            {
                CurrentUser.CatID = Int32.Parse(value);
                enterSuggestions.SwitchContext(this, CurrentUser, "CatID", "CatIDIsFocused", Suggestions);
                enterSuggestions.Suggest(CurrentUser, propertiesToHide);
                PropChanged("CatIDTextBox");
            }
        }

        public RelayCommand SuggestAllUsersCommand { get; set; }

        private void SuggestAllUsers()
        {
            enterSuggestions.SuggestAll(propertiesToHide);
        }

        private ObservableCollection<string> _suggestions = new ObservableCollection<string>();
        public ObservableCollection<string> Suggestions
        {
            get { return _suggestions; }
            set { _suggestions = value; PropChanged("Suggestions"); }
        }

        private string _selectedSuggestion;
        public string SelectedSuggestion
        {
            get
            {
                return _selectedSuggestion;
            }
            set
            {
                _selectedSuggestion = value;
                //enterSuggestions.Select(CurrentUser, _selectedSuggestion);
                //PropChanged("UsernameTextBox");
                //PropChanged("PasswordTextBox");
                //PropChanged("EmailTextBox");
                //PropChanged("CatIDTextBox");
            }
        }
        private bool _usernameIsFocused;
        public bool UsernameIsFocused
        {
            get { return _usernameIsFocused; }
            set
            {
                _usernameIsFocused = value;
                PropChanged("UsernameIsFocused");
            }
        }
        private bool _emailIsFocused;
        public bool EmailIsFocused
        {
            get { return _emailIsFocused; }
            set
            {
                _emailIsFocused = value;
                PropChanged("EmailIsFocused");
            }
        }
        private bool _passwordIsFocused;
        public bool PasswordIsFocused
        {
            get { return _passwordIsFocused; }
            set
            {
                _passwordIsFocused = value;
                PropChanged("PasswordIsFocused");
            }
        }
        private bool _catIDIsFocused;
        public bool CatIDIsFocused
        {
            get { return _catIDIsFocused; }
            set
            {
                _catIDIsFocused = value;
                PropChanged("CatIDIsFocused");
            }
        }
        private bool _suggestionsIsFocused;
        public bool SuggestionsIsFocused
        {
            get { return _suggestionsIsFocused; }
            set
            {
                _suggestionsIsFocused = value;
                PropChanged("SuggestionsIsFocused");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void PropChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public RelayCommand AddUserCommand { get; set; }
        public bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        private void AddUser ()
        {
            if (IsValid(EmailTextBox))
            {
                CurrentUser = new User(UsernameTextBox, PasswordTextBox, EmailTextBox, Int32.Parse(CatIDTextBox));
                //user.addToDatabase();
                
            } else
            {
                MessageBox.Show("Invalid email address!");
            }
            enterSuggestions.TrySaveSuggestion(CurrentUser, IsValid, "Email");
        }
        public RelayCommand CheckUserCommand { get; set; }
        private void printUser()
        {
            MessageBox.Show("Username focused:" + UsernameIsFocused);
            UsernameIsFocused = true;
            //MessageBox.Show(CurrentUser.Username + " " + CurrentUser.Password + " " + CurrentUser.Email + " " + CurrentUser.CatID);
        }
        public RelayCommand FocusSuggestionsBoxCommand { get; set; }
        private void FocusSuggestionsBox()
        {
            FocusHelper.SetFocus(this, "SuggestionsIsFocused");
        }
        private void SelectSuggestion()
        {
            enterSuggestions.Select(this, CurrentUser, _selectedSuggestion);
            PropChanged("UsernameTextBox");
            PropChanged("PasswordTextBox");
            PropChanged("EmailTextBox");
            PropChanged("CatIDTextBox");
        }
        public RelayCommand SelectSuggestionCommand { get; set; }
    }
}
