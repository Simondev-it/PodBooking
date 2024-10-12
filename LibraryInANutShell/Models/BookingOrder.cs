﻿using System;
using System.Collections.Generic;

namespace LibraryInANutShell.Models;

public partial class BookingOrder
{
    public int Id { get; set; }

    public int? Amount { get; set; }

    public int? Quantity { get; set; }

    public string? Status { get; set; }

    public DateOnly? Date { get; set; }

    public int BookingId { get; set; }

    public int ProductId { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}