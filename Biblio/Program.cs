
/// <summary>
/// Programme principal de gestion d'une bibliothèque.
/// Gère les utilisateurs, livres et emprunts via un menu interactif.
/// </summary>

// Inclusion des bibliothèques nécessaires
using Biblio;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.CompilerServices;

/// <summary>
/// Liste des utilisateurs de la bibliothèque.
/// </summary>
var listeUtilisateurs = new List<IUtilisateur>();

/// <summary>
/// Liste des livres disponibles à la bibliothèque.
/// </summary>
var listeLivres = new List<Livre>();

/// <summary>
/// Crée un nouveau livre et l'ajoute à la liste des livres.
/// Demande à l'utilisateur d'entrer le titre, l'auteur, l'ISBN et l'année de sortie du livre.
/// </summary>
void creeLivre()
{
    Console.WriteLine("Entrez le titre du livre");
    string titre = Console.ReadLine();
    Console.WriteLine("Entrez l'auteur du livre");
    string auteur = Console.ReadLine();
    Console.WriteLine("Entrez l'isbn du livre");
    string isbn = Console.ReadLine();
    Console.WriteLine("Entrez l'année de sortie du livre");
    string annee = Console.ReadLine();

    try
    {
        // Ajoute le livre à la liste après l'avoir créé
        listeLivres.Add(new Livre(titre, auteur, int.Parse(annee), int.Parse(isbn)));
    }
    catch
    {
        throw new Exception("Une erreur est survenue lors de l'enregistrement du livre");
    }
}

/// <summary>
/// Crée un utilisateur standard et l'ajoute à la liste des utilisateurs.
/// Demande le nom et le prénom.
/// </summary>
void creeUtilisateur()
{
    Console.WriteLine("Entrez le nom de l'utilisateur");
    string nom = Console.ReadLine();
    Console.WriteLine("Entrez le prenom de l'utilisateur");
    string prenom = Console.ReadLine();
    listeUtilisateurs.Add(new Utilisateur(nom, prenom, listeUtilisateurs.Count));
}

/// <summary>
/// Crée un utilisateur premium et l'ajoute à la liste des utilisateurs.
/// Demande le nom et le prénom.
/// </summary>
void creeUtilisateurPremium()
{
    Console.WriteLine("Entrez le nom de l'utilisateur");
    string nom = Console.ReadLine();
    Console.WriteLine("Entrez le prenom de l'utilisateur");
    string prenom = Console.ReadLine();
    listeUtilisateurs.Add(new UtilisateurPremium(nom, prenom, listeUtilisateurs.Count));
}

/// <summary>
/// Supprime un livre de la bibliothèque.
/// L'utilisateur choisit un livre à partir de la liste des livres.
/// </summary>
void supprimerlivre()
{
    Console.WriteLine("Choissisez le livre à supprimer");
    listerLivres();
    try
    {
        int num = int.Parse(Console.ReadLine());
        var livre = listeLivres.OrderBy(livre => livre.Titre).Skip(num).First();
        if (livre.EstEmprunter)
        {
            throw new Exception("Le livre est emprunté");
        }
        listeLivres.Remove(livre);
        Console.WriteLine("Le livre a bien été supprimé");
    }
    catch
    {
        throw new Exception("Entrez une valeur valide");
    }
}

/// <summary>
/// Supprime un utilisateur de la liste des utilisateurs.
/// L'utilisateur choisit un utilisateur à partir de la liste.
/// </summary>
void supprimerUtilisateur()
{
    Console.WriteLine("Choisissez un utilisateur");
    listerUtilisateurs();
    try
    {
        int num = int.Parse(Console.ReadLine());
        var utilisateur = listeUtilisateurs.OrderBy(util => util.Nom).ThenBy(util => util.Prenom).Skip(num).First();
        listeUtilisateurs.Remove(utilisateur);
    }
    catch
    {
        throw new Exception("Entrez une valeur valide");
    }
}

/// <summary>
/// Affiche la liste des livres disponibles à la bibliothèque.
/// Les livres sont triés par titre.
/// </summary>
void listerLivres()
{
    int cpt = 0;
    foreach (var livre in listeLivres.OrderBy(livre => livre.Titre))
    {
        Console.WriteLine($"{cpt}. {livre.Titre} par {livre.Auteur} sorti en {livre.AnneeDePublication}. ISBN:{livre.ISBN} est emprunté : {(livre.EstEmprunter ? "Oui" : "Non")}");
        cpt++;
    }
}

