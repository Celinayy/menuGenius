using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;
using MG_Admin_GUI.ViewModels;
using MG_Admin_GUI.Models;


namespace MG_Admin_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public PurchaseViewModel PurchaseVM { get; set; }
        public ReservationViewModel ReservationVM { get; set; }
        public ProductViewModel ProductVM { get; set; }
        public IngredientViewModel IngredientVM { get; set; }
        public CategoryViewModel CategoryVM { get; set; }
        public AllergenViewModel AllergenVM { get; set; }
        public DeskViewModel DeskVM { get; set; }
        public UserViewModel UserVM { get; set; }
        public EventLogViewModel EventLogVM { get; set; }

        private string openedFilePath;
        private bool isImageModified = false;
        string connectionString;
        //string imageFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");

        private User loggedInUser;

        OpenFileDialog openFileDialog = new OpenFileDialog();


        public MainWindow()
        {
            InitializeComponent();

            PurchaseVM = new PurchaseViewModel();
            ReservationVM = new ReservationViewModel();
            CategoryVM = new CategoryViewModel();
            ProductVM = new ProductViewModel();
            IngredientVM = new IngredientViewModel();
            AllergenVM = new AllergenViewModel();
            DeskVM = new DeskViewModel();
            UserVM = new UserViewModel();
            EventLogVM = new EventLogViewModel();

            UpdateClassesFromDatabase();
            SetDatacontexts();
            RefreshListViews();
            ShowLoginWindow();

        }

        private void ShowLoginWindow()
        {
            LoginWindow loginWindow = new LoginWindow(UserVM.Users);
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

        private void DataUpdater_DataUpdated(object sender, EventArgs e)
        {
            Dispatcher.Invoke(() => UpdateClassesFromDatabase());
        }

        public void SetDatacontexts()
        {
            cobCategories.ItemsSource = CategoryVM.Categories;
            cobIngredientAllergen.ItemsSource = AllergenVM.Allergens;
            cobIngredients.ItemsSource = IngredientVM.Ingredients;
            cobReservationDesks.ItemsSource = DeskVM.Desks;

            tabControl.DataContext = PurchaseVM;
            tabReservation.DataContext = ReservationVM;
            tabProduct.DataContext = ProductVM;
            grdIngredients.DataContext = IngredientVM;
            grdCategories.DataContext = CategoryVM;
            grdAllergens.DataContext = AllergenVM;
            grdDesks.DataContext = DeskVM;
            tabUser.DataContext = UserVM;
            tabEventLog.DataContext = EventLogVM;
        }

        public void UpdateClassesFromDatabase()
        {
            AllergenVM.Allergens = Allergen.GetAllergens();
            IngredientVM.Ingredients = Ingredient.GetIngredients();
            CategoryVM.Categories = Category.GetCategories();
            ProductVM.Products = Product.GetProducts();
            UserVM.Users = User.GetUsers();
            DeskVM.Desks = Desk.GetDesks();
            PurchaseVM.Purchases = Purchase.GetPurchases();
            ReservationVM.Reservations = Reservation.GetReservations();
            EventLogVM.EventLogs = EventLog.GetEventLogs();
        }

        private void RefreshListViews()
        {
            lvPurchased.ItemsSource = PurchaseVM.Purchases.Where(purchase => purchase.status == "ordered").ToList();
            lvCooked.ItemsSource = PurchaseVM.Purchases.Where(purchase => purchase.status == "cooked").ToList();
            lvServed.ItemsSource = PurchaseVM.Purchases.Where(purchase => purchase.status == "served" && purchase.paid == false).ToList();
            dgPurchases.ItemsSource = PurchaseVM.Purchases.Where(purchase => purchase.status == "served" && purchase.paid == true).ToList();
        }

        private void UpdatePurchaseStatus(int purchaseId, string newStatus)
        {
            using (var connection = DatabaseHandler.OpenConnection())
            {
                string query = "UPDATE purchases SET status = @status WHERE id = @id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@status", newStatus.ToString());
                    command.Parameters.AddWithValue("@id", purchaseId);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void btnOrderedToCooked_Click(object sender, RoutedEventArgs e)
        {
            if (lvPurchased.SelectedItem != null)
            {
                Purchase selectedPurchase = (Purchase)lvPurchased.SelectedItem;
                selectedPurchase.status = "cooked";
                UpdatePurchaseStatus(selectedPurchase.id, "cooked");
                RefreshListViews();
            }
        }

        private void btnCookedToServed_Click(object sender, RoutedEventArgs e)
        {
            if (lvCooked.SelectedItem != null)
            {
                Purchase selectedPurchase = (Purchase)lvCooked.SelectedItem;
                selectedPurchase.status = "served";
                UpdatePurchaseStatus(selectedPurchase.id, "served");
                RefreshListViews();
            }

        }

        private void chkPurchased_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var purchase = (Purchase)checkBox.DataContext;
            purchase.paid = true;
            UpdatePaidStatus(purchase, true);
        }

        private void chkPurchased_Unchecked(object sender, RoutedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var purchase = (Purchase)checkBox.DataContext;
            purchase.paid = false;
            UpdatePaidStatus(purchase, false);
        }

        private void UpdatePaidStatus(Purchase purchase, bool isPaid)
        {
            int id = purchase.id;
            using (var connection = DatabaseHandler.OpenConnection())
            {
                connection.Open();
                string query = "UPDATE purchase SET paid = @isPaid WHERE id = @id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ispaid", isPaid);
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
            RefreshListViews();
        }

        private void dgReservations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgReservations.SelectedItem != null)
            {
                ReservationVM.selectedReservation = (Reservation)dgReservations.SelectedItem;
                cobReservationDesks.SelectedIndex = dgReservations.SelectedIndex;
            }
        }

        private void dgProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgProducts.SelectedItem != null)
            {
                ProductVM.selectedProduct = (Product)dgProducts.SelectedItem;

                //string newImagePath = ProductVM.selectedProduct.FullImagePath;

                //BitmapImage bi = new BitmapImage();
                //bi.BeginInit();
                //bi.CacheOption = BitmapCacheOption.OnLoad;
                //bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                //bi.UriSource = new Uri(newImagePath);
                //bi.EndInit();
                //imgProductImage.Source = bi;
                //cobCategories.SelectedItem = ProductVM.selectedProduct.productCategory;

                //Category selectedCategory = (Category)CategoryVM.Categories.FirstOrDefault(category => category.id == ProductVM.selectedProduct.productCategory.id);
                //cobCategories.SelectedValue = selectedCategory;
            }

            //if (dgProducts.SelectedItem != null)
            //{
            //    ProductVM.selectedProduct = (Product)dgProducts.SelectedItem;
            //    string newImagePath = ProductVM.selectedProduct.FullImagePath;

            //    imgProductImage.Source = new BitmapImage(new Uri(newImagePath));
            //    Category selectedCategory = CategoryVM.Categories.FirstOrDefault(category => category.id == ProductVM.selectedProduct.productCategory.id);
            //    cobCategories.SelectedItem = selectedCategory;
            //}
        }

        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            lvProductIngredient.ItemsSource = null;
            lvProductIngredient.Items.Add(cobIngredients.SelectedItem);

            //if (ProductVM.selectedProduct == null)
            //{
            //    ProductVM.selectedProduct = new Product();
            //}

            //if (ProductVM.selectedProduct.productIngredients == null)
            //{
            //    ProductVM.selectedProduct.productIngredients = new ObservableCollection<Ingredient>();
            //}

            //ProductVM.selectedProduct.productIngredients.Add(cobIngredients.SelectedItem as Ingredient);

            // Frissítsd az ItemsSource-t, ha kell
            //lvProductIngredient.ItemsSource = ProductVM.selectedProduct.productIngredients;
        }

        public static BitmapImage LoadBitmapImage(string fileName)
        {
            BitmapImage bitmapImage = new BitmapImage();

            // UI szálra való visszatérés Dispatcher.Invoke segítségével.
            Application.Current.Dispatcher.Invoke(() =>
            {
                using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                    bitmapImage.Freeze();
                }
            });

            return bitmapImage;


            //using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            //{
            //    var bitmapImage = new BitmapImage();
            //    bitmapImage.BeginInit();
            //    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            //    bitmapImage.StreamSource = stream;
            //    bitmapImage.EndInit();
            //    bitmapImage.Freeze();
            //    return bitmapImage;
            //}
        }

        private void btnImageOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Képfájlok (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                if (openFileDialog.ShowDialog() == true)
                {

                    imgProductImage.Source = LoadBitmapImage(openFileDialog.FileName);
                    tbProductImage.Text = Path.GetFileName(openFileDialog.FileName);
                    openedFilePath = openFileDialog.FileName;
                    isImageModified = true;

                }

                //VAGY

                //OpenFileDialog openFileDialog = new OpenFileDialog();
                //openFileDialog.Filter = "Képfájlok (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

                //if (openFileDialog.ShowDialog() == true)
                //{

                //    using (FileStream stream = File.OpenRead(openFileDialog.FileName))
                //    {
                //        byte[] imageData = new byte[stream.Length];
                //        stream.Read(imageData, 0, imageData.Length);
                //        imgProductImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                //        tbProductImage.Text = Path.GetFileName(openFileDialog.FileName);
                //        openedFilePath = openFileDialog.FileName;
                //        isImageModified = true;

                //        stream.Close();
                //    }
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt a fájl megnyitása közben: {ex.Message}");
            }
        }

        private void btnAddAllergen_Click(object sender, RoutedEventArgs e)
        {
            lvIngredientAllergens.ItemsSource = null;
            lvIngredientAllergens.Items.Add(cobIngredientAllergen.SelectedItem);
        }

        private void dgIngredients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgIngredients.SelectedItem != null)
            {
                IngredientVM.selectedIngredient = (Ingredient)dgIngredients.SelectedItem;
            }
        }

        private void dgCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCategories.SelectedItem != null)
            {
                CategoryVM.selectedCategory = (Category)dgCategories.SelectedItem;
            }
        }

        private void dgAllergens_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgAllergens.SelectedItem != null)
            {
                AllergenVM.selectedAllergen = (Allergen)dgAllergens.SelectedItem;
            }
        }

        private void dgDesks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDesks.SelectedItem != null)
            {
                DeskVM.selectedDesk = (Desk)dgDesks.SelectedItem;
            }
        }

        private void dgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgUsers.SelectedItem != null)
            {
                UserVM.selectedUser = (User)dgUsers.SelectedItem;
            }
        }

        private void dgEventLogs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgEventLogs.SelectedItem != null)
            {
                EventLogVM.selectedEvent = (EventLog)dgEventLogs.SelectedItem;
            }
        }

        private void btnDataSave_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                string buttonName = clickedButton.Name;
                string saveQuery = null;
                string tableName = null;
                string tableName2 = null;
                Dictionary<string, object> parameters = new Dictionary<string, object>();


                try
                {
                    switch (buttonName)
                    {
                        case "btnReservationSave":
                            tableName = "reservation";
                            saveQuery = $"INSERT INTO {tableName}(id, numberOfGuests, arrivalTime, getawayTime, name, phone, deskId) VALUES(@id, @numberOfGuests, @arrivalTime, @getawayTime, @name, @phone, @deskId)";

                            parameters.Add("@id", int.Parse(tbReservationId.Text));
                            parameters.Add("@numberOfGuests", int.Parse(tbReservationGuestNo.Text));
                            parameters.Add("@arrivalTime", DateTime.Parse(dtpReservationArrivalTime.Text));
                            parameters.Add("@getawayTime", DateTime.Parse(dtpReservationGetawayTime.Text));
                            parameters.Add("@name", tbReservationName.Text);
                            parameters.Add("@phone", tbReservationPhone.Text);
                            parameters.Add("@deskId", cobReservationDesks.SelectedValue);
                            break;

                        case "btnProductSave":
                            tableName = "product";
                            tableName2 = "product_ingredient";
                            saveQuery = $"INSERT INTO {tableName} (id, name, description, categoryId, packing, price, isFood, image) VALUES (@id, @name, @description, @categoryId, @packing, @price, @isFood, @image);";

                            parameters.Add("@id", int.Parse(tbProductId.Text));
                            parameters.Add("@name", tbProductName.Text);
                            parameters.Add("@description", tbProductDescription.Text);
                            parameters.Add("@categoryId", cobCategories.SelectedValue);
                            parameters.Add("@packing", tbProductPacking.Text);
                            parameters.Add("@price", int.Parse(tbProductPrice.Text));
                            parameters.Add("@isFood", cbProductIsFood.IsChecked.HasValue && cbProductIsFood.IsChecked.Value);
                            parameters.Add("@image", tbProductImage.Text);

                            List<string> ingredientInsertCommands = new List<string>();
                            foreach (var ingredient in lvProductIngredient.Items)
                            {
                                int ingredientId = ((Ingredient)ingredient).id;
                                string ingredientInsertCommand = $"INSERT INTO {tableName2} (productId, ingredientId) VALUES (@id, {ingredientId});";
                                ingredientInsertCommands.Add(ingredientInsertCommand);
                            }

                            saveQuery += string.Join("", ingredientInsertCommands);

                            //string destinationPath = Path.Combine(imageFolderPath, tbProductImage.Text);
                            //File.Copy(openedFilePath, destinationPath, true);
                            break;

                        case "btnIngredientSave":
                            tableName = "ingredient";
                            tableName2 = "ingredient_allergen";
                            saveQuery = $"INSERT INTO {tableName} (id, name, inStock, qUnit) VALUES (@id, @name);";

                            parameters.Add("@id", int.Parse(tbIngredientId.Text));
                            parameters.Add("@name", tbIngredientName.Text);

                            List<string> allergenInsertCommands = new List<string>();
                            foreach (var allergen in lvIngredientAllergens.Items)
                            {
                                int allergenId = ((Allergen)allergen).id;
                                string allergenInsertCommand = $"INSERT INTO {tableName2} (ingredientId, allergenId) VALUES (@id, {allergenId});";
                                allergenInsertCommands.Add(allergenInsertCommand);
                            }

                            saveQuery += string.Join("", allergenInsertCommands);

                            break;

                        case "btnCategorySave":
                            tableName = "category";
                            saveQuery = $"INSERT INTO {tableName} (id, name) VALUES (@id, @name)";

                            parameters.Add("@id", int.Parse(tbCategoryId.Text));
                            parameters.Add("@name", tbCategoryName.Text);
                            break;

                        case "btnAllergenSave":
                            tableName = "allergen";
                            saveQuery = $"INSERT INTO {tableName} (id, code, name) VALUES (@id, @code, @name)";

                            parameters.Add("@id", int.Parse(tbAllergenId.Text));
                            parameters.Add("@code", decimal.Parse(tbAllergenCode.Text.Replace('.', ',')));
                            parameters.Add("@name", tbAllergenName.Text);
                            break;

                        case "btnDeskSave":
                            tableName = "desk";
                            saveQuery = $"INSERT INTO {tableName} (id, numberOfSeats) VALUES (@id, @numberOfSeats)";

                            parameters.Add("@id", int.Parse(tbDeskId.Text));
                            parameters.Add("@numberOfSeats", int.Parse(tbDeskNumberOfSeats.Text));
                            break;

                        case "btnUserSave":
                            tableName = "user";
                            saveQuery = $"INSERT INTO {tableName} (id, name, email, password, phone, admin) VALUES (@id, @name, @email, @password, @phone, @admin)";

                            parameters.Add("@id", int.Parse(tbUserId.Text));
                            parameters.Add("@name", tbUserName.Text);
                            parameters.Add("@email", tbUserEmail.Text);
                            parameters.Add("@password", tbUserPassword.Text);
                            parameters.Add("@phone", tbUserPhone.Text);
                            parameters.Add("@admin", cbUserAdmin.IsChecked.HasValue && cbUserAdmin.IsChecked.Value);
                            break;

                        default:
                            // Alapértelmezett eset
                            break;
                    }
                    ExecuteQuery(saveQuery, parameters);
                    //ClearPropertyFields();
                    ClearSelectedItems();
                    UpdateClassesFromDatabase();
                }


                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Hiba történt az új adat felvitelekor a {0} táblába! {1}", tableName, ex.Message), "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnDataUpdate_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                string buttonName = clickedButton.Name;
                string updateQuery = null;
                string tableName = null;
                string tableName2 = null;
                string deleteQuery = null;
                Dictionary<string, object> parameters = new Dictionary<string, object>();


                try
                {
                    switch (buttonName)
                    {
                        case "btnReservationUpdate":
                            tableName = "reservation";
                            updateQuery = $"UPDATE {tableName} SET id = @id, numberOfGuests = @numberOfGuests, arrivalTime = @arrivalTime, getawayTime = @getawayTime, name = @name, phone = @phone, deskId = @deskId WHERE id = @id";

                            parameters.Add("@id", int.Parse(tbReservationId.Text));
                            parameters.Add("@numberOfGuests", int.Parse(tbReservationGuestNo.Text));
                            parameters.Add("@arrivalTime", DateTime.Parse(dtpReservationArrivalTime.Text));
                            parameters.Add("@getawayTime", DateTime.Parse(dtpReservationGetawayTime.Text));
                            parameters.Add("@name", tbReservationName.Text);
                            parameters.Add("@phone", tbReservationPhone.Text);
                            parameters.Add("@deskId", cobReservationDesks.SelectedValue);
                            break;

                        case "btnProductUpdate":
                            tableName = "product";
                            tableName2 = "product_ingredient";
                            updateQuery = $"UPDATE {tableName} SET name = @name, description = @description, categoryId = @categoryId, packing = @packing, price = @price, isFood = @isFood, image = @image WHERE id = @id;";

                            parameters.Add("@id", int.Parse(tbProductId.Text));
                            parameters.Add("@name", tbProductName.Text);
                            parameters.Add("@description", tbProductDescription.Text);
                            parameters.Add("@categoryId", cobCategories.SelectedValue);
                            parameters.Add("@packing", tbProductPacking.Text);
                            parameters.Add("@price", int.Parse(tbProductPrice.Text));
                            parameters.Add("@isFood", cbProductIsFood.IsChecked.HasValue && cbProductIsFood.IsChecked.Value);
                            parameters.Add("@image", tbProductImage.Text);

                            deleteQuery = $"DELETE FROM {tableName2} WHERE productId = @id";
                            using (var connection = DatabaseHandler.OpenConnection())
                            {
                                using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
                                {
                                    command.Parameters.AddWithValue("@id", int.Parse(tbProductId.Text));
                                    command.ExecuteNonQuery();
                                }

                                connection.Close();
                            }

                            List<string> ingredientInsertCommands = new List<string>();
                            foreach (var ingredient in lvProductIngredient.Items)
                            {
                                int ingredientId = ((Ingredient)ingredient).id;
                                string ingredientInsertCommand = $"INSERT INTO {tableName2} (productId, ingredientId) VALUES (@id, {ingredientId});";
                                ingredientInsertCommands.Add(ingredientInsertCommand);
                            }

                            updateQuery += string.Join("", ingredientInsertCommands);

                            if (isImageModified)
                            {
                                //string destinationPath = Path.Combine(imageFolderPath, tbProductImage.Text);
                                //File.Copy(openedFilePath, destinationPath, true);
                            }
                            break;

                        case "btnIngredientUpdate":
                            tableName = "ingredient";
                            tableName2 = "ingredient_allergen";
                            updateQuery = $"UPDATE {tableName} SET name = @name, WHERE id = @id";

                            parameters.Add("@id", int.Parse(tbIngredientId.Text));
                            parameters.Add("@name", tbIngredientName.Text);

                            deleteQuery = $"DELETE FROM {tableName2} WHERE ingredientId = @id";
                            using (var connection = DatabaseHandler.OpenConnection())
                            {
                                using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
                                {
                                    command.Parameters.AddWithValue("@id", int.Parse(tbIngredientId.Text));
                                    command.ExecuteNonQuery();
                                }

                                connection.Close();
                            }

                            List<string> allergenInsertCommands = new List<string>();
                            foreach (var allergen in lvIngredientAllergens.Items)
                            {
                                int allergenId = ((Allergen)allergen).id;
                                string allergenInsertCommand = $"INSERT INTO {tableName2} (ingredientId, allergenId) VALUES (@id, {allergenId});";
                                allergenInsertCommands.Add(allergenInsertCommand);
                            }

                            updateQuery += string.Join("", allergenInsertCommands);

                            break;

                        case "btnCategoryUpdate":
                            tableName = "category";
                            updateQuery = $"UPDATE {tableName} SET name = @name WHERE id = @id";

                            parameters.Add("@id", int.Parse(tbCategoryId.Text));
                            parameters.Add("@name", tbCategoryName.Text);
                            break;

                        case "btnAllergenUpdate":
                            tableName = "allergen";
                            updateQuery = $"UPDATE {tableName} SET code = @code, name = @name WHERE id = @id";

                            parameters.Add("@id", int.Parse(tbAllergenId.Text));
                            parameters.Add("@code", decimal.Parse(tbAllergenCode.Text.Replace('.', ',')));
                            parameters.Add("@name", tbAllergenName.Text);
                            break;

                        case "btnDeskUpdate":
                            tableName = "desk";
                            updateQuery = $"UPDATE {tableName} SET numberOfSeats = @numberOfSeats WHERE id = @id";

                            parameters.Add("@id", int.Parse(tbDeskId.Text));
                            parameters.Add("@numberOfSeats", int.Parse(tbDeskNumberOfSeats.Text));
                            break;

                        case "btnUserUpdate":
                            tableName = "user";
                            updateQuery = $"UPDATE {tableName} SET name = @name, email = @email, password = @password, phone = @phone, admin = @admin WHERE id = @id";

                            parameters.Add("@id", int.Parse(tbUserId.Text));
                            parameters.Add("@name", tbUserName.Text);
                            parameters.Add("@email", tbUserEmail.Text);
                            parameters.Add("@password", tbUserPassword.Text);
                            parameters.Add("@phone", tbUserPhone.Text);
                            parameters.Add("@admin", cbUserAdmin.IsChecked.HasValue && cbUserAdmin.IsChecked.Value);
                            break;

                        default:
                            // Alapértelmezett eset
                            break;
                    }
                    ExecuteQuery(updateQuery, parameters);
                    //ClearPropertyFields();
                    ClearSelectedItems();
                    UpdateClassesFromDatabase();
                }


                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Hiba történt a {0} frissítésekor! {1}", tableName, ex.Message), "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnDataDelete_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                string buttonName = clickedButton.Name;
                string deleteQuery = null;
                string tableName = null;
                string tableName2 = null;
                Dictionary<string, object> parameters = new Dictionary<string, object>();


                try
                {
                    switch (buttonName)
                    {
                        case "btnReservationDelete":
                            tableName = "reservation";
                            deleteQuery = $"DELETE FROM {tableName} WHERE id = @id";

                            parameters.Add("@id", int.Parse(tbReservationId.Text));

                            break;

                        case "btnProductDelete":
                            tableName = "product";
                            tableName2 = "product_ingredient";
                            deleteQuery = $"DELETE FROM {tableName2} WHERE productId = @id";
                            deleteQuery += $"DELETE FROM {tableName} WHERE id = @id";


                            parameters.Add("@id", int.Parse(tbProductId.Text));

                            try
                            {
                                //int searchedId = int.Parse(tbProductId.Text);
                                //Product selectedProduct = ProductVM.Products.FirstOrDefault(p => p.id == searchedId);
                                //imgProductImage.Source = null;
                                //GC.Collect();
                                //GC.WaitForPendingFinalizers();
                                //File.Delete(ProductVM.selectedProduct.FullImagePath);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }

                            break;

                        case "btnIngredientDelete":
                            tableName = "ingredient";
                            deleteQuery = $"DELETE FROM {tableName} WHERE id = @id";

                            parameters.Add("@id", int.Parse(tbIngredientId.Text));

                            break;

                        case "btnCategoryDelete":
                            tableName = "category";
                            deleteQuery = $"DELETE FROM {tableName} WHERE id = @id";

                            parameters.Add("@id", int.Parse(tbCategoryId.Text));

                            break;

                        case "btnAllergenDelete":
                            tableName = "allergen";
                            deleteQuery = $"DELETE FROM {tableName} WHERE id = @id";

                            parameters.Add("@id", int.Parse(tbAllergenId.Text));

                            break;

                        case "btnDeskDelete":
                            tableName = "desk";
                            deleteQuery = $"DELETE FROM {tableName} WHERE id = @id";

                            parameters.Add("@id", int.Parse(tbDeskId.Text));

                            break;

                        case "btnUserDelete":
                            tableName = "user";
                            deleteQuery = $"DELETE FROM {tableName} WHERE id = @id";

                            parameters.Add("@id", int.Parse(tbUserId.Text));

                            break;

                        default:
                            // Alapértelmezett eset
                            break;
                    }
                    ExecuteQuery(deleteQuery, parameters);
                    ClearSelectedItems();
                    UpdateClassesFromDatabase();
                    cobCategories.ItemsSource = CategoryVM.Categories;
                }

                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Hiba történt a {0} frissítésekor! {1}", tableName, ex.Message), "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        public void ExecuteQuery(string query, Dictionary<string, object> parameters = null)
        {
            using (var connection = DatabaseHandler.OpenConnection())
            {
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            AddParametersToCommand(command, parameters);
                        }
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hiba történt az adatbázisba írás során!" + ex.Message, "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                    transaction.Rollback();
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        private void AddParametersToCommand(MySqlCommand command, Dictionary<string, object> parameters)
        {
            foreach (var parameter in parameters)
            {
                command.Parameters.AddWithValue(parameter.Key, parameter.Value);
            }
        }


        private void btnDataCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearSelectedItems();
        }

        private void ClearPropertyFields()
        {
            tbReservationId.Text = string.Empty;
            tbReservationGuestNo.Text = string.Empty;
            dtpReservationArrivalTime.Text = string.Empty;
            dtpReservationGetawayTime.Text = string.Empty;
            dtpReservationArrivalTime.Value = DateTime.Now;
            dtpReservationGetawayTime.Value = DateTime.Now;
            tbReservationName.Text = string.Empty;
            tbReservationPhone.Text = string.Empty;
            cobReservationDesks.SelectedItem = null;

            tbProductId.Text = string.Empty;
            tbProductName.Text = string.Empty;
            cobCategories.SelectedValue = null;
            tbProductPacking.Text = string.Empty;
            tbProductPrice.Text = string.Empty;
            cbProductIsFood.IsChecked = false;
            tbProductDescription.Text = string.Empty;
            imgProductImage.Source = null;

            tbIngredientId.Text = string.Empty;
            tbIngredientName.Text = string.Empty;

            tbCategoryId.Text = string.Empty;
            tbCategoryName.Text = string.Empty;

            tbAllergenId.Text = string.Empty;
            tbAllergenCode.Text = string.Empty;
            tbAllergenName.Text = string.Empty;

            tbDeskId.Text = string.Empty;
            tbDeskNumberOfSeats.Text = string.Empty;

            tbUserId.Text = string.Empty;
            tbUserName.Text = string.Empty;
            tbUserEmail.Text = string.Empty;
            tbUserPassword.Text = string.Empty;
            tbUserPhone.Text = string.Empty;
            cbUserAdmin.IsChecked = false;

            tbEventLogId.Text = string.Empty;
            tbEventLogEventType.Text = string.Empty;
            tbEventLogAffectedTable.Text = string.Empty;
            tbEventLogAffectedId.Text = string.Empty;
            tbEventLogEventDescription.Text = string.Empty;
            tbEventLogDate.Text = string.Empty;
        }

        private void ClearSelectedItems()
        {
            ReservationVM.selectedReservation = null;
            ProductVM.selectedProduct = null;
            IngredientVM.selectedIngredient = null;
            CategoryVM.selectedCategory = null;
            AllergenVM.selectedAllergen = null;
            DeskVM.selectedDesk = null;
            UserVM.selectedUser = null;
            EventLogVM.selectedEvent = null;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void btnImageCancel_Click(object sender, RoutedEventArgs e)
        {
            imgProductImage.Source = null;
        }
    }
}

