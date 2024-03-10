using MG_Admin_GUI.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

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