/// <summary>
/// Affiche la liste des utilisateurs de la bibliothèque.
/// Les utilisateurs sont triés par nom puis par prénom.
/// </summary>
void listerUtilisateurs()
{
    int cpt = 0;
    foreach (var util in listeUtilisateurs.OrderBy(util => util.Nom).ThenBy(util => util.Prenom))
    {
        Console.WriteLine($"{cpt}. {util.Nom} {util.Prenom}");
        cpt++;
    }
}

/// <summary>
/// Menu utilisateur pour emprunter, rendre ou voir les livres empruntés.
/// </summary>
/// <param name="utilisateur">L'utilisateur pour lequel afficher le menu.</param>
void menuUtilisateur(IUtilisateur utilisateur)
{
    while (true)
    {
        Console.WriteLine("Appuyez sur une touche pour continuer");
        Console.ReadKey();
        Console.Clear();
        Console.WriteLine("""
        Choisissez une option :
        1. Emprunter un livre
        2. Rendre un livre
        3. Voir les livres empruntés
        X. Quitter
        """);

        switch (Console.ReadLine())
        {
            case "1":
                Console.WriteLine("Choisissez le livre à emprunter");
                listerLivres();
                try
                {
                    int num = int.Parse(Console.ReadLine());
                    var livre = listeLivres.OrderBy(livre => livre.Titre).Skip(num).First();
                    if (livre.EstEmprunter)
                    {
                        throw new Exception("Le livre est emprunté");
                    }
                    utilisateur.empruter(livre);
                    Console.WriteLine("Le livre a bien été emprunté");
                }
                catch (FormatException)
                {
                    throw new Exception("Entrez une valeur valide");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                break;

            case "2":
                Console.WriteLine("Choisissez le livre à rendre");
                utilisateur.listerLivres();
                try
                {
                    int num = int.Parse(Console.ReadLine());
                    var emprunt = utilisateur.Emprunts.OrderBy(emp => emp.Livre.Titre).Skip(num).First();
                    utilisateur.rendre(emprunt);
                    Console.WriteLine("Le livre a bien été rendu");
                }
                catch
                {
                    throw new Exception("Entrez une valeur valide");
                }
                break;

            case "3":
                utilisateur.listerLivres();
                break;

            case "X":
                return;

            default:
                Console.WriteLine("Entrez une option valide");
                break;
        }
    }
}

/// <summary>
/// Menu principal pour gérer les livres et les utilisateurs.
/// </summary>
while (true)
{
    Console.WriteLine("Appuyez sur une touche pour continuer");
    Console.ReadKey();
    Console.Clear();
    Console.WriteLine("""
        1. Ajouter un livre
        2. Supprimer un livre
        3. Ajouter un utilisateur
        4. Ajouter un utilisateur premium
        5. Supprimer un utilisateur
        6. Lister les livres
        7. Lister les utilisateurs
        8. Choisir un utilisateur pour emprunter/rendre/voir livres empruntés
        X. Quitter
    """);
    try
    {
        switch (Console.ReadLine())
        {
            case "1":
                creeLivre();
                break;
            case "2":
                supprimerlivre();
                break;
            case "3":
                creeUtilisateur();
                break;
            case "4":
                creeUtilisateurPremium();
                break;
            case "5":
                supprimerUtilisateur();
                break;
            case "6":
                listerLivres();
                break;
            case "7":
                listerUtilisateurs();
                break;
            case "8":
                Console.WriteLine("Choisissez un utilisateur");
                listerUtilisateurs();
                try
                {
                    int num = int.Parse(Console.ReadLine());
                    var utilisateur = listeUtilisateurs.OrderBy(util => util.Nom).ThenBy(util => util.Prenom).Skip(num).First();
                    menuUtilisateur(utilisateur);
                }
                catch
                {
                    throw new Exception("Entrez une valeur valide");
                }
                break;

            case "X":
                return;

            default:
                Console.WriteLine("Entrez une option valide");
                break;
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}
