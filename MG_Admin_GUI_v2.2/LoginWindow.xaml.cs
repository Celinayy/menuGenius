using MG_Admin_GUI.Models;
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
        private menugeniusContext dbContext;

        private ObservableCollection<user> users;
        private user loggedInUser;

        public LoginWindow(menugeniusContext dbContext)
        {
            InitializeComponent();
            this.dbContext = dbContext;
            this.users = users;
        }

        public user GetLoggedInUser()
        {
            return loggedInUser;
        }

        public user GetLoggedInUser(string userName)
        {
            return dbContext.users.FirstOrDefault(u => u.name == userName && u.admin);
        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string enteredPassword = pbUserPassword.Password;
            string enteredUserName = tbUserName.Text;

            user loggedInUser = GetLoggedInUser(enteredUserName);

            if (loggedInUser != null && BCrypt.Net.BCrypt.Verify(enteredPassword, loggedInUser.password))
            {
                DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Hibás név, jelszó vagy nincs admin jog!");
            }
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

                user loggedInUser = GetLoggedInUser(enteredUserName);

                if (loggedInUser != null && BCrypt.Net.BCrypt.Verify(enteredPassword, loggedInUser.password))
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
