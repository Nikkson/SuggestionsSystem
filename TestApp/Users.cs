using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestApp
{
    public class Users
    {
        static string connectionString = "SERVER=localhost;DATABASE=wpf_test_app;UID=ps;PASSWORD=ProgramniSredi!";
        MySqlConnection connection = new MySqlConnection(connectionString);
        ObservableCollection<string> _data = new ObservableCollection<string>();
        public ObservableCollection<string> GetData()
        {
            string connectionString = "SERVER=localhost;DATABASE=wpf_test_app;UID=ps;PASSWORD=ProgramniSredi!";

            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM users", connection);

            connection.Open();
            MySqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                string newEntry = "Username: " + dataReader.GetString(2) + " Email: " + dataReader.GetString(1) + " Cat ID: " + dataReader.GetInt32(4);
                _data.Add(newEntry);
            }

            connection.Close();

            return _data;
        }

        public ObservableCollection<string> SearchByUsername(string username)
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM users WHERE username LIKE @searchPhrase", connection);
            string searchPhrase = "%" + username + "%";
            cmd.Parameters.AddWithValue("searchPhrase", searchPhrase);

            connection.Open();
            MySqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                string newEntry = "Username: " + dataReader.GetString(2) + " Email: " + dataReader.GetString(1) + " Cat ID: " + dataReader.GetInt32(4);
                _data.Add(newEntry);
            }

            connection.Close();

            return _data;
        }

        public ObservableCollection<string> SearchByEmail(string email)
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM users WHERE email LIKE @searchPhrase", connection);
            string searchPhrase = "%" + email + "%";
            cmd.Parameters.AddWithValue("searchPhrase", searchPhrase);

            connection.Open();
            MySqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                string newEntry = "Username: " + dataReader.GetString(2) + " Email: " + dataReader.GetString(1) + " Cat ID: " + dataReader.GetInt32(4);
                _data.Add(newEntry);
            }

            connection.Close();

            return _data;
        }
    }
}
