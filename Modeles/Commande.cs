using System;
using System.Collections.Generic;

namespace ConsoleElectroShop.Modeles;

public partial class Commande
{
    public int Id { get; set; }

    public DateTime? Date { get; set; }

    public string? Statut { get; set; }

    public int? ClientId { get; set; }

    public virtual Client? Client { get; set; }

    public virtual ICollection<LignesCommande> LignesCommandes { get; set; } = new List<LignesCommande>();
}
