using System;
using System.Collections.Generic;

namespace EFCore.Models;

public partial class Salesperson
{
    public int SalespersonId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    //public string? Address { get; set; }

    //public string? City { get; set; }

    //public string? State { get; set; }

    //public string? Zipcode { get; set; }

    public string SalesGroupState { get; set; } = null!;

    public int SalesGroupType { get; set; }

    public string FullName { get {return FirstName + " " + LastName;}}

    public SalesGroup SalesGroup { get; set; }
    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
