using System;
using System.Collections.Generic;

namespace Alza.DbContexts.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string ImgUri { get; set; } = null!;

    public decimal Price { get; set; }

    public string? Description { get; set; }
}
