using System;
using System.Collections.Generic;

namespace PodBooking.SWP391.Models;

public partial class Slot
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? StartTime { get; set; }

    public int? EndTime { get; set; }

    public int? Price { get; set; }

    public string? Status { get; set; }

    public int PodId { get; set; }

    public virtual Pod Pod { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
