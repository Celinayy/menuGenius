using System;
using System.Collections.Generic;

namespace MG_Admin_GUI.Models;

public partial class Allergen
{
    public ulong Id { get; set; }

    public decimal Code { get; set; }

    public string Name { get; set; } = null!;
}
