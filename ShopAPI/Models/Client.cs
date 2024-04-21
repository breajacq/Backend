using System;
using System.Collections.Generic;

namespace ShopAPI.Models;

public partial class Client
{
    public Guid Id { get; set; }

    public string? ClientName { get; set; }

    public string? ClientSurname { get; set; }

    public DateOnly? Birthday { get; set; }

    public char? Gender { get; set; }

    public DateOnly? RegistrationDate { get; set; }

    public Guid? AddressId { get; set; }

    public virtual Address? Address { get; set; }
}
