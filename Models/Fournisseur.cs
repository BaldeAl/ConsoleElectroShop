using System;
using System.Collections.Generic;

namespace ConsoleElectroShop.Models;

public partial class Fournisseur
{
    public int Id { get; set; }

    public string? Nom { get; set; }

    public string? Contact { get; set; }

    public virtual ICollection<Produit> Produits { get; set; } = new List<Produit>();
}
