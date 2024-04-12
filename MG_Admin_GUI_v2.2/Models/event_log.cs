
namespace MG_Admin_GUI.Models;

public partial class event_log
{
    public ulong id { get; set; }

    public string event_type { get; set; } = null!;

    public ulong? user_id { get; set; }

    public string route { get; set; } = null!;

    public string body { get; set; } = null!;

    public DateTime date_time { get; set; }

    public DateTime? deleted_at { get; set; }

    public virtual user? user { get; set; }
}
