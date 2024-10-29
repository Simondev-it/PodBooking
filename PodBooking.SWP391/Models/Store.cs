using System;
using System.Collections.Generic;

namespace PodBooking.SWP391.Models;

public partial class Store
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }
    public string? Image { get; set; }
    public string? Status { get; set; }

    public string? Contact { get; set; }

    public virtual ICollection<Pod> Pods { get; set; } = new List<Pod>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
