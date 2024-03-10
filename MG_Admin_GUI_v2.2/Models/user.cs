using System;
using System.Collections.Generic;

namespace MG_Admin_GUI.Models;

public partial class user
{
    public ulong id { get; set; }

    public string name { get; set; } = null!;

    public string email { get; set; } = null!;

    public DateTime? email_verified_at { get; set; }

    public string password { get; set; } = null!;

    public string phone { get; set; } = null!;

    public bool admin { get; set; }

    public string? remember_token { get; set; }

    public DateTime? deleted_at { get; set; }

    public virtual ICollection<event_log> event_logs { get; set; } = new List<event_log>();

    public virtual ICollection<product_user> product_users { get; set; } = new List<product_user>();

    public virtual ICollection<purchase> purchases { get; set; } = new List<purchase>();

    public virtual ICollection<reservation> reservations { get; set; } = new List<reservation>();
}
