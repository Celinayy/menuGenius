using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MG_Admin_GUI.Models
{
    public class Desk : INotifyPropertyChanged
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

        private int _number_of_seats;
        public int number_of_seats
        {
            get { return _number_of_seats; }
            set
            {
                if (_number_of_seats != value)
                {
                    _number_of_seats = value;
                    OnPropertyChanged(nameof(number_of_seats));
                }
            }
        }

        public Desk(MySqlDataReader reader)
        {
            id = reader.GetInt32("Id");
            number_of_seats = reader.GetInt32(number_of_seats);
        }

        public static ObservableCollection<Desk> GetDesks()
        {
            ObservableCollection<Desk> Desks = new ObservableCollection<Desk>();
            using (var connection = DatabaseHandler.OpenConnection())
            {
                var query = "SELECT * FROM desks";

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Desks.Add(new Desk(reader));
                        }
                    }
                }
            }
            return Desks;
        }


        public override string ToString()
        {
            return $"Asztalszám: {id}, Ülőhely: {number_of_seats}";
        }

        public override bool Equals(object? obj)
        {
            return obj is Desk desk && id == desk.id;
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
