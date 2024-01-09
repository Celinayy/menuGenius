using MG_Admin_GUI;
using MG_Admin_GUI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MG_Admin_GUI
{
    public class Image : INotifyPropertyChanged
    {

        private int _id;
        public int id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(id));
                }
            }
        }

        private string _img_name;
        public string img_name
        {
            get { return _img_name; }
            set
            {
                if (_img_name != value)
                {
                    _img_name = value;
                    OnPropertyChanged(nameof(img_name));
                }
            }
        }

        private byte[] _img_data;
        public byte[] img_data
        {
            get { return _img_data; }
            set
            {
                if (_img_data != value)
                {
                    _img_data = value;
                    OnPropertyChanged(nameof(img_data));
                    OnImageSourceUpdated();
                }
            }
        }

        public Image(MySqlDataReader reader)
        {
            id = reader.GetInt32("id");
            img_name = reader.GetString("img_name");
            img_data = (byte[])reader["img_data"];
        }

        public Image()
        {

        }

        public static ObservableCollection<Image> GetImages()
        {
            ObservableCollection<Image> Images = new ObservableCollection<Image>();
            using (var connection = DatabaseHandler.OpenConnection())
            {
                var query = "SELECT * FROM images";

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Images.Add(new Image(reader));
                        }
                    }
                }
            }
            return Images;
        }

        public event EventHandler ImageSourceUpdated;
        public virtual void OnImageSourceUpdated()
        {
            ImageSourceUpdated?.Invoke(this, EventArgs.Empty);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
