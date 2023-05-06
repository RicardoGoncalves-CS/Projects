using System;
using System.Collections.Generic;

namespace NorthwindAPI.Data;

public partial class ProductsAboveAveragePrice
{
    public string ProductName { get; set; } = null!;

    public decimal? UnitPrice { get; set; }
}
