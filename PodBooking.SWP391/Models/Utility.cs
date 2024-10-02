using System;
using System.Collections.Generic;

namespace PodBooking.SWP391.Models;

public partial class Utility
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Image { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Pod> Pods { get; set; } = new List<Pod>();
}
