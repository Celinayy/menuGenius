using MG_Admin_GUI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Linq;

using System.Windows;


namespace MG_Admin_GUI
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private ObservableCollection<User> users;
        private User loggedInUser;

        public LoginWindow(ObservableCollection<User> users)
        {
            InitializeComponent();
            this.users = users;
        }

        public User GetLoggedInUser()
        {
            return loggedInUser;
        }

        public User GetLoggedInUser(int userId)
        {
            User user = null;
            using (var connection = DatabaseHandler.OpenConnection())
            {
                var query = "SELECT * FROM users WHERE id = @userId";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = new User(reader);
                        }
                    }
                }
            }
            return user;
        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string enteredPassword = pbUserPassword.Password;
            string enteredUserName = tbUserName.Text;
            User loggedInUser = users.FirstOrDefault(u => u.name == enteredUserName && u.admin);

            string storedHashedPasswordFromDatabase = loggedInUser.password;

            if (BCrypt.Net.BCrypt.Verify(enteredPassword, storedHashedPasswordFromDatabase))
            {
                //MessageBox.Show($"Sikeres bejelentkezés! Üdv, {loggedInUser.name}!");
                DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Hibás név, jelszó vagy nincs admin jog!");
            }
            //loggedInUser = users.FirstOrDefault(u => u.name == tbUserName.Text && u.password == pbUserPassword.Password && u.admin);

            //if (loggedInUser != null)
            //{
            //    MessageBox.Show($"Sikeres bejelentkezés! Üdv, {loggedInUser.name}!");
            //    DialogResult = true;
            //    this.Close();
            //}
            //else
            //{
            //    MessageBox.Show("Hibás név, jelszó vagy nincs admin jog!");
            //}
        }

        private void btnLoginClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}