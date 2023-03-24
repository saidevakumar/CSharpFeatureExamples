using System;
using System.Collections.Generic;

namespace EFCore.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Zipcode { get; set; }

    //public string? CmpLastFirst { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
