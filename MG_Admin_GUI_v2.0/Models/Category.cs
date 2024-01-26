using System;
using System.Collections.Generic;

namespace MG_Admin_GUI.Models;

public partial class Category
{
    public ulong Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
