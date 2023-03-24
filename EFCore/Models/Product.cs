using System;
using System.Collections.Generic;

namespace EFCore.Models;

public partial class Product
{
    public string ProductId { get; set; } = null!;

    public string? ProductName { get; set; }

    public int? Size { get; set; }

    public string? Variety { get; set; }

    public decimal? Price { get; set; }

    public string? Status { get; set; }

    //public bool Perishable { get; set; }

    //public int? ExpirationDays { get; set; }

    //public bool? Refrigerated { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();
}
