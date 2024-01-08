using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MG_Admin_GUI.Models;

namespace MG_Admin_GUI.ViewModels
{
    public class EventLogViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<EventLog> _EventLog;
        public ObservableCollection<EventLog> EventLogs
        {
            get { return _EventLog; }
            set
            {
                if (_EventLog != value)
                {
                    _EventLog = value;
                    OnPropertyChanged(nameof(EventLogs));
                }
            }
        }

        private EventLog _selectedEvent;
        public EventLog selectedEvent
        {
            get { return _selectedEvent; }
            set
            {
                if (value != _selectedEvent)
                {
                    _selectedEvent = value;
                    OnPropertyChanged(nameof(selectedEvent));
                }
            }
        }

        public EventLogViewModel()
        {
            EventLogs = EventLog.GetEventLogs();
            selectedEvent = null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
