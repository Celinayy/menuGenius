using MG_Admin_GUI.Models;
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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            loggedInUser = users.FirstOrDefault(u => u.name == tbUserName.Text && u.password == pbUserPassword.Password && u.admin);

            if (loggedInUser != null)
            {
                MessageBox.Show($"Sikeres bejelentkezés! Üdv, {loggedInUser.name}!");
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
    }
}