using System;
using System.Collections.Generic;

namespace EFCore.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal? TotalDue { get; set; }

    public string Status { get; set; } = null!;

    public int CustomerId { get; set; }

    public int SalespersonId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();

    public virtual Salesperson Salesperson { get; set; } = null!;
}
