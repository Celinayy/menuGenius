
namespace MG_Admin_GUI.Models;

public partial class ingredient
{
    public ulong id { get; set; }

    public string name { get; set; } = null!;

    public DateTime? deleted_at { get; set; }

    public List<product> products { get; } = new List<product>();

    public List<product_ingredient> product_ingredients = new List<product_ingredient>();

    public List<allergen> allergens { get; } = new List<allergen>();

    public List<ingredient_allergen> ingredient_allergens = new List<ingredient_allergen>();

    public override string ToString()
    {
        return string.Join(", ", allergens?.Select(allergen => allergen.name) ?? Enumerable.Empty<string>());
    }

    public string allergensAsString 
    {  
        get {  return string.Join(", ", allergens?.Select(allergen => allergen.name) ?? Enumerable.Empty<string>()); }
    }

    //private void UpdateAllergensAsString()
    //{
    //    allergensAsString = string.Join(", ", allergens?.Select(allergen => allergen.name) ?? Enumerable.Empty<string>());
    //}


}
