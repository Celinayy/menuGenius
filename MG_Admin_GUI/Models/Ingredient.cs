using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MG_Admin_GUI.Models
{
    public class Ingredient : INotifyPropertyChanged
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


        private ObservableCollection<Allergen> _ingredientAllergens;
        public ObservableCollection<Allergen> ingredientAllergens
        {
            get { return _ingredientAllergens; }
            set
            {
                if (_ingredientAllergens != value)
                {
                    _ingredientAllergens = value;
                    OnPropertyChanged(nameof(ingredientAllergens));
                    UpdateAllergensAsString();
                }
            }
        }

        public Ingredient(MySqlDataReader reader)
        {
            id = reader.GetInt32("id");
            name = reader.GetString("name");
            ingredientAllergens = GetAllergensForIngredient(reader.GetInt32("id"));

        }

        public static ObservableCollection<Ingredient> GetIngredients()
        {
            ObservableCollection<Ingredient> Ingredients = new ObservableCollection<Ingredient>();
            using (var connection = DatabaseHandler.OpenConnection())
            {
                var query = "SELECT * FROM ingredients";

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Ingredients.Add(new Ingredient(reader));
                        }
                    }
                }
            }
            return Ingredients;
        }

        public static ObservableCollection<Allergen> GetAllergensForIngredient(int ingredientId)
        {
            ObservableCollection<Allergen> allergens = new ObservableCollection<Allergen>();
            using (var connection = DatabaseHandler.OpenConnection())
            {
                var query = "SELECT * FROM allergens INNER JOIN ingredient_allergen ON allergens.id = ingredient_allergen.allergen_id WHERE ingredient_allergen.ingredient_id = @ingredientId";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ingredientId", ingredientId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Allergen allergen = new Allergen(reader);
                            allergens.Add(allergen);
                        }
                    }
                }
            }
            return allergens;
        }

        private string _allergensAsString;
        public string allergensAsString
        {
            get { return _allergensAsString; }
            private set
            {
                if (_allergensAsString != value)
                {
                    _allergensAsString = value;
                    OnPropertyChanged(nameof(allergensAsString));
                }
            }
        }

        private void UpdateAllergensAsString()
        {
            allergensAsString = string.Join(", ", ingredientAllergens?.Select(allergen => allergen.name) ?? Enumerable.Empty<string>());
            OnPropertyChanged(nameof(allergensAsString));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
