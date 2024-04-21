using System;
using System.Collections.Generic;

namespace ShopAPI.Models;

public partial class Product
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Category { get; set; }

    public decimal? Price { get; set; }

    public int? AvailableStock { get; set; }

    public DateOnly? LastUpdateDate { get; set; }

    public Guid? SupplierId { get; set; }

    public Guid? ImageId { get; set; }

    public virtual Image? Image { get; set; }

    public virtual Supplier? Supplier { get; set; }
}
