
namespace MG_Admin_GUI.Models;

public partial class reservation
{
    public ulong id { get; set; }

    public int number_of_guests { get; set; }

    public DateTime checkin_date { get; set; }

    public DateTime checkout_date { get; set; }

    public string name { get; set; } = null!;

    public string phone { get; set; } = null!;

    public ulong desk_id { get; set; }

    public ulong? user_id { get; set; }

    public bool closed { get; set; }

    public string? comment { get; set; }

    public DateTime? deleted_at { get; set; }

    public virtual desk desk { get; set; } = null!;

    public virtual user? user { get; set; }
}
