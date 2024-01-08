using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MG_Admin_GUI.Models
{
    public class Allergen : INotifyPropertyChanged
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

        private decimal _code;
        public decimal code
        {
            get { return _code; }
            set
            {
                if (_code != value)
                {
                    _code = value;
                    OnPropertyChanged(nameof(code));
                }
            }
        }

        private string _name;
        public string name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged(nameof(name));
                }
            }
        }

        public Allergen(MySqlDataReader reader)
        {
            id = reader.GetInt32("id");
            code = reader.GetDecimal("code");
            name = reader.GetString("name");
        }

        public static ObservableCollection<Allergen> GetAllergens()
        {
            ObservableCollection<Allergen> Allergens = new ObservableCollection<Allergen>();
            using (var connection = DatabaseHandler.OpenConnection())
            {
                var query = "SELECT * FROM allergens";

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Allergens.Add(new Allergen(reader));
                        }
                    }
                }
            }
            return Allergens;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
