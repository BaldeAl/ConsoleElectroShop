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
        Console.WriteLine("***********************************************");
        Console.WriteLine("Que souhaitez-vous faire?");
        Console.WriteLine("1. Ajouter un produit");
        Console.WriteLine("2. Ajouter un client");
        Console.WriteLine("3. Mettre à jour un produit");
        Console.WriteLine("4. Passer une commande");
        Console.WriteLine("5. Afficher une commande avec Lazy Loading");
        Console.WriteLine("6. Afficher une commande avec Eager Loading");
        Console.WriteLine("7. Afficher les commandes d'un client (Procédure stockée)");
        Console.WriteLine("8. Passer une commande via procédure stockée");
        Console.WriteLine("9. Afficher les commandes avec leur total (Vue)");
        Console.WriteLine("10. Afficher tout les produits ");
        Console.WriteLine("11. Afficher tout les clients ");
        Console.WriteLine("12. Quitter");
        Console.WriteLine("***********************************************");
        Console.Write("Choisissez une option: ");
        

        string choix = Console.ReadLine();
        Console.Clear();

        switch (choix)
        {
            case "1":
                Console.WriteLine("----------------------------------------");
                produitService.AjouterProduit();
                break;
            case "2":
                Console.WriteLine("----------------------------------------");
                clientService.AjouterClient();
                break;
            case "3":
                Console.WriteLine("----------------------------------------");
                produitService.MettreAJourProduit();
                break;
            case "4":
                Console.WriteLine("----------------------------------------");
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
                Console.WriteLine("----------------------------------------");
                Console.Write("Entrez l'ID de la commande : ");
                int commandeIdLazy = Convert.ToInt32(Console.ReadLine());
                commandeService.TesterLazyLoading(commandeIdLazy);
                break;
            case "6":
                Console.WriteLine("----------------------------------------");
                Console.Write("Entrez l'ID de la commande : ");
                int commandeIdEager = Convert.ToInt32(Console.ReadLine());
                commandeService.TesterEagerLoading(commandeIdEager);
                break;
            case "7":
                Console.WriteLine("----------------------------------------");
                // Appel de la procédure stockée pour récupérer les commandes d'un client
                Console.Write("Entrez l'ID du client pour voir ses commandes: ");
                int clientIdProc = Convert.ToInt32(Console.ReadLine());
                commandeService.GetCommandesByClient(clientIdProc);
                break;
            case "8":
                Console.WriteLine("----------------------------------------");

                Console.Write("Entrez l'ID du client: ");
                int client_Id = Convert.ToInt32(Console.ReadLine());

                Console.Write("Entrez le statut de la commande: ");
                string statut = Console.ReadLine();

                var produits_Commandes = new Dictionary<int, int>();
                bool ajouter_Produits = true;

                while (ajouter_Produits)
                {
                    Console.Write("Entrez l'ID du produit: ");
                    int produitId = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Entrez la quantité: ");
                    int quantite = Convert.ToInt32(Console.ReadLine());

                    produits_Commandes.Add(produitId, quantite);

                    Console.Write("Ajouter un autre produit à la commande ? (o/n): ");
                    ajouter_Produits = Console.ReadLine().ToLower() == "o";
                }
                commandeService.AddCommandeWithLignes(client_Id, produits_Commandes, statut);
                break;
            case "9":
                // Afficher les commandes avec leur total (via la vue)
                Console.WriteLine("----------------------------------------");
                commandeService.GetCommandesAvecTotal();
                break;
            case "10":
                Console.WriteLine("----------------------------------------");
                produitService.AfficherLesProduits();
                break;
            case "11":
                Console.WriteLine("----------------------------------------");
                clientService.AfficherLesClients();
                break;
            case "12":
                continuer = false;
                Console.WriteLine("Fin du programme.");
                break;
            default:
                Console.WriteLine("Choix invalide. Veuillez réessayer.");
                break;
        }
        if (continuer)
        {
            Console.WriteLine("***********************************************");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
            Console.Clear(); // Effacer l'écran avant d'afficher le menu à nouveau
        }

    }
}