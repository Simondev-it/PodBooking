using System;
using System.Collections.Generic;

namespace LibraryInANutShell.Models;

public partial class Pod
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Image { get; set; }

    public string? Description { get; set; }

    public int? Rating { get; set; }

    public string? Status { get; set; }

    public int TypeId { get; set; }

    public int StoreId { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Slot> Slots { get; set; } = new List<Slot>();

    public virtual Store Store { get; set; } = null!;

    public virtual Type Type { get; set; } = null!;

    public virtual ICollection<Utility> Utilities { get; set; } = new List<Utility>();
}
