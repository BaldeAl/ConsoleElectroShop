// See https://aka.ms/new-console-template for more information
using ConsoleElectroShop.Modeles;
using ConsoleElectroShop.Services;
using Microsoft.EntityFrameworkCore;

var optionsBuilder = new DbContextOptionsBuilder<DbElectroShopContext>();
optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\balde\\source\\repos\\ConsoleElectroShop\\Data\\DbElectroShop.mdf;Database=DbElectroShop;Integrated Security=True;");

using (var context = new DbElectroShopContext(optionsBuilder.Options))
{
    var produitService = new ProduitService(context);
    var clientService = new ClientService(context);
    var commandeService = new CommandeService(context);

    bool continuer = true;

    while (continuer)
    {
        Console.WriteLine("Que souhaitez-vous faire?");
        Console.WriteLine("1. Ajouter un produit");
        Console.WriteLine("2. Ajouter un client");
        Console.WriteLine("3. Mettre à jour un produit");
        Console.WriteLine("4. Passer une commande");
        Console.WriteLine("5. Afficher une commande avec Lazy Loading");
        Console.WriteLine("6. Afficher une commande avec Eager Loading");
        Console.WriteLine("7. Quitter");
        Console.Write("Choisissez une option: ");

        string choix = Console.ReadLine();

        switch (choix)
        {
            case "1":
                produitService.AjouterProduit();
                break;
            case "2":
                clientService.AjouterClient();
                break;
            case "3":
                produitService.MettreAJourProduit();
                break;
            case "4":
                // Saisie du client et des produits pour la commande
                Console.Write("Entrez l'ID du client: ");
                int clientId = Convert.ToInt32(Console.ReadLine());

                var produitsCommandes = new Dictionary<int, int>();
                bool ajouterProduits = true;

                while (ajouterProduits)
                {
                    Console.Write("Entrez l'ID du produit: ");
                    int produitId = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Entrez la quantité: ");
                    int quantite = Convert.ToInt32(Console.ReadLine());

                    produitsCommandes.Add(produitId, quantite);

                    Console.Write("Ajouter un autre produit à la commande ? (o/n): ");
                    ajouterProduits = Console.ReadLine().ToLower() == "o";
                }

                // Passer la commande
                commandeService.PasserCommande(clientId, produitsCommandes);
                break;
            case "5":
                Console.Write("Entrez l'ID de la commande : ");
                int commandeIdLazy = Convert.ToInt32(Console.ReadLine());
                commandeService.TesterLazyLoading(commandeIdLazy);
                break;
            case "6":
                Console.Write("Entrez l'ID de la commande : ");
                int commandeIdEager = Convert.ToInt32(Console.ReadLine());
                commandeService.TesterEagerLoading(commandeIdEager);
                break;
            case "7":
                continuer = false;
                Console.WriteLine("Fin du programme.");
                break;
            default:
                Console.WriteLine("Choix invalide. Veuillez réessayer.");
                break;
        }
    }
}