using System;
using System.Collections.Generic;

namespace EFCore.Models;

public partial class SalesGroup
{
    public int Id { get; set; }

    public string State { get; set; } = null!;

    public int Type { get; set; }


    public virtual ICollection<Salesperson> salesperson{ get; } = new List<Salesperson>();
}
