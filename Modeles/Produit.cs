using System;
using System.Collections.Generic;

namespace ConsoleElectroShop.Modeles;

public partial class Produit
{
    public int Id { get; set; }

    public string? Nom { get; set; }

    public int? Prix { get; set; }

    public int? Stock { get; set; }

    public virtual ICollection<LignesCommande> LignesCommandes { get; set; } = new List<LignesCommande>();

    public virtual Stock? StockNavigation { get; set; }

    public virtual ICollection<Fournisseur> Fournisseurs { get; set; } = new List<Fournisseur>();
}
