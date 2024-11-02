using System;
using System.Collections.Generic;

namespace LibraryInANutShell.Models;

public partial class Booking
{
    public int Id { get; set; }

    public DateOnly? Date { get; set; }

    public string? Status { get; set; }

    public string? Feedback { get; set; }

    public int PodId { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<BookingOrder> BookingOrders { get; set; } = new List<BookingOrder>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Pod Pod { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Slot> Slots { get; set; } = new List<Slot>();
}
