using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MG_Admin_GUI.Models
{
    public class Reservation : INotifyPropertyChanged
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


        private int _number_of_guests;
        public int number_of_guests
        {
            get { return _number_of_guests; }
            set
            {
                if (_number_of_guests != value)
                {
                    _number_of_guests = value;
                    OnPropertyChanged(nameof(number_of_guests));
                }
            }
        }

        private DateTime _checkin_date;
        public DateTime checkin_date
        {
            get { return _checkin_date; }
            set
            {
                if (_checkin_date != value)
                {
                    _checkin_date = value;
                    OnPropertyChanged(nameof(checkin_date));
                }
            }
        }

        private DateTime _checkout_date;
        public DateTime checkout_date
        {
            get { return _checkout_date; }
            set
            {
                if (_checkout_date != value)
                {
                    _checkout_date = value;
                    OnPropertyChanged(nameof(checkout_date));
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

        private string _phone;
        public string phone
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

        private Desk _reservationDesk;
        public Desk reservationDesk
        {
            get { return _reservationDesk; }
            set
            {
                if (_reservationDesk != value)
                {
                    _reservationDesk = value;
                    OnPropertyChanged(nameof(reservationDesk));
                }
            }
        }

        private bool _reservationClosed;
        public bool reservationClosed
        {
            get => _reservationClosed;
            set
            {
                if (_reservationClosed != value)
                {
                    _reservationClosed = value;
                    OnPropertyChanged(nameof(reservationClosed));
                }
            }
        }

        public Reservation(MySqlDataReader reader)
        {
            id = reader.GetInt32("Id");
            number_of_guests = reader.GetInt32("number_of_guests");
            checkin_date= reader.GetDateTime("checkin_date");
            checkout_date = reader.GetDateTime("checkout_date");
            name = reader.GetString("name");
            phone = reader.GetString("phone");
            reservationDesk = Purchase.GetDeskForPurchase(reader.GetInt32("desk_id"));
            reservationClosed = reader.GetBoolean("closed");
        }

        public static ObservableCollection<Reservation> GetReservations()
        {
            ObservableCollection<Reservation> Reservations = new ObservableCollection<Reservation>();
            using (var connection = DatabaseHandler.OpenConnection())
            {
                var query = "SELECT * FROM reservations";

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Reservations.Add(new Reservation(reader));
                        }
                    }
                }
            }
            return Reservations;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
