using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;
using MG_Admin_GUI.ViewModels;

namespace MG_Admin_GUI.Models
{
    public class Product : INotifyPropertyChanged
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

        private string _name;
        public string name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(name));
                }
            }
        }

        private string _description;
        public string description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(description));
                }
            }
        }

        private Category _productCategory;
        public Category productCategory
        {
            get { return _productCategory; }
            set
            {
                if (_productCategory != value)
                {
                    _productCategory = value;
                    OnPropertyChanged(nameof(productCategory));
                }
            }
        }

        private string _packing;
        public string packing
        {
            get { return _packing; }
            set
            {
                if (_packing != value)
                {
                    _packing = value;
                    OnPropertyChanged(nameof(packing));
                }
            }
        }

        private int _price;
        public int price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPropertyChanged(nameof(price));
                }
            }
        }

        private bool _is_food;
        public bool is_food
        {
            get { return _is_food; }
            set
            {
                if (_is_food != value)
                {
                    _is_food = value;
                    OnPropertyChanged(nameof(is_food));
                }
            }
        }

        private Image _productImage;
        public Image productImage
        {
            get { return _productImage; }
            set
            {
                if (_productImage != value)
                {
                    _productImage = value;
                    OnPropertyChanged(nameof(productImage));
                }
            }
        }

        private ObservableCollection<Ingredient> _productIngredients;
        public ObservableCollection<Ingredient> productIngredients
        {
            get { return _productIngredients; }
            set
            {
                if (value != _productIngredients)
                {
                    _productIngredients = value;
                    OnPropertyChanged(nameof(productIngredients));
                    UpdateIngredientsAsString();
                }
            }
        }

        public Product(MySqlDataReader reader)
        {
            id = reader.GetInt32("id");
            name = reader.GetString("name");
            description = reader.GetString("description");
            productCategory = GetCategoryForProduct(reader.GetInt32("category_id"));
            packing = reader.GetString("packing");
            price = reader.GetInt32("price");
            is_food = reader.GetBoolean("is_food");
            productImage = GetImageForProduct(reader.GetInt32("image_id"));
            productIngredients = GetIngredientForProduct(reader.GetInt32("id"));
        }

        public Product() { }

        public CategoryViewModel CategoryVM => new CategoryViewModel();
        public ImageViewModel ImageVM => new ImageViewModel();


        public static ObservableCollection<Product> GetProducts()
        {
            ObservableCollection<Product> Products = new ObservableCollection<Product>();
            using (var connection = DatabaseHandler.OpenConnection())
            {
                var query = "SELECT * FROM products";

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Products.Add(new Product(reader));
                        }
                    }
                }
            }
            return Products;
        }

        public Category GetCategoryForProduct(int categoryId)
        {
            return CategoryVM.Categories.FirstOrDefault(category => category.id == categoryId);
        }

        public Image GetImageForProduct(int imageId)
        {
            return ImageVM.Images.FirstOrDefault(image => image.id == imageId);
        }


        public static ObservableCollection<Ingredient> GetIngredientForProduct(int productId)
        {
            ObservableCollection<Ingredient> ingredients = new ObservableCollection<Ingredient>();
            using (var connection = DatabaseHandler.OpenConnection())
            {
                var query = "SELECT i.* FROM ingredients i " +
                               "JOIN product_ingredient pi ON i.id = pi.ingredient_id " +
                               "WHERE pi.product_id = @productId";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@productId", productId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ingredients.Add(new Ingredient(reader));
                        }
                    }
                }
            }
            return ingredients;
        }

        private string _productIngredientsAsString;
        public string productIngredientsAsString
        {
            get { return _productIngredientsAsString; }
            private set
            {
                if (_productIngredientsAsString != value)
                {
                    _productIngredientsAsString = value;
                    OnPropertyChanged(nameof(productIngredientsAsString));
                }
            }
        }

        //string imageFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");


        private void UpdateIngredientsAsString()
        {
            productIngredientsAsString = string.Join(", ", productIngredients?.Select(ingredient => ingredient.name) ?? Enumerable.Empty<string>());
            OnPropertyChanged(nameof(productIngredientsAsString));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
