
namespace MG_Admin_GUI.Models;

public partial class allergen
{
    public ulong id { get; set; }

    public decimal code { get; set; }

    public string name { get; set; } = null!;

    public DateTime? deleted_at { get; set; }

    public List<ingredient> ingredients { get; } = new List<ingredient>();

    public List<ingredient_allergen> ingredient_allergens = new List<ingredient_allergen>();

    public override string ToString()
    {
        return name;
    }

    public override bool Equals(object? obj)
    {
        return obj is allergen allergen && id == allergen.id;
    }
}
