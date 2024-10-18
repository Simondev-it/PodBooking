﻿using System;
using System.Collections.Generic;

namespace LibraryInANutShell.Models;

public partial class Payment
{
    public int Id { get; set; }

    public string? Method { get; set; }

    public int? Amount { get; set; }

    public DateOnly? Date { get; set; }

    public string? Status { get; set; }

    public int BookingId { get; set; }

    public virtual Booking Booking { get; set; } = null!;
}