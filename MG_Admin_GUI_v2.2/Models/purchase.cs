using System;
using System.Collections.Generic;

namespace MG_Admin_GUI.Models;

public partial class purchase
{
    public ulong id { get; set; }

    public DateTime date_time { get; set; }

    public int total_pay { get; set; }

    public string status { get; set; } = null!;

    public bool paid { get; set; }

    public ulong? user_id { get; set; }

    public ulong desk_id { get; set; }

    public string stripe_id { get; set; } = null!;

    public DateTime? deleted_at { get; set; }

    public virtual desk desk { get; set; } = null!;

    public virtual ICollection<product_purchase> product_purchases { get; set; } = new List<product_purchase>();

    public virtual user? user { get; set; }
}
