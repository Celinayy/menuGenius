using System;
using System.Collections.Generic;

namespace MG_Admin_GUI.Models;

public partial class personal_access_token
{
    public ulong id { get; set; }

    public string tokenable_type { get; set; } = null!;

    public ulong tokenable_id { get; set; }

    public string name { get; set; } = null!;

    public string token { get; set; } = null!;

    public string? abilities { get; set; }

    public DateTime? last_used_at { get; set; }

    public DateTime? expires_at { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }
}
