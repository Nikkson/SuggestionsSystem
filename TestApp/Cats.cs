﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestApp
{
    public class Cats
    {
        ObservableCollection<string> _data = new ObservableCollection<string>();
        public ObservableCollection<string> GetData()
        {
            string connectionString = "SERVER=localhost;DATABASE=wpf_test_app;UID=ps;PASSWORD=ProgramniSredi!";

            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM cats", connection);

            connection.Open();
            MySqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                string newEntry = "Name: " + dataReader.GetString(1) + "; Breed: " + dataReader.GetString(2);
                _data.Add(newEntry);
            }

            connection.Close();

            return _data;
        }
        public ObservableCollection<string> SearchByCatName(string catName)
        {
            string connectionString = "SERVER=localhost;DATABASE=wpf_test_app;UID=ps;PASSWORD=ProgramniSredi!";

            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM cats WHERE name LIKE @searchPhrase", connection);
            string searchPhrase = "%" + catName + "%";
            cmd.Parameters.AddWithValue("searchPhrase", searchPhrase);

            connection.Open();
            MySqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                string newEntry = "Name: " + dataReader.GetString(1) + "; Breed: " + dataReader.GetString(2);
                _data.Add(newEntry);
            }

            connection.Close();

            return _data;
        }
        public ObservableCollection<string> SearchByBreed(string breed)
        {
            string connectionString = "SERVER=localhost;DATABASE=wpf_test_app;UID=ps;PASSWORD=ProgramniSredi!";

            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM cats WHERE breed LIKE @searchPhrase", connection);
            string searchPhrase = "%" + breed + "%";
            cmd.Parameters.AddWithValue("searchPhrase", searchPhrase);

            connection.Open();
            MySqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                string newEntry = "Name: " + dataReader.GetString(1) + "; Breed: " + dataReader.GetString(2);
                _data.Add(newEntry);
            }

            connection.Close();

            return _data;
        }
    }
}
