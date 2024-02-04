using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MG_Admin_GUI.Models;

public partial class IngredientAllergen
{
    [Key]
    public ulong IngredientId { get; set; }
    [Key]
    public ulong AllergenId { get; set; }

    public virtual Allergen Allergen { get; set; } = null!;

    public virtual Ingredient Ingredient { get; set; } = null!;
}
