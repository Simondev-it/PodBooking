using System;
using System.Collections.Generic;

namespace LibraryInANutShell.Models;

public partial class Type
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Capacity { get; set; }

    public virtual ICollection<Pod> Pods { get; set; } = new List<Pod>();
}
