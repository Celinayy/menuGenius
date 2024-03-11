using System;
using System.Collections.Generic;

namespace MG_Admin_GUI.Models;

public partial class product_ingredient
{
    public ulong product_id { get; set; }

    public ulong ingredient_id { get; set; }

    public DateTime? deleted_at { get; set; }

    public virtual ingredient ingredient { get; set; } = null!;

    public virtual product product { get; set; } = null!;
}
