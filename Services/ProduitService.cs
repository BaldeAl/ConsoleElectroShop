using ConsoleElectroShop.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleElectroShop.Services
{
    public class ProduitService
    {
        private readonly DbElectroShopContext _context;

        public ProduitService(DbElectroShopContext context)
        {
            _context = context;
        }

        public void AjouterProduit()
        {
            try {
                Console.Write("Entrez le nom du produit: ");
                string nom = Console.ReadLine();

                if (string.IsNullOrEmpty(nom))
                {
                    throw new Exception("Le nom du produit ne peut pas être vide.");
                }

                Console.Write("Entrez le prix du produit: ");
                int prix = Convert.ToInt32(Console.ReadLine());
                if (prix <= 0)
                {
                    throw new Exception("Le prix du produit doit être supérieur à 0.");
                }

                Console.Write("Entrez la quantité en stock: ");
                int stock = Convert.ToInt32(Console.ReadLine());

                var produit = new Produit
                {
                    Nom = nom,
                    Prix = prix,
                    Stock = stock
                };

                _context.Produits.Add(produit);
                _context.SaveChanges();
                Console.WriteLine($"Produit '{nom}' ajouté avec succès.");
            }
        catch (FormatException fe)
                {
                    Console.WriteLine($"Erreur de format : {fe.Message}. Veuillez entrer des données valides.");
                }
        catch (Exception ex)
            {
                    Console.WriteLine($"Erreur lors de l'ajout du produit : {ex.Message}");
            }

        }


        // Méthode pour mettre à jour un produit existant
        public void MettreAJourProduit()
        {
            Console.Write("Entrez l'ID du produit à mettre à jour: ");
            int produitId = Convert.ToInt32(Console.ReadLine());

            // Recherche du produit dans la base de données
            var produit = _context.Produits.Find(produitId);

            if (produit != null)
            {
                Console.WriteLine($"Produit trouvé: {produit.Nom}, Prix: {produit.Prix}, Stock: {produit.Stock}");

                Console.Write("Entrez le nouveau prix (ou appuyez sur Entrée pour conserver l'actuel): ");
                string prixInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(prixInput))
                {
                    produit.Prix = Convert.ToInt32(prixInput);
                }

                Console.Write("Entrez la nouvelle quantité en stock (ou appuyez sur Entrée pour conserver l'actuelle): ");
                string stockInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(stockInput))
                {
                    produit.Stock = Convert.ToInt32(stockInput);
                }

                // Sauvegarder les changements dans la base de données
                _context.SaveChanges();
                Console.WriteLine($"Le produit '{produit.Nom}' a été mis à jour avec succès.");
            }
            else
            {
                Console.WriteLine("Produit non trouvé.");
            }
        }

        //fonction pour afficher tout les produits
        public void AfficherLesProduits()
        {
            var produits = _context.Produits.ToList();

            if (produits.Any())
            {
                Console.WriteLine("Liste des produits :");
                foreach (var produit in produits)
                {
                    Console.WriteLine($"ID: {produit.Id}, Nom: {produit.Nom}, Prix: {produit.Prix}, Stock: {produit.Stock}");
                }
            }
            else
            {
                Console.WriteLine("Aucun produit trouvé.");
            }
        }

    }

}
