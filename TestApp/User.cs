using MySql.Data.MySqlClient;
using SuggestionsSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestApp
{
    public class User
    {
        EnterSuggestions enterSuggestions = new EnterSuggestions("users.txt");
        static string connectionString = "SERVER=localhost;DATABASE=wpf_test_app;UID=ps;PASSWORD=ProgramniSredi!";
        MySqlConnection connection = new MySqlConnection(connectionString);
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int CatID { get; set; }
        public User()
        {
            this.Username = "";
            this.Password = "";
            this.Email = "";
            this.CatID = 0;
        }
        public User(string username, string password, string email, int catID)
        {
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.CatID = catID;
        }
        public void addToDatabase()
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO users (email, username, password, catID) VALUES (@email,@username,@password,@catID)", connection);
            cmd.Parameters.AddWithValue("email", Email);
            cmd.Parameters.AddWithValue("username", Username);
            cmd.Parameters.AddWithValue("password", Password);
            cmd.Parameters.AddWithValue("catID", CatID);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("User \"" + Username + "\" added successfully!");
        }
    }
}
