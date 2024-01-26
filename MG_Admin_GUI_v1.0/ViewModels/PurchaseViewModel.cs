using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using MG_Admin_GUI.Models;

namespace MG_Admin_GUI.ViewModels
{
    public class PurchaseViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Purchase> _Purchases;
        public ObservableCollection<Purchase> Purchases
        {
            get { return _Purchases; }
            set
            {
                if (_Purchases != value)
                {
                    _Purchases = new ObservableCollection<Purchase>(value.OrderBy(purchase => purchase.date_time));
                    OnPropertyChanged(nameof(Purchases));
                }
            }
        }



        public PurchaseViewModel()
        {
            Purchases = Purchase.GetPurchases();
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
