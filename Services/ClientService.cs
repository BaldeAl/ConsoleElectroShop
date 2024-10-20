﻿using ConsoleElectroShop.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleElectroShop.Services
{
    public class ClientService
    {
        private readonly DbElectroShopContext _context;

        public ClientService(DbElectroShopContext context)
        {
            _context = context;
        }

        public void AjouterClient()
        {
            Console.Write("Entrez le nom du client: ");
            string nom = Console.ReadLine();

            Console.Write("Entrez l'adresse du client: ");
            string adresse = Console.ReadLine();

            Console.Write("Entrez l'email du client: ");
            string email = Console.ReadLine();

            var client = new Client
            {
                Nom = nom,
                Adresse = adresse,
                Email = email
            };

            _context.Clients.Add(client);
            _context.SaveChanges();
            Console.WriteLine($"Client '{nom}' ajouté avec succès.");
        }

        public void AfficherLesClients()
        {
            var clients = _context.Clients.ToList();

            if (clients.Any())
            {
                Console.WriteLine("Liste des clients :");
                foreach (var client in clients)
                {
                    Console.WriteLine($"ID: {client.Id}, Nom: {client.Nom}, Adresse: {client.Adresse}, Email: {client.Email}");
                }
            }
            else
            {
                Console.WriteLine("Aucun client trouvé.");
            }
        }



    }

}
