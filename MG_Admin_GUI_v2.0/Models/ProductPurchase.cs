using System;
using System.Collections.Generic;

namespace MG_Admin_GUI.Models;

public partial class ProductPurchase
{
    public ulong Id { get; set; }

    public ulong ProductId { get; set; }

    public ulong PurchaseId { get; set; }

    public int Quantity { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Purchase Purchase { get; set; } = null!;
}
