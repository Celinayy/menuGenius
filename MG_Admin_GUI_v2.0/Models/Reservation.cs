using System;
using System.Collections.Generic;

namespace MG_Admin_GUI.Models;

public partial class Reservation
{
    public ulong Id { get; set; }

    public int NumberOfGuests { get; set; }

    public DateTime CheckinDate { get; set; }

    public DateTime CheckoutDate { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public ulong DeskId { get; set; }

    public ulong? UserId { get; set; }

    public bool Closed { get; set; }

    public virtual Desk Desk { get; set; } = null!;

    public virtual User? User { get; set; }
    public string? Comment { get; set; }
}
