using System;
using System.Collections.Generic;

namespace ShopAPI.Models;

public partial class Supplier
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public Guid? AddressId { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual Address? Address { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
