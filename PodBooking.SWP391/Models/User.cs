 using System;
using System.Collections.Generic;

namespace PodBooking.SWP391.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Name { get; set; }

    public string? Image { get; set; }

    public string? Role { get; set; }

    public string? Type { get; set; }

    public string? PhoneNumber { get; set; }

    public int? Point { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
