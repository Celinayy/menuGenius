using System;
using System.Collections.Generic;

namespace MG_Admin_GUI.Models;

public partial class Ingredient
{
    public ulong Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ProductIngredient> ProductIngredients { get; set; } = new List<ProductIngredient>();


}
