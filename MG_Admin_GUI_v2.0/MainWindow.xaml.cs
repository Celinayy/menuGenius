using System.Diagnostics;
using System.Windows.Media.Imaging;
using MG_Admin_GUI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Data.Common;
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
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;

namespace MG_Admin_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MenugeniusContext dbContext = new MenugeniusContext();
        Purchase purchase = new Purchase();
        Reservation reservation = new Reservation();
        Product product = new Product();
        Category category = new Category();
        Ingredient ingredient = new Ingredient();
        Allergen allergen = new Allergen();
        Desk desk = new Desk();
        private User loggedInUser;

        public MainWindow()
        {
            InitializeComponent();
            ShowLoginWindow();
            LoadDatas();

        }

        private void ShowLoginWindow()
        {
            LoginWindow loginWindow = new LoginWindow(dbContext);
            bool? result = loginWindow.ShowDialog();

            if (result == true)
            {
                loggedInUser = loginWindow.GetLoggedInUser();
            }
            else
            {
                Close();
            }
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

            var orderedPurchases = dbContext.Purchases
                .Where(p => p.Status == "ordered")
                .OrderBy(p => p.DateTime)
                .Include(p => p.ProductPurchases)
                    .ThenInclude(pp => pp.Product)
                .ToList();

            var orderedObservableCollection = new ObservableCollection<Purchase>(orderedPurchases);
            dgOrdered.ItemsSource = orderedObservableCollection;

            var cookedPurchases = dbContext.Purchases
                .Where(p => p.Status == "cooked")
                .OrderBy(p => p.DateTime)
                .Include(p => p.ProductPurchases)
                    .ThenInclude(pp => pp.Product)
                .ToList();

            var cookedObservableCollection = new ObservableCollection<Purchase>(cookedPurchases);
            dgCooked.ItemsSource = cookedObservableCollection;


            var servedPurchases = dbContext.Purchases
                .Where(p => p.Status == "served" && p.Paid == false)
                .OrderBy(p => p.DateTime)
                .Include(p => p.ProductPurchases)
                    .ThenInclude(pp => pp.Product)
                .ToList();

            var servedObservableCollection = new ObservableCollection<Purchase>(servedPurchases);
            dgServed.ItemsSource = servedObservableCollection;

            var paidPurchases = dbContext.Purchases
                .Where(p => p.Status == "served" && p.Paid == true)
                .OrderBy(p => p.DateTime)
                .Include(p => p.ProductPurchases)
                    .ThenInclude(pp => pp.Product)
                .ToList();

            var paidObservableCollection = new ObservableCollection<Purchase>(paidPurchases);
            dgPurchases.ItemsSource = paidObservableCollection;


            //var productsWithIngredients = dbContext.Products
            //    .Include(p => p.ProductIngredients)
            //        .ThenInclude(pi => pi.Ingredient)
            //    .ToList();

            //var productsObservableCollection = new ObservableCollection<Product>(productsWithIngredients);
            //dgProducts.ItemsSource = productsObservableCollection;

            //dgOrdered.ItemsSource = dbContext.Purchases.Local.ToObservableCollection().Where(p => p.Status == "ordered").OrderBy(p => p.DateTime);
            //dgCooked.ItemsSource = dbContext.Purchases.Local.ToObservableCollection().Where(p => p.Status == "cooked").OrderBy(p => p.DateTime);
            //dgServed.ItemsSource = dbContext.Purchases.Local.ToObservableCollection().Where(p => p.Status == "served" && p.Paid == false).OrderBy(p => p.DateTime);
            //dgPurchases.ItemsSource = dbContext.Purchases.Local.ToObservableCollection().Where(p => p.Status == "served" && p.Paid == true).OrderBy(p => p.DateTime);
            dgReservations.ItemsSource = dbContext.Reservations.Local.ToObservableCollection();
            dgProducts.ItemsSource = dbContext.Products.Local.ToObservableCollection();
            dgCategories.ItemsSource = dbContext.Categories.Local.ToObservableCollection();
            dgIngredients.ItemsSource = dbContext.Ingredients.Local.ToObservableCollection();
            dgAllergens.ItemsSource = dbContext.Allergens.Local.ToObservableCollection();
            dgDesks.ItemsSource = dbContext.Desks.Local.ToObservableCollection();
            //spCategory.DataContext = dbContext.Categories.Local.ToObservableCollection();
            //spIngredient.DataContext = dbContext.Ingredients.Local.ToObservableCollection();
            //spAllergen.DataContext = dbContext.Allergens.Local.ToObservableCollection();
            //spDesk.DataContext = dbContext.Desks.Local.ToObservableCollection();
            dgUsers.ItemsSource = dbContext.Users.Local.ToObservableCollection();
            dgEventLogs.ItemsSource = dbContext.EventLogs.Local.ToObservableCollection();

            cobProductCategory.ItemsSource = dbContext.Categories.Local.ToObservableCollection();
            cobProductsIngredients.ItemsSource = dbContext.Ingredients.Local.ToObservableCollection();
            cobIngredientAllergens.ItemsSource = dbContext.Allergens.Local.ToObservableCollection();
        }

        private void ClearTextBoxes()
        {
            tbReservationName.Text = string.Empty;
            tbReservationPhone.Text = string.Empty;
            tbReservationDesk.Text = string.Empty;
            tbReservationNumberOfGuests.Text = string.Empty;
            tbReservationUser.Text = string.Empty;
            dtpReservationCheckinDate.Text = string.Empty;
            dtpReservationCheckoutDate.Text = string.Empty;
            chbReservationClosed.IsChecked = false;
        }

        private void dgPurchases_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ( dgPurchases.SelectedItem != null) 
            { 
                purchase = (Purchase)dgPurchases.SelectedItem;
            }
            if ( dgOrdered.SelectedItem != null) 
            { 
                purchase = (Purchase)dgOrdered.SelectedItem;
            }
            if ( dgCooked.SelectedItem != null) 
            { 
                purchase = (Purchase)dgCooked.SelectedItem;
            }
            if ( dgServed.SelectedItem != null) 
            { 
                purchase = (Purchase)dgServed.SelectedItem;
            }
        }

        private void btnOrderedToCooked_Click(object sender, RoutedEventArgs e)
        {
            if (purchase == null) { return; }
            purchase.Status = "cooked";
            dbContext.SaveChanges();
            LoadDatas();
        }

        private void btnCookedToServed_Click(object sender, RoutedEventArgs e)
        {
            if (purchase == null) { return; }
            purchase.Status = "served";
            dbContext.SaveChanges();
            LoadDatas();
        }

        private void dgReservations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgReservations.SelectedItem == null) { return; }
            reservation = (Reservation)dgReservations.SelectedItem;
            tabReservation.DataContext = reservation;
        }

        private void btnReservationSave_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tbReservationName.Text)) { return; }

            reservation = new Reservation()
            {
                NumberOfGuests = int.Parse(tbReservationNumberOfGuests.Text),
                CheckinDate = DateTime.Parse(dtpReservationCheckinDate.Text),
                CheckoutDate = DateTime.Parse(dtpReservationCheckoutDate.Text),
                Name = tbReservationName.Text,
                Phone = tbReservationPhone.Text,
                DeskId = ulong.Parse(tbReservationDesk.Text),
                UserId = ulong.Parse(tbReservationUser.Text),
                Closed = chbReservationClosed.IsChecked.Value
            };

            if (dgReservations.SelectedItem != null)
            {
                dbContext.Entry((Reservation)dgReservations.SelectedItem).State = EntityState.Unchanged;
            }

            dbContext.Reservations.Add(reservation);
            dbContext.SaveChanges();
            dbContext.Reservations.Load();
            LoadDatas();
            ClearTextBoxes();
        }

        private void btnReservationUpdate_Click(object sender, RoutedEventArgs e)
        {
            dbContext.SaveChanges();
            LoadDatas();
            ClearTextBoxes();
        }

        private void btnReservationDelete_Click(object sender, RoutedEventArgs e)
        {
            dbContext.Reservations.Remove(reservation);
            dbContext.SaveChanges();
            LoadDatas();
        }

        private void dgProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgProducts.SelectedItem == null) { return; }
            product = (Product)dgProducts.SelectedItem;
            tabProduct.DataContext = product;
            var ingredientForProduct = dbContext.ProductIngredients
                .Where(pi => pi.ProductId == product.Id)
                .Select(pi => pi.Ingredient)
                .ToList();
            lvProductIngredients.ItemsSource = ingredientForProduct;
        }

        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCategories.SelectedItem == null) { return; }
            category = (Category)dgCategories.SelectedItem;
            spCategory.DataContext = category;
        }

        private void dgIngredients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgIngredients.SelectedItem == null) { return; }
            ingredient = (Ingredient)dgIngredients.SelectedItem;
            spIngredient.DataContext = ingredient;

            var allergenForIngredient = dbContext.IngredientAllergens
                .Where(pi => pi.IngredientId == ingredient.Id)
                .Select(pi => pi.Allergen)
                .ToList();
            lvIngredientAllergens.ItemsSource = allergenForIngredient;

        }

        private void dgAllergens_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgAllergens.SelectedItem == null) { return; }
            allergen = (Allergen)dgAllergens.SelectedItem;
            spAllergen.DataContext = allergen;
        }

        private void dgDesks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDesks.SelectedItem == null) { return; }
            desk = (Desk)dgDesks.SelectedItem;
            spDesk.DataContext = desk;
        }

        private void btnNewImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Képfájlok|*.jpg;*.png;*.bmp|Mindennemű fájl|*.*";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;
                string fileName = System.IO.Path.GetFileName(imagePath);

                BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath));
                byte[] imageData = ConvertBitmapToByteArray(bitmapImage);

                tbImageName.Text = fileName;
                product.Image.ImgData = imageData;
            }
        }

        private byte[] ConvertBitmapToByteArray(BitmapImage bitmapImage)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(stream);

                return stream.ToArray();
            }
        }


    }
}