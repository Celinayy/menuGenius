using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using MG_Admin_GUI.Models;

namespace MG_Admin_GUI.ViewModels
{
    public class DeskViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Desk> _Desks;
        public ObservableCollection<Desk> Desks
        {
            get { return _Desks; }
            set
            {
                if (_Desks != value)
                {
                    _Desks = value;
                    OnPropertyChanged(nameof(Desks));
                }
            }
        }

        private Desk _selectedDesk;
        public Desk selectedDesk
        {
            get { return _selectedDesk; }
            set
            {
                if (_selectedDesk != value)
                {
                    _selectedDesk = value;
                    OnPropertyChanged(nameof(selectedDesk));
                }
            }
        }

        public DeskViewModel()
        {
            Desks = Desk.GetDesks();
            selectedDesk = null;
        }

        public Desk GetDeskById(int deskId)
        {
            return Desks.FirstOrDefault(desk => desk.id == deskId);
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
