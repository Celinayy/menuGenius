using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MG_Admin_GUI.Models
{
    public class Category : INotifyPropertyChanged
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

        public Category(MySqlDataReader reader)
        {
            id = reader.GetInt32("id");
            name = reader.GetString("name");
        }

        public static ObservableCollection<Category> GetCategories()
        {
            ObservableCollection<Category> Categories = new ObservableCollection<Category>();
            using (var connection = DatabaseHandler.OpenConnection())
            {
                var query = "SELECT * FROM categories";

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Categories.Add(new Category(reader));
                        }
                    }
                }
            }
            return Categories;
        }

        public override string ToString()
        {
            return name; // vagy az a tulajdonság, amit meg szeretnél jeleníteni
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
