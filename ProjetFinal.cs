using System;
using System.Collections.Generic;

namespace Projet_Final_Intro_Prog
{
    internal class ProjetFinal
    {
        // Tableaux des employés
        static string[] codesEmployes = { "001", "002", "003", "004", "005" };

        static string[] nomEmployes = { "Samir", "Mohamed", "Afef", "Jean-Gabriel", "Eve" };

        // Tableaux des articles
        static string[] codeArticles = { "A1", "A2", "B1", "B2", "B3", "L1", "L2", "L3" };
        static string[] nomArticles = { "Crayon", "Cahier Canada", "Table pliante", "Fauteuil en cuir","Bureau d'etudiant", "Laptop ASUS", "Laptop HP", "Laptop ACER" };
        static double[] prixArticles = { 3.99, 1.59, 66.99, 199.99, 118.99, 600.89, 700.89, 250.99 };

        // Panier (liste d’indices)
        static List<int> panier = new List<int>();

        static void Main(string[] args)
        {
            int indexEmploye = AuthentifierEmploye();

            if (indexEmploye == -1)
            {
                Console.WriteLine("ERREUR: Numéro d'employé invalide");
                AttendreFermeture();
                return;
            }

            Console.WriteLine($"Bonjour, {nomEmployes[indexEmploye]}");
            MenuPrincipal(indexEmploye);

            // Attendre avant de fermer
            AttendreFermeture();
        }

        // Fonction d’authentification : retourne l’index de l’employé ou -1 si invalide
        static int AuthentifierEmploye()
        {
            Console.Write("Veuillez entrer votre code (chiffres): ");
            string code = Console.ReadLine();

            for (int i = 0; i < codesEmployes.Length; i++)
            {
                if (codesEmployes[i] == code)
                {
                    return i; // retourne l’index trouvé
                }
            }
            return -1; // pas trouvé
        }

        // Procédure du menu principal
        static void MenuPrincipal(int indexEmploye)
        {
            string choix;
            do
            {
                Console.WriteLine("************************");
                Console.WriteLine("MENU PRINCIPAL");
                Console.WriteLine("************************");
                Console.WriteLine("1. Ajouter un article");
                Console.WriteLine("2. Supprimer un article");
                Console.WriteLine("3. Afficher le panier");
                Console.WriteLine("0. Payer");
                Console.Write("Votre choix: ");
                choix = Console.ReadLine();

                switch (choix)
                {
                    case "1": AjouterArticle(); break;
                    case "2": SupprimerArticle(); break;
                    case "3": AfficherPanier(); break;
                    case "0": Payer(indexEmploye); break;
                    default: Console.WriteLine("Choix invalide..."); break;
                }

            } while (choix != "0");
        }

        // Procédure pour ajouter un article
        static void AjouterArticle()
        {
            Console.WriteLine("********************");
            Console.WriteLine("AJOUT ARTICLE");
            Console.WriteLine("********************");

            for (int i = 0; i < codeArticles.Length; i++)
            {
                Console.WriteLine($"{codeArticles[i]}: {nomArticles[i]} - {prixArticles[i]}$");
            }

            Console.Write("Votre choix: ");
            string choix = Console.ReadLine();

            int index = TrouverArticle(choix);
            if (index != -1)
                panier.Add(index);
            else
                Console.WriteLine("Choix invalide...");
        }

        // Procédure pour supprimer un article
        static void SupprimerArticle()
        {
            Console.WriteLine("********************");
            Console.WriteLine("RETIRER ARTICLE");
            Console.WriteLine("********************");

            for (int i = 0; i < panier.Count; i++)
            {
                int idx = panier[i];
                Console.WriteLine($"{codeArticles[idx]}: {nomArticles[idx]} - {prixArticles[idx]}$");
            }

            Console.Write("Votre choix: ");
            string choix = Console.ReadLine();

            int index = TrouverArticle(choix);
            if (panier.Contains(index))
                panier.Remove(index);
            else
                Console.WriteLine("Choix invalide...");
        }

        // Procédure pour afficher le panier
        static void AfficherPanier()
        {
            Console.WriteLine("********************");
            Console.WriteLine("PANIER");
            Console.WriteLine("********************");

            foreach (int idx in panier)
            {
                Console.WriteLine($"{codeArticles[idx]}: {nomArticles[idx]} - {prixArticles[idx]}$");
            }
        }

        // Procédure pour payer
        static void Payer(int indexEmploye)
        {
            Console.WriteLine("******************************");
            Console.WriteLine("** FACTURE **");
            Console.WriteLine("******************************");

            double sousTotal = 0;
            foreach (int idx in panier)
            {
                Console.WriteLine($"- {codeArticles[idx]}: {nomArticles[idx]} {prixArticles[idx]}$");
                sousTotal += prixArticles[idx];
            }

            // Rabais mystère 25% avec 50% de chance
            Random rand = new Random();
            double rabais = 0;
            if (rand.Next(2) == 1)
            {
                rabais = sousTotal * 0.25;
                Console.WriteLine($"Rabais mystère: {rabais:F2}$");
                sousTotal -= rabais;
            }

            double tps = sousTotal * 0.05;
            double tvq = sousTotal * 0.09975;
            double total = sousTotal + tps + tvq;

            Console.WriteLine("------------------------------");
            Console.WriteLine($"Sous-total: {sousTotal:F2}$");
            Console.WriteLine($"TPS: {tps:F2}$");
            Console.WriteLine($"TVQ: {tvq:F2}$");
            Console.WriteLine($"Total: {total:F2}$");
            Console.WriteLine("******************************");
            Console.WriteLine($"Vous avez été servi par {nomEmployes[indexEmploye]}");
            Console.WriteLine($"Date: {DateTime.Now:yyyy-MM-dd}");
            Console.WriteLine($"Heure: {DateTime.Now:HH:mm:ss}");
            Console.WriteLine("******************************");
        }

        // Fonction utilitaire pour trouver un article par son code
        static int TrouverArticle(string code)
        {
            for (int i = 0; i < codeArticles.Length; i++)
            {
                if (codeArticles[i] == code)
                    return i;
            }
            return -1;
        }

        // Procédure pour attendre une touche avant de fermer
        static void AttendreFermeture()
        {
            Console.WriteLine("\nAppuyez sur une touche pour fermer la fenêtre...");
            Console.ReadKey(true);
        }
    }
}