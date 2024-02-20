using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace MG_Admin_GUI.Models;

public partial class Product
{
    public ulong Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public ulong CategoryId { get; set; }

    public string Packing { get; set; } = null!;

    public int Price { get; set; }

    public bool IsFood { get; set; }

    public ulong ImageId { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Image Image { get; set; } = null!;

    public virtual ICollection<ProductPurchase> ProductPurchases { get; set; } = new List<ProductPurchase>();

    public virtual ICollection<ProductUser> ProductUsers { get; set; } = new List<ProductUser>();

}
