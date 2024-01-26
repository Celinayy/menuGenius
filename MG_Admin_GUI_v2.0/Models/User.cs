using System;
using System.Collections.Generic;

namespace MG_Admin_GUI.Models;

public partial class User
{
    public ulong Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime? EmailVerifiedAt { get; set; }

    public string Password { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public bool Admin { get; set; }

    public string? RememberToken { get; set; }

    public virtual ICollection<EventLog> EventLogs { get; set; } = new List<EventLog>();

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
