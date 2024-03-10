using System;
using System.Collections.Generic;

namespace MG_Admin_GUI.Models;

public partial class product
{
    public ulong id { get; set; }

    public string name { get; set; } = null!;

    public string description { get; set; } = null!;

    public ulong category_id { get; set; }

    public string packing { get; set; } = null!;

    public int price { get; set; }

    public bool is_food { get; set; }

    public ulong image_id { get; set; }

    public DateTime? deleted_at { get; set; }

    public virtual category category { get; set; } = null!;

    public virtual image image { get; set; } = null!;

    public virtual ICollection<product_purchase> product_purchases { get; set; } = new List<product_purchase>();

    public virtual ICollection<product_user> product_users { get; set; } = new List<product_user>();

    public List<ingredient> ingredients { get; } = new List<ingredient>();

    public List<product_ingredient> product_ingredients = new List<product_ingredient>();
}
