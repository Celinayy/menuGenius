using System;
using System.Collections.Generic;

namespace MG_Admin_GUI.Models;

public partial class ProductUser
{
    public ulong Id { get; set; }

    public ulong ProductId { get; set; }

    public ulong UserId { get; set; }

    public bool Favorite { get; set; }

    public int Stars { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
