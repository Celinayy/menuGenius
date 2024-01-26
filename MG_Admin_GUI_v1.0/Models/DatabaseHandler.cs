using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Windows;

namespace MG_Admin_GUI.Models
{
    public class DatabaseHandler
    {
        public static MySqlConnection OpenConnection()
        {
            MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nem sikerült kapcsolódni az adatbázishoz!" + ex.Message, "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return connection;
        }
    }
}
