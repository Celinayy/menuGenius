using System;
using System.Collections.Generic;

namespace MG_Admin_GUI.Models;

public partial class Desk
{
    public ulong Id { get; set; }

    public int NumberOfSeats { get; set; }

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
