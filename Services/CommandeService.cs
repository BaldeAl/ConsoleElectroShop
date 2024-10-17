using ConsoleElectroShop.Modeles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleElectroShop.Services
{
    public class CommandeService
    {
        private readonly DbElectroShopContext _context;

        public CommandeService(DbElectroShopContext context)
        {
            _context = context;
        }

        public void PasserCommande(int clientId, Dictionary<int, int> produitsCommandes)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Créer la commande
                    var commande = new Commande
                    {
                        ClientId = clientId,
                        Date = DateTime.Now,
                        Statut = "En cours"
                    };

                    _context.Commandes.Add(commande);
                    _context.SaveChanges(); // Sauvegarde initiale pour obtenir l'ID de la commande

                    // Parcourir les produits commandés (produitId, quantité)
                    foreach (var produitCommande in produitsCommandes)
                    {
                        int produitId = produitCommande.Key;
                        int quantite = produitCommande.Value;

                        // Récupérer le produit
                        var produit = _context.Produits.Find(produitId);
                        if (produit == null)
                        {
                            throw new Exception($"Le produit avec l'ID {produitId} n'existe pas.");
                        }

                        // Vérifier le stock disponible
                        if (produit.Stock < quantite)
                        {
                            throw new Exception($"Le produit '{produit.Nom}' n'a pas assez de stock pour la quantité demandée.");
                        }

                        // Réduire le stock
                        produit.Stock -= quantite;
                        _context.SaveChanges();

                        // Ajouter une ligne de commande
                        var ligneCommande = new LignesCommande
                        {
                            CommandeId = commande.Id,
                            ProduitId = produitId,
                            Quantite = quantite,
                            PrixUnitaire = produit.Prix
                        };

                        _context.LignesCommandes.Add(ligneCommande);
                        _context.SaveChanges();
                    }

                    // Tout est correct, on confirme la transaction
                    transaction.Commit();
                    Console.WriteLine("Commande passée avec succès.");
                }
                catch (Exception ex)
                {
                    // En cas d'erreur, on annule la transaction
                    transaction.Rollback();
                    Console.WriteLine($"Erreur lors du passage de la commande : {ex.Message}");
                }
            }
        }

        public void TesterLazyLoading(int commandeId)
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            var commande = _context.Commandes.FirstOrDefault(c => c.Id == commandeId);
            Console.WriteLine($"Commande ID: {commande.Id}, Date: {commande.Date}");

            foreach (var ligne in commande.LignesCommandes)
            {
                Console.WriteLine($"Produit ID = {ligne.ProduitId}, Quantité = {ligne.Quantite}");
            }

            stopwatch.Stop();
            Console.WriteLine($"Temps Lazy Loading: {stopwatch.ElapsedMilliseconds} ms");
        }

        public void TesterEagerLoading(int commandeId)
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            var commande = _context.Commandes
                .Include(c => c.LignesCommandes)
                .FirstOrDefault(c => c.Id == commandeId);

            Console.WriteLine($"Commande ID: {commande.Id}, Date: {commande.Date}");

            foreach (var ligne in commande.LignesCommandes)
            {
                Console.WriteLine($"Produit ID = {ligne.ProduitId}, Quantité = {ligne.Quantite}");
            }

            stopwatch.Stop();
            Console.WriteLine($"Temps Eager Loading: {stopwatch.ElapsedMilliseconds} ms");
        }


        // Méthode pour afficher toutes les commandes
        public void AfficherToutesLesCommandes()
        {
            var commandes = _context.Commandes.ToList();

            if (!commandes.Any())
            {
                Console.WriteLine("Aucune commande n'a été trouvée.");
                return;
            }

            foreach (var commande in commandes)
            {
                Console.WriteLine($"\nCommande ID = {commande.Id}, Date = {commande.Date}, Statut = {commande.Statut}");
                foreach (var ligne in commande.LignesCommandes)
                {
                    Console.WriteLine($"\tProduit ID = {ligne.ProduitId}, Quantité = {ligne.Quantite}, Prix Unitaire = {ligne.PrixUnitaire}");
                }
            }
        }
    }

}
