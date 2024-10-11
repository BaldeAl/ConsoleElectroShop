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

    bool continuer = true;

    while (continuer)
    {
        Console.WriteLine("Que souhaitez-vous faire?");
        Console.WriteLine("1. Ajouter un produit");
        Console.WriteLine("2. Ajouter un client");
        Console.WriteLine("3. Mettre à jour un produit");
        Console.WriteLine("4. Quitter");
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
                continuer = false;
                Console.WriteLine("Fin du programme.");
                break;
            default:
                Console.WriteLine("Choix invalide. Veuillez réessayer.");
                break;
        }
    }
}