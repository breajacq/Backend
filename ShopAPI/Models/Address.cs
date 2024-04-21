using System;
using System.Collections.Generic;

namespace ShopAPI.Models;

public partial class Address
{
    public Guid Id { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
}
