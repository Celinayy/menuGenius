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
    public class IngredientViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Ingredient> _Ingredients;
        public ObservableCollection<Ingredient> Ingredients
        {
            get { return _Ingredients; }
            set
            {
                if (_Ingredients != value)
                {
                    _Ingredients = value;
                    OnPropertyChanged(nameof(Ingredients));
                }
            }
        }

        private Ingredient _selectedIngredient;
        public Ingredient selectedIngredient
        {
            get { return _selectedIngredient; }
            set
            {
                if (_selectedIngredient != value)
                {
                    _selectedIngredient = value;
                    OnPropertyChanged(nameof(selectedIngredient));
                }
            }
        }

        public IngredientViewModel()
        {
            Ingredients = Ingredient.GetIngredients();
            selectedIngredient = null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
