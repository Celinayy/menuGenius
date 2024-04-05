using MG_Admin_GUI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
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

namespace MG_Admin_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        menugeniusContext dbContext = new menugeniusContext();

        product selectedProduct = new product();
        image selectedImage = new image();
        purchase selectedPurchase = new purchase();
        reservation selectedReservation = new reservation();
        category selectedCategory = new category();
        ingredient selectedIngredient = new ingredient();
        allergen selectedAllergen = new allergen();
        desk selectedDesk = new desk();
        user selectedUser = new user();
        event_log selectedEventLog = new event_log();

        private user loggedInUser;
        string imagePath = string.Empty;
        List<ingredient> productIngredients;

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
            dgReservations.SelectedItem = null;
            selectedReservation = null;
            tabReservation.DataContext = selectedReservation;
        }

        private void btnReservationSave_Click(object sender, RoutedEventArgs e)
        {
            var user = (user)cobReservationUser.SelectedItem;
            var desk = (desk)cobReservationDesk.SelectedItem;

            var reservation = new reservation()
            {
                number_of_guests = int.Parse(tbReservationNumberOfGuests.Text),
                checkin_date = DateTime.Parse(dtpReservationCheckinDate.Text),
                checkout_date = DateTime.Parse(dtpReservationCheckoutDate.Text),
                name = (user != null ? user.name : tbReservationName.Text),
                phone = tbReservationPhone.Text,
                desk_id = desk.id,
                user_id = (user != null ? user.id : null),
                closed = chbReservationClosed.IsChecked.HasValue && chbReservationClosed.IsChecked.Value,
                comment = tbReservationComment.Text
            };
            dbContext.reservations.Add(reservation);
            dbContext.SaveChanges();
            dgReservations.SelectedItem = null;
            selectedReservation = null;
            tabReservation.DataContext = selectedReservation;
        }

        private void btnReservationUpdate_Click(object sender, RoutedEventArgs e)
        {
            dbContext.SaveChanges();
            dgReservations.SelectedItem = null;
            selectedReservation = null;
            tabReservation.DataContext = selectedReservation;
        }

        private void btnReservationDelete_Click(object sender, RoutedEventArgs e)
        {
            dbContext.reservations.Remove(selectedReservation);
            dbContext.SaveChanges();
            dgReservations.SelectedItem = null;
            selectedReservation = null;
            tabReservation.DataContext = selectedReservation;
        }

        private void btnReservationCancel_Click(object sender, RoutedEventArgs e)
        {
            dgReservations.SelectedItem = null;
            selectedReservation = null;
            tabReservation.DataContext = selectedReservation;
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

        private void btnProductSave_Click(object sender, RoutedEventArgs e)
        {
            var image = new image()
            {
                img_name = tbImageName.Text,
                img_data = selectedImage.img_data
            };

            var product = new product()
            {
                name = tbProductName.Text,
                description = tbProductDescription.Text,
                category_id = ulong.Parse(cobProductCategory.SelectedValuePath),
                packing = tbProductPacking.Text,
                price = int.Parse(tbProductPrice.Text),
                is_food = chkProductIsFood.IsChecked.HasValue && chkProductIsFood.IsChecked.Value,
                image_id = image.id,
            };
            dbContext.products.Add(product);
            dbContext.SaveChanges();

            foreach (ListViewItem item in lvProductIngredients.Items)
            {
                ingredient ingredient = item.Tag as ingredient;
                if (ingredient != null)
                {
                    product_ingredient productIngredient = new product_ingredient
                    {
                        product_id = product.id,
                        ingredient_id = ingredient.id
                    };
                    dbContext.product_ingredients.Add(productIngredient);
                }
            }

            dgProducts.SelectedItem = null;
            selectedProduct = null;
            //lvProductIngredients.Items.Clear();
            tabProduct.DataContext = selectedProduct;
        }

        private void btnProductUpdate_Click(object sender, RoutedEventArgs e)
        {
            dbContext.SaveChanges();
        }

        private void btnProductDelete_Click(object sender, RoutedEventArgs e)
        {
            dbContext.products.Remove(selectedProduct);
            dbContext.SaveChanges();
        }

        private void btnProductCancel_Click(object sender, RoutedEventArgs e)
        {
            dgProducts.SelectedItem = null;
            selectedProduct = null;
            tabProduct.DataContext = selectedProduct;
        }

        private void btnProductAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            lvProductIngredients.Items.Add(cobProductIngredients.SelectedItem);
        }

        private void btnProductRemoveIngredient_Click(object sender, RoutedEventArgs e)
        {
            if (lvProductIngredients.SelectedItem != null)
            {
                lvProductIngredients.Items.Remove(lvProductIngredients.SelectedItems[0]);
            }
        }

        private void btnNewImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Képfájlok|*.jpg;*.png;*.bmp|Mindennemű fájl|*.*";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == true)
            {
                imagePath = openFileDialog.FileName;
                string fileName = System.IO.Path.GetFileName(imagePath);
                selectedImage = new image();

                BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath));
                byte[] imageData = ConvertBitmapToByteArray(bitmapImage);
                selectedImage.img_name = fileName;
                selectedImage.img_data = imageData;

                imgImage.Source = bitmapImage;
                tbImageName.Text = fileName;
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

        private void btnDeleteImage_Click(object sender, RoutedEventArgs e)
        {
            tbImageName.Text = string.Empty;
            imgImage.Source = null;
            selectedImage = null;
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

        private void btnCategorySave_Click(object sender, RoutedEventArgs e)
        {
            var category = new category()
            {
                name = tbListsCategoryName.Text,
            };
            dbContext.categories.Add(category);
            dbContext.SaveChanges();
            dgCategories.SelectedItem = null;
            selectedCategory = null;
            spCategory.DataContext = selectedCategory;
        }

        private void btnCategoryUpdate_Click(object sender, RoutedEventArgs e)
        {
            dbContext.SaveChanges();
        }

        private void btnCategoryDelete_Click(object sender, RoutedEventArgs e)
        {
            dbContext.categories.Remove(selectedCategory);
            dbContext.SaveChanges();
        }

        private void btnCategoryCancel_Click(object sender, RoutedEventArgs e)
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

        private void btnIngredientSave_Click(object sender, RoutedEventArgs e)
        {
            var ingredient = new ingredient()
            {
                name = tbListsIngredientName.Text,
            };
            dbContext.ingredients.Add(ingredient);
            dbContext.SaveChanges();

            foreach (ListViewItem item in lvIngredientAllergens.Items)
            {
                allergen allergen = item.Tag as allergen;
                if (allergen != null)
                {
                    ingredient_allergen ingredientAllergen = new ingredient_allergen
                    {
                        ingredient_id = ingredient.id,
                        allergen_id = allergen.id
                    };
                    dbContext.ingredient_allergens.Add(ingredientAllergen);
                }
            }
            dbContext.SaveChanges();
            dgIngredients.SelectedItem = null;
            selectedIngredient = null;
            spIngredient.DataContext = selectedIngredient;
        }

        private void btnIngredientUpdate_Click(object sender, RoutedEventArgs e)
        {
            dbContext.SaveChanges();
        }

        private void btnIngredientDelete_Click(object sender, RoutedEventArgs e)
        {
            dbContext.ingredients.Remove(selectedIngredient);
            dbContext.SaveChanges();
        }

        private void btnIngredientCancel_Click(object sender, RoutedEventArgs e)
        {
            dgIngredients.SelectedItem = null;
            selectedIngredient = null;
            spIngredient.DataContext = selectedIngredient;
        }

        private void btnIngredientAllergenAdd_Click(object sender, RoutedEventArgs e)
        {
            lvIngredientAllergens.Items.Add(cobIngredientAllergens.SelectedItem);
        }

        private void btnIngredientAllergenDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvIngredientAllergens.SelectedItem != null)
            {
                lvIngredientAllergens.Items.Remove(lvIngredientAllergens.SelectedItems[0]);
            }
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

        private void btnAllergenSave_Click(object sender, RoutedEventArgs e)
        {
            var allergen = new allergen()
            {
                name = tbListsAllergenName.Text,
            };
            dbContext.allergens.Add(allergen);
            dbContext.SaveChanges();
            dgAllergens.SelectedItem = null;
            selectedAllergen = null;
            spAllergen.DataContext = selectedAllergen;
        }

        private void btnAllergenUpdate_Click(object sender, RoutedEventArgs e)
        {
            dbContext.SaveChanges();
        }

        private void btnAllergenDelete_Click(object sender, RoutedEventArgs e)
        {
            dbContext.allergens.Remove(selectedAllergen);
            dbContext.SaveChanges();
        }

        private void btnAllergenCancel_Click(object sender, RoutedEventArgs e)
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

        private void btnDeskSave_Click(object sender, RoutedEventArgs e)
        {
            var desk = new desk()
            {
                number_of_seats = int.Parse(tbListsDeskNoS.Text)
            };
            dbContext.desks.Add(desk);
            dbContext.SaveChanges();
            dgDesks.SelectedItem = null;
            selectedDesk = null;
            spDesk.DataContext = selectedDesk;
        }

        private void btnDeskUpdate_Click(object sender, RoutedEventArgs e)
        {
            dbContext.SaveChanges();
        }

        private void btnDeskDelete_Click(object sender, RoutedEventArgs e)
        {
            dbContext.desks.Remove(selectedDesk);
            dbContext.SaveChanges();
        }

        private void btnDeskCancel_Click(object sender, RoutedEventArgs e)
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

        private void btnUserSave_Click(object sender, RoutedEventArgs e)
        {
            var user = new user()
            {
                name = tbUserName.Text,
                email = tbUserEmail.Text,
                password = tbUserPassword.Text,
                phone = tbUserPhone.Text,
                admin = cbUserAdmin.IsChecked.HasValue && cbUserAdmin.IsChecked.Value,
            };
            dbContext.users.Add(user);
            dbContext.SaveChanges();
            dgUsers.SelectedItem = null;
            selectedUser = null;
            tabUser.DataContext = selectedUser;
        }

        private void btnUserUpdate_Click(object sender, RoutedEventArgs e)
        {
            dbContext.SaveChanges();
        }

        private void btnUserDelete_Click(object sender, RoutedEventArgs e)
        {
            dbContext.users.Remove(selectedUser);
            dbContext.SaveChanges();
        }

        private void btnUserCancel_Click(object sender, RoutedEventArgs e)
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