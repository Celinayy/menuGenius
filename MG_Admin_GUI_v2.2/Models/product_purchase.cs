
namespace MG_Admin_GUI.Models;

public partial class product_purchase
{
    public ulong id { get; set; }

    public ulong product_id { get; set; }

    public ulong purchase_id { get; set; }

    public int quantity { get; set; }

    public DateTime? deleted_at { get; set; }

    public virtual product product { get; set; } = null!;

    public virtual purchase purchase { get; set; } = null!;
}
