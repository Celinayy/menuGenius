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
    public class ReservationViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Reservation> _Reservations;
        public ObservableCollection<Reservation> Reservations
        {
            get { return _Reservations; }
            set
            {
                if (_Reservations != value)
                {
                    _Reservations = value;
                    OnPropertyChanged(nameof(Reservations));
                }
            }
        }

        private Reservation _selectedReservation;
        public Reservation selectedReservation
        {
            get { return _selectedReservation; }
            set
            {
                if (_selectedReservation != value)
                {
                    _selectedReservation = value;
                    OnPropertyChanged(nameof(selectedReservation));
                }
            }
        }

        public ReservationViewModel()
        {
            Reservations = Reservation.GetReservations();
            selectedReservation = null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
