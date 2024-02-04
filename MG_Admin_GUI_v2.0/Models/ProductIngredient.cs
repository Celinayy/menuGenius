using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MG_Admin_GUI.Models;

public partial class ProductIngredient
{
    [Key]
    public ulong ProductId { get; set; }

    [Key]
    public ulong IngredientId { get; set; }

    public virtual Ingredient Ingredient { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
