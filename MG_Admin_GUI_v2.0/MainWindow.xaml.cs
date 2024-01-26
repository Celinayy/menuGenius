using MG_Admin_GUI.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MG_Admin_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MenugeniusContext dbContext = new MenugeniusContext();
        Reservation reservation = new Reservation();
        Product product = new Product();
        public MainWindow()
        {
            InitializeComponent();
            LoadDatas();

        }

        public void LoadDatas()
        {
            dbContext.Allergens.Load();
            dbContext.Categories.Load();
            dbContext.Desks.Load();
            dbContext.EventLogs.Load();
            dbContext.Images.Load();
            dbContext.Ingredients.Load();
            dbContext.Products.Load();
            dbContext.Purchases.Load();
            dbContext.Reservations.Load();
            dbContext.Users.Load();

            dgOrdered.ItemsSource = dbContext.Purchases.Where(p => p.Status == "ordered").OrderBy(p => p.DateTime).ToList();
            dgCooked.ItemsSource = dbContext.Purchases.Where(p => p.Status == "cooked").OrderBy(p => p.DateTime).ToList();
            dgServed.ItemsSource = dbContext.Purchases.Where(p => p.Status == "served" && p.Paid == false).OrderBy(p => p.DateTime).ToList();
            dgPurchases.ItemsSource = dbContext.Purchases.Local.ToObservableCollection().Where(p => p.Status == "served" && p.Paid == true).OrderBy(p => p.DateTime).ToList();
            dgReservations.ItemsSource = dbContext.Reservations.Local.ToObservableCollection();
            dgProducts.ItemsSource = dbContext.Products.Local.ToObservableCollection();
            spCategory.DataContext = dbContext.Categories.Local.ToObservableCollection();
            spIngredient.DataContext = dbContext.Ingredients.Local.ToObservableCollection();
            spAllergen.DataContext = dbContext.Allergens.Local.ToObservableCollection();
            spDesk.DataContext = dbContext.Desks.Local.ToObservableCollection();
            dgUsers.ItemsSource = dbContext.Users.Local.ToObservableCollection();
            dgEventLogs.ItemsSource = dbContext.EventLogs.Local.ToObservableCollection();
        }

        private void dgReservations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgReservations.SelectedItem == null) { return; }
            reservation = (Reservation)dgReservations.SelectedItem;
            tabReservation.DataContext = reservation;
        }
    }
}