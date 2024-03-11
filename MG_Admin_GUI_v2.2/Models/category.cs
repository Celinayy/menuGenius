using System;
using System.Collections.Generic;

namespace MG_Admin_GUI.Models;

public partial class category
{
    public ulong id { get; set; }

    public string name { get; set; } = null!;

    public DateTime? deleted_at { get; set; }

    public virtual ICollection<product> products { get; set; } = new List<product>();
}
