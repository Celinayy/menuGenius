
namespace MG_Admin_GUI.Models;

public partial class product_user
{
    public ulong id { get; set; }

    public ulong product_id { get; set; }

    public ulong user_id { get; set; }

    public bool favorite { get; set; }

    public int stars { get; set; }

    public DateTime? deleted_at { get; set; }

    public virtual product product { get; set; } = null!;

    public virtual user user { get; set; } = null!;
}
