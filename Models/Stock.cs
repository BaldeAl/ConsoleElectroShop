using System;
using System.Collections.Generic;

namespace ConsoleElectroShop.Models;

public partial class Stock
{
    public int ProduitId { get; set; }

    public int? QuantiteStock { get; set; }

    public virtual Produit Produit { get; set; } = null!;
}
