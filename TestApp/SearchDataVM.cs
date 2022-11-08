using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SuggestionsSystem;

namespace TestApp
{
    public class SearchDataVM : DependencyObject, INotifyPropertyChanged
    {
        Cats cats = new Cats();
        Users users = new Users();
        //SearchSuggestions searchSuggestions = new SearchSuggestions();
        //SearchSuggestions catNameSearchSuggestions = new SearchSuggestions(this, "cat_names.txt", "CatNameTextBox", "CatNameIsFocused", CatsList, Suggestions)
        public SearchDataVM()
        {
            SearchCatByName = new RelayCommand(SearchByCatName);
            SearchCatByBreed = new RelayCommand(SearchByBreed);
            SearchUserByUsername = new RelayCommand(SearchByUsername);
            SearchUserByEmail = new RelayCommand(SearchByEmail);
            FocusSuggestionsBoxCommand = new RelayCommand(FocusSuggestionsBox);
            SelectSuggestionCommand = new RelayCommand(SelectSuggestion);
            SuggestAllCatNamesCommand = new RelayCommand(SuggestAllCatNames);
            SuggestAllBreedsCommand = new RelayCommand(SuggestAllBreeds);
            SuggestAllUsernamesCommand = new RelayCommand(SuggestAllUsernames);
            SuggestAllEmailsCommand = new RelayCommand(SuggestAllEmails);
        }

        private ObservableCollection<string> _catsList = new ObservableCollection<string>();
        public ObservableCollection<string> CatsList
        {
            get { return _catsList; }
            set { _catsList = value; PropChanged("CatsList"); }
        }

        private ObservableCollection<string> _usersList = new ObservableCollection<string>();
        public ObservableCollection<string> UsersList
        {
            get { return _usersList; }
            set { _usersList = value; PropChanged("UsersList"); }
        }

