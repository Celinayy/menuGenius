using System;
using System.Collections.Generic;

namespace MG_Admin_GUI.Models;

public partial class Purchase
{
    public ulong Id { get; set; }

    public DateTime DateTime { get; set; }

    public int TotalPay { get; set; }

    public string Status { get; set; } = null!;

    public bool Paid { get; set; }

    public ulong? UserId { get; set; }

    public ulong DeskId { get; set; }

    public string StripeId { get; set; } = null!;

    public DateTime? DeletedAt { get; set; }

    public virtual Desk Desk { get; set; } = null!;

    public virtual ICollection<ProductPurchase> ProductPurchases { get; set; } = new List<ProductPurchase>();

    public virtual User? User { get; set; }
}
