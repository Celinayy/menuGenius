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
    public class CategoryViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Category> _Categories;
        public ObservableCollection<Category> Categories
        {
            get { return _Categories; }
            set
            {
                if (_Categories != value)
                {
                    _Categories = value;
                    OnPropertyChanged(nameof(Categories));
                }
            }
        }

        private Category _selectedCategory;
        public Category selectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    OnPropertyChanged(nameof(selectedCategory));
                }
            }
        }

        public CategoryViewModel()
        {
            Categories = Category.GetCategories();
            selectedCategory = null;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
