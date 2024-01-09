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
    public class AllergenViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Allergen> _Allergens;
        public ObservableCollection<Allergen> Allergens
        {
            get { return _Allergens; }
            set
            {
                if (_Allergens != value)
                {
                    _Allergens = value;
                    OnPropertyChanged(nameof(Allergens));
                }
            }
        }

        private Allergen _selectedAllergen;
        public Allergen selectedAllergen
        {
            get { return _selectedAllergen; }
            set
            {
                if (_selectedAllergen != value)
                {
                    _selectedAllergen = value;
                    OnPropertyChanged(nameof(selectedAllergen));
                }
            }
        }

        public AllergenViewModel()
        {
            Allergens = Allergen.GetAllergens();
            selectedAllergen = null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
