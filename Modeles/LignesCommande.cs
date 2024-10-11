using System;
using System.Collections.Generic;

namespace ConsoleElectroShop.Modeles;

public partial class LignesCommande
{
    public int Id { get; set; }

    public int? CommandeId { get; set; }

    public int? ProduitId { get; set; }

    public int? Quantite { get; set; }

    public int? PrixUnitaire { get; set; }

    public virtual Commande? Commande { get; set; }

    public virtual Produit? Produit { get; set; }
}
