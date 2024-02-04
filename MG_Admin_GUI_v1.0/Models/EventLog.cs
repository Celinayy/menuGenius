using MG_Admin_GUI.ViewModels;
using MySql.Data.MySqlClient;
using System;
using System.Linq;
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

        private string _route;
        public string route
        {
            get { return _route; }
            set
            {
                if (value != _route)
                {
                    _route = value;
                    OnPropertyChanged(nameof(route));
                }
            }
        }

        private string? _body;
        public string? body
        {
            get { return _body; }
            set
            {
                if (value != _body)
                {
                    _body = value;
                    OnPropertyChanged(nameof(body));
                }
            }
        }

        private DateTime _date_time;
        public DateTime date_time
        {
            get { return _date_time; }
            set
            {
                if (value != _date_time)
                {
                    _date_time = value;
                    OnPropertyChanged(nameof(date_time));
                }
            }
        }

        private User? _eventlog_user;
        public User? eventlog_user
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

        private DateTime? _deleted_at;
        public DateTime? deleted_at
        {
            get { return _deleted_at; }
            set
            {
                if (value != _deleted_at)
                {
                    _deleted_at = value;
                    OnPropertyChanged(nameof(deleted_at));
                }
            }
        }

        public EventLog(MySqlDataReader reader)
        {
            id = reader.GetInt32("id");
            event_type = reader.GetString("event_type");
            //eventlog_user = GetUserForEventlog(reader.GetInt32("user_id"));

            if (!reader.IsDBNull(reader.GetOrdinal("user_id")))
            {
                eventlog_user = GetUserForEventlog(reader.GetInt32("user_id"));
            }
            else
            {
                eventlog_user = null;
            }


            route = reader.GetString("route");
            //body = reader.GetInt32("body");

            if (!reader.IsDBNull(reader.GetOrdinal("body")))
            {
                body = reader.GetString("body");
            }
            else
            {
                body = null;
            }



            date_time = reader.GetDateTime("date_time");

            if (!reader.IsDBNull(reader.GetOrdinal("deleted_at")))
            {
                deleted_at = reader.GetDateTime("deleted_at");
            }
            else
            {
                deleted_at = null;
            }

        }

        public UserViewModel UserVM => new UserViewModel();


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

        public User GetUserForEventlog(int userId)
        {
            return UserVM.Users.FirstOrDefault(user => user.id == userId);
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
