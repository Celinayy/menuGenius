using MG_Admin_GUI.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MG_Admin_GUI
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private MenugeniusContext dbContext;
        
        private ObservableCollection<User> users;
        private User loggedInUser;

        public LoginWindow(MenugeniusContext dbContext)
        {
            InitializeComponent();
            this.dbContext = dbContext;
            this.users = users;
        }

        public User GetLoggedInUser()
        {
            return loggedInUser;
        }

        public User GetLoggedInUser(string userName)
        {
            return dbContext.Users.FirstOrDefault(u => u.Name == userName && u.Admin);
        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string enteredPassword = pbUserPassword.Password;
            string enteredUserName = tbUserName.Text;

            User loggedInUser = GetLoggedInUser(enteredUserName);

            if (loggedInUser != null && BCrypt.Net.BCrypt.Verify(enteredPassword, loggedInUser.Password))
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

        private void loginEnter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string enteredPassword = pbUserPassword.Password;
                string enteredUserName = tbUserName.Text;

                User loggedInUser = GetLoggedInUser(enteredUserName);

                if (loggedInUser != null && BCrypt.Net.BCrypt.Verify(enteredPassword, loggedInUser.Password))
                {
                    DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Hibás név, jelszó vagy nincs admin jog!");
                }
            }
        }
    }
}
