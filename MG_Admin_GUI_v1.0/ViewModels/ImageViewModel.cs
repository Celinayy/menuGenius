using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG_Admin_GUI
{
    public class ImageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Image> _Images;
        public ObservableCollection<Image> Images
        {
            get { return _Images; }
            set
            {
                if (_Images != value)
                {
                    _Images = value;
                    OnPropertyChanged(nameof(Images));
                }
            }
        }

        private Image _selectedImage;
        public Image selectedImage
        {
            get { return _selectedImage; }
            set
            {
                if (_selectedImage != value)
                {
                    _selectedImage = value;
                    OnPropertyChanged(nameof(selectedImage));
                }
            }
        }

        public ImageViewModel()
        {
            Images = Image.GetImages();
            selectedImage = null;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