        private ObservableCollection<string> _suggestions = new ObservableCollection<string>();
        public ObservableCollection<string> Suggestions
        {
            get { return _suggestions; }
            set { _suggestions = value; PropChanged("Suggestions"); }
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
        private string _catNameTextBox;
        public string CatNameTextBox { 
            get { return _catNameTextBox; } 
            set { _catNameTextBox = value;
                SearchSuggestions.SwitchContext(this, "cat_names.txt", "CatNameTextBox", "CatNameIsFocused", CatsList, Suggestions);
                SearchSuggestions.Suggest(this);
                PropChanged("CatNameTextBox");
            } 
        }
        public RelayCommand SuggestAllCatNamesCommand { get; set; }
        private void SuggestAllCatNames()
        {
            SearchSuggestions.SwitchContext(this, "cat_names.txt", "CatNameTextBox", "CatNameIsFocused", CatsList, Suggestions);
            SearchSuggestions.Suggest(this);
        }

        private bool _catNameIsFocused;
        public bool CatNameIsFocused
        {
            get { return _catNameIsFocused; }
            set
            {
                _catNameIsFocused = value;
                if (_catNameIsFocused)
                {
                    SearchSuggestions.SwitchContext(this, "cat_names.txt", "CatNameTextBox", "CatNameIsFocused", CatsList, Suggestions);
                    SearchSuggestions.Suggest(this);
                }
                PropChanged("CatNameIsFocused");
            }
        }
        private string _catBreedTextBox;
        public string CatBreedTextBox
        {
            get
            {
                return _catBreedTextBox;
            }
            set
            {
                _catBreedTextBox = value;
                SearchSuggestions.SwitchContext(this, "cat_breeds.txt", "CatBreedTextBox", "CatBreedIsFocused", CatsList, Suggestions);
                SearchSuggestions.Suggest(this);
                PropChanged("CatBreedTextBox");
            }
        }

        public RelayCommand SuggestAllBreedsCommand { get; set; }

        private void SuggestAllBreeds()
        {
            SearchSuggestions.SwitchContext(this, "cat_breeds.txt", "CatBreedTextBox", "CatBreedIsFocused", CatsList, Suggestions);
            SearchSuggestions.Suggest(this);
        }

        private bool _catBreedIsFocused;
        public bool CatBreedIsFocused
        {
            get { return _catBreedIsFocused; }
            set
            {
                _catBreedIsFocused = value;
                if (CatBreedIsFocused)
                {
                    SearchSuggestions.SwitchContext(this, "cat_breeds.txt", "CatBreedTextBox", "CatBreedIsFocused", CatsList, Suggestions);
                    SearchSuggestions.Suggest(this);
                }
                PropChanged("CatBreedIsFocused");
            }
        }
        private string _usernameTextBox;
        public string UsernameTextBox
        {
            get
            {
                return _usernameTextBox;
            }
            set
            {
                _usernameTextBox = value;
                SearchSuggestions.SwitchContext(this, "usernames.txt", "UsernameTextBox", "UsernameIsFocused", UsersList, Suggestions);
                SearchSuggestions.Suggest(this);
                PropChanged("UsernameTextBox");
            }
        }

        public RelayCommand SuggestAllUsernamesCommand { get; set; }

        private void SuggestAllUsernames()
        {
            SearchSuggestions.SwitchContext(this, "usernames.txt", "UsernameTextBox", "UsernameIsFocused", UsersList, Suggestions);
            SearchSuggestions.Suggest(this);
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
        private string _emailTextBox;
        public string EmailTextBox
        {
            get
            {
                return _emailTextBox;
            }
            set
            {
                _emailTextBox = value;
                SearchSuggestions.SwitchContext(this, "emails.txt", "EmailTextBox", "EmailIsFocused", UsersList, Suggestions);
                SearchSuggestions.Suggest(this);
                PropChanged("EmailTextBox");
            }
        }

        public RelayCommand SuggestAllEmailsCommand { get; set; }

        private void SuggestAllEmails()
        {
            SearchSuggestions.SwitchContext(this, "emails.txt", "EmailTextBox", "EmailIsFocused", UsersList, Suggestions);
            SearchSuggestions.Suggest(this);
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
                
                //SearchSuggestions.Select(this,_selectedSuggestion);
                //FocusHelper.SetFocus(this, "UsernameIsFocused");
            }
        }
        public RelayCommand SearchCatByName { get; set; }
        public RelayCommand SearchCatByBreed { get; set; }
        public RelayCommand SearchUserByUsername { get; set; }
        public RelayCommand SearchUserByEmail { get; set; }
        public RelayCommand FocusSuggestionsBoxCommand { get; set; }
        public RelayCommand SelectSuggestionCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void PropChanged (String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void SearchByCatName()
        {
            CatsList.Clear();
            CatsList = cats.SearchByCatName(CatNameTextBox);
            SearchSuggestions.SwitchContext(this, "cat_names.txt", "CatNameTextBox", "CatNameIsFocused", CatsList, Suggestions);
            SearchSuggestions.TrySaveSuggestion(this);
        }
        private void SearchByBreed()
        {
            CatsList.Clear();
            CatsList = cats.SearchByBreed(CatBreedTextBox);
            SearchSuggestions.SwitchContext(this, "cat_breeds.txt", "CatBreedTextBox", "CatBreedIsFocused", CatsList, Suggestions);
            SearchSuggestions.TrySaveSuggestion(this);
        }
        private void SearchByUsername()
        {
            UsersList.Clear();
            UsersList = users.SearchByUsername(UsernameTextBox);
            SearchSuggestions.SwitchContext(this, "usernames.txt", "UsernameTextBox", "UsernameIsFocused", UsersList, Suggestions);
            SearchSuggestions.TrySaveSuggestion(this);
        }
        private void SearchByEmail()
        {
            UsersList.Clear();
            UsersList = users.SearchByEmail(EmailTextBox);
            SearchSuggestions.SwitchContext(this, "emails.txt", "EmailTextBox", "EmailIsFocused", UsersList, Suggestions);
            SearchSuggestions.TrySaveSuggestion(this);
        }
        private void FocusSuggestionsBox()
        {
            FocusHelper.SetFocus(this, "SuggestionsIsFocused");
        }
        private void SelectSuggestion()
        {
            SearchSuggestions.Select(this, _selectedSuggestion);
        }
        
    }
}
