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

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenImage();
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

    }
}
