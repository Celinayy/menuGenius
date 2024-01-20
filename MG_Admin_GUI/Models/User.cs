using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MySql.Data.MySqlClient;

namespace MG_Admin_GUI.Models
{
    public class User : INotifyPropertyChanged
    {
        private int _id;
        public int id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(id));
                }
            }
        }

        private string _name;
        public string name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(name));
                }
            }
        }

        private string _email;
        public string email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(email));
                }
            }
        }

        private string _password;
        public string password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(password));
                }
            }
        }

        private string? _phone;
        public string? phone
        {
            get { return _phone; }
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                    OnPropertyChanged(nameof(phone));
                }
            }
        }

        private bool _admin;
        public bool admin
        {
            get { return _admin; }
            set
            {
                if (_admin != value)
                {
                    _admin = value;
                    OnPropertyChanged(nameof(admin));
                }
            }
        }

        public User(MySqlDataReader reader)
        {
            id = reader.GetInt32("id");
            name = reader.GetString("name");
            email = reader.GetString("email");
            password = reader.GetString("password");
            //phone = reader.GetString("phone");
            if (!reader.IsDBNull(reader.GetOrdinal("phone")))
            {
                phone = reader.GetString("phone");
            }
            admin = reader.GetBoolean("admin");
        }

        public User(int id)
        {

        }

        public static ObservableCollection<User> GetUsers()
        {
            ObservableCollection<User> Users = new ObservableCollection<User>();
            using (var connection = DatabaseHandler.OpenConnection())
            {
                var query = "SELECT * FROM users";

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Users.Add(new User(reader));
                        }
                    }
                }
            }
            return Users;
        }

        public override string ToString()
        {
            return $"{id} - {name} - {phone}";
        }

        public override bool Equals(object? obj)
        {
            return obj is User user && id == user.id;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
