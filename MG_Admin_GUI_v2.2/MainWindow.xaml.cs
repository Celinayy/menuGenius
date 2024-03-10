using MG_Admin_GUI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MG_Admin_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        menugeniusContext dbContext = new menugeniusContext();

        product selectedProduct = new product();
        purchase selectedPurchase = new purchase();
        reservation selectedReservation = new reservation();
        category selectedCategory = new category();
        ingredient selectedIngredient = new ingredient();
        allergen selectedAllergen = new allergen();
        desk selectedDesk = new desk();
        user selectedUser = new user();
        event_log selectedEventLog = new event_log();

        private user loggedInUser;


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
            dbContext.allergens.Load();
            dbContext.categories.Load();
            dbContext.desks.Load();
            dbContext.event_logs.Load();
            dbContext.images.Load();
            dbContext.ingredients.Load();
            dbContext.products.Load();
            dbContext.purchases.Load();
            dbContext.reservations.Load();
            dbContext.users.Load();
            dbContext.product_ingredients.Load();
            dbContext.ingredient_allergens.Load();

            var orderedPurchases = dbContext.purchases
                .Where(p => p.status == "ordered")
                .OrderBy(p => p.date_time)
                .Include(p => p.product_purchases)
            .ThenInclude(pp => pp.product)
                .ToList();

            var orderedObservableCollection = new ObservableCollection<purchase>(orderedPurchases);
            dgOrdered.ItemsSource = orderedObservableCollection;

            var cookedPurchases = dbContext.purchases
                .Where(p => p.status == "cooked")
                .OrderBy(p => p.date_time)
                .Include(p => p.product_purchases)
            .ThenInclude(pp => pp.product)
            .ToList();

            var cookedObservableCollection = new ObservableCollection<purchase>(cookedPurchases);
            dgCooked.ItemsSource = cookedObservableCollection;


            var servedPurchases = dbContext.purchases
                .Where(p => p.status == "served" && p.paid == false)
                .OrderBy(p => p.date_time)
                .Include(p => p.product_purchases)
            .ThenInclude(pp => pp.product)
            .ToList();

            var servedObservableCollection = new ObservableCollection<purchase>(servedPurchases);
            dgServed.ItemsSource = servedObservableCollection;

            var paidPurchases = dbContext.purchases
                .Where(p => p.status == "served" && p.paid == true)
                .OrderBy(p => p.date_time)
                .Include(p => p.product_purchases)
            .ThenInclude(pp => pp.product)
            .ToList();

            var paidObservableCollection = new ObservableCollection<purchase>(paidPurchases).OrderBy(p => p.date_time);
            dgPurchases.ItemsSource = paidObservableCollection;

            dgReservations.ItemsSource = dbContext.reservations.Local.ToObservableCollection();
            dgProducts.ItemsSource = dbContext.products.Local.ToObservableCollection();
            dgCategories.ItemsSource = dbContext.categories.Local.ToObservableCollection();
            dgIngredients.ItemsSource = dbContext.ingredients.Local.ToObservableCollection();
            dgAllergens.ItemsSource = dbContext.allergens.Local.ToObservableCollection();
            dgDesks.ItemsSource = dbContext.desks.Local.ToObservableCollection();
            dgUsers.ItemsSource = dbContext.users.Local.ToObservableCollection();
            dgEventLogs.ItemsSource = dbContext.event_logs.Local.ToObservableCollection();

            cobReservationDesk.ItemsSource = dbContext.desks.Local.ToObservableCollection().OrderBy(d => d.id);
            cobReservationUser.ItemsSource = dbContext.users.Local.ToObservableCollection().OrderBy(u => u.name);
            cobProductCategory.ItemsSource = dbContext.categories.Local.ToObservableCollection().OrderBy(c => c.name);
            cobProductIngredients.ItemsSource = dbContext.ingredients.Local.ToObservableCollection().OrderBy(i => i.name);
            cobIngredientAllergens.ItemsSource = dbContext.allergens.Local.ToObservableCollection().OrderBy(a => a.name);
        }
        #region Purchases
        private void dgPurchases_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgPurchases.SelectedItem != null)
            {
                selectedPurchase = (purchase)dgPurchases.SelectedItem;
            }
            if (dgOrdered.SelectedItem != null)
            {
                selectedPurchase = (purchase)dgOrdered.SelectedItem;
            }
            if (dgCooked.SelectedItem != null)
            {
                selectedPurchase = (purchase)dgCooked.SelectedItem;
            }
            if (dgServed.SelectedItem != null)
            {
                selectedPurchase = (purchase)dgServed.SelectedItem;
            }
        }

        private void dgOrdered_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dgOrdered.SelectedItem = null;
            selectedPurchase = null;
            tabPurchase.DataContext = selectedPurchase;
        }

        private void dgCooked_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dgCooked.SelectedItem = null;
            selectedPurchase = null;
            tabPurchase.DataContext = selectedPurchase;
        }

        private void dgServed_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dgServed.SelectedItem = null;
            selectedPurchase = null;
            tabPurchase.DataContext = selectedPurchase;
        }

        private void dgPurchases_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dgPurchases.SelectedItem = null;
            selectedPurchase = null;
            tabPurchase.DataContext = selectedPurchase;
        }

        private void btnOrderedToCooked_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPurchase == null) { return; }
            selectedPurchase.status = "cooked";
            dbContext.SaveChanges();
            LoadDatas();
        }

        private void btnCookedToServed_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPurchase == null) { return; }
            selectedPurchase.status = "served";
            dbContext.SaveChanges();
            LoadDatas();
        }

        void OnChecked(object sender, RoutedEventArgs e)
        {
            if (selectedPurchase == null) { return; }
            selectedPurchase.paid = true;
            dbContext.SaveChanges();
            LoadDatas();
        }
        #endregion

        #region Reservations
        private void dgReservations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgReservations.SelectedItem == null) { return; }
            selectedReservation = (reservation)dgReservations.SelectedItem;
            tabReservation.DataContext = selectedReservation;

        }

        private void dgReservations_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }


        #endregion

        #region Products
        private void dgProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgProducts.SelectedItem == null) { return; }
            selectedProduct = (product)dgProducts.SelectedItem;
            tabProduct.DataContext = selectedProduct;
        }

        private void dgProducts_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dgProducts.SelectedItem = null;
            selectedProduct = null;
            tabProduct.DataContext = selectedProduct;
        }


        #endregion

        #region Lists
        private void dgCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCategories.SelectedItem == null) { return; }
            selectedCategory = (category)dgCategories.SelectedItem;
            spCategory.DataContext = selectedCategory;
        }

        private void dgCategories_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dgCategories.SelectedItem = null;
            selectedCategory = null;
            spCategory.DataContext = selectedCategory;
        }



        private void dgIngredients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgIngredients.SelectedItem == null) { return; }
            selectedIngredient = (ingredient)dgIngredients.SelectedItem;
            spIngredient.DataContext = selectedIngredient;
        }

        private void dgIngredients_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dgIngredients.SelectedItem = null;
            selectedIngredient = null;
            spIngredient.DataContext = selectedIngredient;

        }

        private void dgAllergens_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgAllergens.SelectedItem == null) { return; }
            selectedAllergen = (allergen)dgAllergens.SelectedItem;
            spAllergen.DataContext = selectedAllergen;
        }

        private void dgAllergens_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dgAllergens.SelectedItem = null;
            selectedAllergen = null;
            spAllergen.DataContext = selectedAllergen;
        }

        private void dgDesks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDesks.SelectedItem == null) { return; }
            selectedDesk = (desk)dgDesks.SelectedItem;
            spDesk.DataContext = selectedDesk;
        }

        private void dgDesks_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dgDesks.SelectedItem = null;
            selectedDesk = null;
            spDesk.DataContext = selectedDesk;
        }

        #endregion

        #region Users
        private void dgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgUsers.SelectedItem == null) { return; }
            selectedUser = (user)dgUsers.SelectedItem;
            tabUser.DataContext = selectedUser;
        }

        private void dgUsers_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dgUsers.SelectedItem = null;
            selectedUser = null;
            tabUser.DataContext = selectedUser;
        }


        #endregion

        #region EventLog
        private void dgEventLogs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgEventLogs.SelectedItem == null) { return; }
            selectedEventLog = (event_log)dgEventLogs.SelectedItem;
            tabEventLog.DataContext = selectedEventLog;
        }

        private void dgEventLogs_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dgEventLogs.SelectedItem = null;
            selectedEventLog = null;
            tabEventLog.DataContext = selectedEventLog;
        }

        #endregion

    }
}