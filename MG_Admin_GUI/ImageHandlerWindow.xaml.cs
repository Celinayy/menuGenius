using MG_Admin_GUI.Models;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MG_Admin_GUI
{
    /// <summary>
    /// Interaction logic for ImageHandler.xaml
    /// </summary>
    public partial class ImageHandler : Window
    {
        public ImageViewModel ImageVM { get; set; }
        private Image imageControl = new Image();
        string imagePath = string.Empty;
        //Image selectedImage = null;

        public ImageHandler()
        {
            InitializeComponent();
            ImageVM = new ImageViewModel();

            //OpenConnection();

            imageControl.ImageSourceUpdated += (sender, args) =>
            {
                Dispatcher.Invoke(() =>
                {
                    DataContext = ImageVM;
                    spSelectedImage.DataContext = ImageVM.selectedImage;
                });
            };

            ImageVM.Images = Image.GetImages();

            DataContext = ImageVM;
            //spSelectedImage.DataContext = ImageVM.selectedImage;

        }

        public Image GetReturnImage()
        {
            return ImageVM.selectedImage;
        }

        public void OpenImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Képfájlok|*.jpg;*.png;*.bmp|Mindennemű fájl|*.*";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == true)
            {
                imagePath = openFileDialog.FileName;
                string fileName = System.IO.Path.GetFileName(imagePath);
                ImageVM.selectedImage = new Image();

                BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath));
                byte[] imageData = ConvertBitmapToByteArray(bitmapImage);
                ImageVM.selectedImage.id = ImageVM.Images.Count + 1;
                ImageVM.selectedImage.img_name = fileName;
                ImageVM.selectedImage.img_data = imageData;

                imgImage.Source = bitmapImage;
                //spSelectedImage.DataContext = ImageVM.selectedImage;
            }
        }

        private byte[] ConvertBitmapToByteArray(BitmapImage bitmapImage)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(stream);

                // MemoryStream tartalmát byte array-be konvertáljuk
                return stream.ToArray();
            }
        }

        //private byte[] RetrieveImageDataFromDatabase(int imageId)
        //{
        //    string query = "SELECT imgData FROM Images WHERE id = @imageID";

        //    using (MySqlConnection connection = OpenConnection())
        //    {

        //        using (MySqlCommand command = new MySqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@id", imageId);

        //            using (MySqlDataReader reader = command.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    return (byte[])reader["imgData"];
        //                }
        //            }
        //        }
        //    }

        //    return null;
        //}

        private void dgImages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ImageVM.selectedImage = (Image)dgImages.SelectedItem;
            //spSelectedImage.DataContext = ImageVM.selectedImage;
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenImage();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string saveQuery = null;
                string tableName = "images";
                Dictionary<string, object> parameters = new Dictionary<string, object>();

                saveQuery = "SET GLOBAL max_allowed_packet = 16777216;";
                saveQuery += $"INSERT INTO {tableName}(id, img_name, img_data) VALUES (@id, @img_name, @img_data)";

                parameters.Add("@id", tbImageId.Text);
                parameters.Add("@img_name", ImageVM.selectedImage.img_name);
                parameters.Add("@img_data", ImageVM.selectedImage.img_data);

                ExecuteQuery(saveQuery, parameters);
                ImageVM.selectedImage = null;
                spSelectedImage = null;
                ImageVM.Images = Image.GetImages();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt az adatbázisba mentés közben!" + ex.Message, "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void btnImageSelect_Click(object sender, RoutedEventArgs e)
        {
            ImageVM.selectedImage = (Image)dgImages.SelectedItem;
            DialogResult = true;
            this.Close();
        }

        private void btnImageDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                string deleteQuery = $"DELETE FROM images WHERE id = @id";

                parameters.Add("@id", ImageVM.selectedImage.id);

                ExecuteQuery(deleteQuery, parameters);
                ImageVM.selectedImage = null;
                spSelectedImage = null;
                ImageVM.Images = Image.GetImages();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt a törlés közben!" + ex.Message, "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
