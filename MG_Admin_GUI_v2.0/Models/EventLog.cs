using System;
using System.Collections.Generic;

namespace MG_Admin_GUI.Models;

public partial class EventLog
{
    public ulong Id { get; set; }

    public string EventType { get; set; } = null!;

    public string AffectedTable { get; set; } = null!;

    public int AffectedId { get; set; }

    public string Event { get; set; } = null!;

    public DateTime Date { get; set; }

    public ulong UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
