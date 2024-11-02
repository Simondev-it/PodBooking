using System;
using System.Collections.Generic;

namespace LibraryInANutShell.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Price { get; set; }

    public string? Description { get; set; }

    public int? Rating { get; set; }

    public int? Stock { get; set; }

    public int StoreId { get; set; }

    public int CategoryId { get; set; }

    public virtual ICollection<BookingOrder> BookingOrders { get; set; } = new List<BookingOrder>();

    public virtual Category Category { get; set; } = null!;

    public virtual Store Store { get; set; } = null!;
}
