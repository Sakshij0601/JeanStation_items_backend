using System;
using System.Collections.Generic;

namespace JeanStationapp.Models;

public partial class Item
{
    public string ItemCode { get; set; } = null!;

    public string ItemName { get; set; } = null!;

    public double? Price { get; set; }

    public int? Qty { get; set; }

    public int? StoreId { get; set; }

    public string? ItemImage { get; set; }

    public virtual Store? Store { get; set; }
}
