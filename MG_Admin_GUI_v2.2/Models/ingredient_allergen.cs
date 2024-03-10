using System;
using System.Collections.Generic;

namespace MG_Admin_GUI.Models;

public partial class ingredient_allergen
{
    public ulong ingredient_id { get; set; }

    public ulong allergen_id { get; set; }

    public DateTime? deleted_at { get; set; }

    public virtual allergen allergen { get; set; } = null!;

    public virtual ingredient ingredient { get; set; } = null!;
}
