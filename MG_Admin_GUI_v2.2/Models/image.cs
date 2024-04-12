
namespace MG_Admin_GUI.Models;

public partial class image
{
    public ulong id { get; set; }

    public string img_name { get; set; } = null!;

    public DateTime? deleted_at { get; set; }

    public byte[]? img_data { get; set; }

    public virtual ICollection<product> products { get; set; } = new List<product>();
}
