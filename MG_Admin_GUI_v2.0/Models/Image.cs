using System;
using System.Collections.Generic;

namespace MG_Admin_GUI.Models;

public partial class Image
{
    public ulong Id { get; set; }

    public string ImgName { get; set; } = null!;

    public byte[]? ImgData { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
