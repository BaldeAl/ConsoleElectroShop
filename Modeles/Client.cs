using System;
using System.Collections.Generic;

namespace ConsoleElectroShop.Modeles;

public partial class Client
{
    public int Id { get; set; }

    public string? Nom { get; set; }

    public string? Adresse { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();
}
