using System;
using System.Collections.Generic;

namespace MG_Admin_GUI.Models;

public partial class desk
{
    public ulong id { get; set; }

    public int number_of_seats { get; set; }

    public DateTime? deleted_at { get; set; }

    public virtual ICollection<purchase> purchases { get; set; } = new List<purchase>();

    public virtual ICollection<reservation> reservations { get; set; } = new List<reservation>();

    public override string ToString()
    {
        return $"asztalszám: {id} - ülőhely: {number_of_seats}";
    }
}
