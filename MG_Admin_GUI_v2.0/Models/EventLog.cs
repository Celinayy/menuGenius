using System;
using System.Collections.Generic;

namespace MG_Admin_GUI.Models;

public partial class EventLog
{
    public ulong Id { get; set; }

    public string EventType { get; set; } = null!;

    public string Route { get; set; } = null!;

    public string? Body { get; set; }

    public DateTime DateTime { get; set; }
    public DateTime? DeletedAt { get; set; }

    public ulong? UserId { get; set; }

    public virtual User? User { get; set; } = null!;
}
