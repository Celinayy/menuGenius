using System;
using System.Collections.Generic;

namespace MG_Admin_GUI.Models;

public partial class password_reset_token
{
    public string email { get; set; } = null!;

    public string token { get; set; } = null!;

    public DateTime? created_at { get; set; }
}
