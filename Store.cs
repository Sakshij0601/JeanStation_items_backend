using System;
using System.Collections.Generic;

namespace JeanStationapp.Models;

public partial class Store
{
    public int StoreId { get; set; }

    public string? StoreName { get; set; }

    public string? Location { get; set; }

    public string? StPhoneNo { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
