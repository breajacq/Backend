using System;
using System.Collections.Generic;

namespace ShopAPI.Models;

public partial class Image
{
    public Guid Id { get; set; }

    public byte[]? Image1 { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
