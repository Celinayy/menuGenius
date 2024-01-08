using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace MG_Admin_GUI.Models
{
    public class EventLog : INotifyPropertyChanged
    {
        private int _id;
        public int id
        {
            get { return _id; }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged(nameof(id));
                }
            }
        }

        private string _event_type;
        public string event_type
        {
            get { return _event_type; }
            set
            {
                if (value != _event_type)
                {
                    _event_type = value;
                    OnPropertyChanged(nameof(event_type));
                }
            }
        }

        private string _affected_table;
        public string affected_table
        {
            get { return _affected_table; }
            set
            {
                if (value != _affected_table)
                {
                    _affected_table = value;
                    OnPropertyChanged(nameof(affected_table));
                }
            }
        }

        private int _affected_id;
        public int affected_id
        {
            get { return _affected_id; }
            set
            {
                if (value != _affected_id)
                {
                    _affected_id = value;
                    OnPropertyChanged(nameof(affected_id));
                }
            }
        }

        private string _event_description;
        public string event_description
        {
            get { return _event_description; }
            set
            {
                if (value != _event_description)
                {
                    _event_description = value;
                    OnPropertyChanged(nameof(event_description));
                }
            }
        }

        private DateTime _date;
        public DateTime date
        {
            get { return _date; }
            set
            {
                if (value != _date)
                {
                    _date = value;
                    OnPropertyChanged(nameof(date));
                }
            }
        }

        private User _eventlog_user;
        public User eventlog_user
        {
            get { return _eventlog_user; }
            set
            {
                if (value != _eventlog_user)
                {
                    _eventlog_user = value;
                    OnPropertyChanged(nameof(eventlog_user));
                }
            }
        }

        public EventLog(MySqlDataReader reader)
        {
            id = reader.GetInt32("id");
            event_type = reader.GetString("event_type");
            affected_table = reader.GetString("affected_table");
            affected_id = reader.GetInt32("affected_id");
            event_description = reader.GetString("event");
            date = reader.GetDateTime("date");
            eventlog_user = reader.GetInt32("user_id");
        }

        public static ObservableCollection<EventLog> GetEventLogs()
        {
            ObservableCollection<EventLog> EventLogs = new ObservableCollection<EventLog>();
            using (var connection = DatabaseHandler.OpenConnection())
            {
                var query = "SELECT * FROM event_logs";

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EventLogs.Add(new EventLog(reader));
                        }
                    }
                }
            }
            return EventLogs;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
