/*
 * Name : Morpion
 * Author : Jeremy-FullStack
 * Date : 14/02/2023
 */
using System;

namespace Morpion
{
    class Program
    {
        enum EtatCase
        {
            Vide,
            Rond,
            Croix
        }
        static EtatCase[,] grille; // grille de 3*3 cases
        static Random geneateur; // générateur aléatoire


        static void Main(string[] args)
        {
            // Message d'acceuil 
            Console.WriteLine("Bienvenue dans le jeu du Morpion !!!");

            // Initialiser les variables
            bool finDeJeu = false;
            grille = new EtatCase[3, 3];
            int nbVide = 9;
            geneateur = new Random();

            // Affichage de la grille
            AfficherGrille();

            // Boucle Principale
            while (!finDeJeu)
            {
                // Jeu utilisateur
                choisirCaseUtilisateur();
                nbVide--;
                // Afficher grille
                AfficherGrille();

                // Jeu Gagnat?
                bool gagne = jeuGagnant(EtatCase.Croix);
                if (gagne)
                {
                    finDeJeu = true;
                    Console.WriteLine("Bravo vous avez gagné!");
                }

                // Jeu de l'ordinateur
                if (!finDeJeu && nbVide > 0)
                {
                    choisirCaseOrdinateur();
                    nbVide--;

                    // Afficher la grille
                    Console.WriteLine("l'ordinateur à jouer :");
                    AfficherGrille();

                    // Jeu Gagnant?
                    if (jeuGagnant(EtatCase.Rond))
                    {
                        finDeJeu = true;
                        Console.WriteLine("Dommage, l'ordinateur a gagné :(");
                    }
                   
                }
          
                // Match Nul ?
                if (nbVide == 0)
                {
                    Console.WriteLine("Match nul!");
                    finDeJeu = true;
                }
            }

            // Fin du jeu
            Console.WriteLine("Appuyer sur une touche pour fermer...");
            Console.ReadKey();

        }

        private static bool jeuGagnant(EtatCase etatCase)
        {
            // Cas d'une ligne
            for(int ligne=0; ligne<3; ligne++)
            {
                if(grille[ligne, 0]==etatCase && grille[ligne, 1]==etatCase && grille[ligne, 2] == etatCase)
                {
                    return true;
                }
            }

            // Cas d'une colonne
            for(int colonne=0; colonne<3; colonne++)
            {
                if(grille[0, colonne]==etatCase && grille[1, colonne]==etatCase && grille[2, colonne] == etatCase)
                {
                    return true;
                }
            }

            // Cas des diagonales
            if(grille[0,0]==etatCase && grille[1,1]==etatCase && grille[2, 2] == etatCase)
            {
                return true;
            }
            if(grille[2,0]==etatCase && grille[1,1]==etatCase && grille[0, 2] == etatCase)
            {
                return true;
            }

            // Par défaut on n'a pas gagné
            return false;
        }

        private static void choisirCaseOrdinateur()
        {
            // Boucler jusqu'à trouver une case vide
            bool choixOK = false;
            while (!choixOK)
            {
                // Choix des coordonnées
                int ligne = geneateur.Next(0, 3);
                int colonne = geneateur.Next(0, 3);
                if(grille[ligne,colonne]== EtatCase.Vide)
                {
                    grille[ligne, colonne] = EtatCase.Rond;
                    choixOK = true;
                }
            }
        }

        /// <summary>
        ///  Permet à l'utilisateur de choisir sa case, qui doit être libre et y mettre une croix
        /// </summary>
        private static void choisirCaseUtilisateur()
        {
            // On boucle jusqu'à un choix correcte
            bool choixOK = false;

            while (!choixOK)
            {
                // Message
                Console.WriteLine("Donnez votre choix de case");

                // Récupération de la réponse
                string reponse = Console.ReadLine();
                int choix;

                // Converstion en entier comprise entre 0 et 8
                if (int.TryParse(reponse, out choix) && choix>=0 && choix<=8)
                {
                    // Case vide?
                    int ligne = choix / 3;
                    int colonne = choix % 3;

                    if(grille[ligne, colonne]== EtatCase.Vide)
                    {
                        // Choix Ok, je valide
                        grille[ligne, colonne]= EtatCase.Croix;
                        choixOK = true;
                    }
                }
            }
        }

        /// <summary>
        /// Affiche la grille du morpion
        /// </summary>
        private static void AfficherGrille()
        {
            string dessinGrille="\n";
            // Trait du Haut
            dessinGrille += "*******\n";

            // Pour chaque ligne

            for (int ligne = 0; ligne < 3; ligne++)
            {
                dessinGrille += "|";

                // Pour chaque Colonne
                for (int colonne = 0; colonne < 3; colonne++)
                {
                    switch (grille[ligne, colonne])
                    {
                        case EtatCase.Vide:
                            dessinGrille += ligne * 3 + colonne;
                            break;
                        case EtatCase.Croix:
                            dessinGrille += "X";
                            break;
                        case EtatCase.Rond:
                            dessinGrille += "O";
                            break;
                    }
                    dessinGrille += "|";
                }
                dessinGrille += "\n*******\n";
            }
            // Affichage console
            Console.WriteLine(dessinGrille);
        }
    }
}
