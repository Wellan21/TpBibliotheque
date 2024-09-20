// See https://aka.ms/new-console-template for more information
using Biblio;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.CompilerServices;

var listeUtilisateurs = new List<IUtilisateur>();
var listeLivres = new List<Livre>();

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
        listeLivres.Add(new Livre(titre, auteur, int.Parse(annee), int.Parse(isbn)));
    }
    catch
    {
        throw new Exception("Une erreur est survenue lors de l'enregistrement du livre");
    }

}

void creeUtilisateur()
{

    Console.WriteLine("Entrez le nom de l'utilisateur");
    string nom = Console.ReadLine();
    Console.WriteLine("Entrez le prenom de l'utilisateur ");
    string prenom = Console.ReadLine();
    listeUtilisateurs.Add(new Utilisateur(nom, prenom, listeUtilisateurs.Count));

}


void creeUtilisateurPremium()
{
    Console.WriteLine("Entrez le nom de l'utilisateur");
    string nom = Console.ReadLine();
    Console.WriteLine("Entrez le prenom de l'utilisateur ");
    string prenom = Console.ReadLine();
    listeUtilisateurs.Add(new UtilisateurPremium(nom, prenom, listeUtilisateurs.Count));
}

void supprimerlivre()
{
    Console.WriteLine("Choissisez le livre a supprimé");
    listerLivres();
    try
    {
        int num = int.Parse(Console.ReadLine());
        var livre = listeLivres.OrderBy(livre => livre.Titre).Skip(num).First();
        if (livre.EstEmprunter)
        {
            throw new Exception("Le livre est emprunté ");
        }
        listeLivres.Remove(livre);
        Console.WriteLine("Le livre a bien été suprimé ");
    }

    catch
    {

        throw new Exception("entrez une valeur valide");
    }

}

void supprimerUtilisateur()
{
    Console.WriteLine("Choisisez un utilisteur");
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

void listerLivres()
{
    int cpt = 0;
    foreach (var livre in listeLivres.OrderBy(livre => livre.Titre))
    {
        Console.WriteLine($"{cpt}. {livre.Titre} par {livre.Auteur} sorti en {livre.AnneeDePublication}. ISBN:{livre.ISBN} est emprunté : { (livre.EstEmprunter ? "Oui" : "Non" )}");
        cpt++;
    }

}

void listerUtilisateurs()
{
    int cpt = 0;
    foreach (var util in listeUtilisateurs.OrderBy(util => util.Nom).ThenBy(util => util.Prenom))
    {
        Console.WriteLine($" {cpt}. {util.Nom} {util.Prenom}");
        cpt++;
    }
}

void menuUtilisateur(IUtilisateur utilisateur)
{

    while (true)
    {
        Console.WriteLine("Appuyer sur une touche pour continuer");
        Console.ReadKey();
        Console.Clear();
        Console.WriteLine("""
        Choisisez une option : 
        1.Emprunter un livre
        2.Rendre un livre 
        3.Voir livres emprunté
        X.Quitter 
        """);

        switch (Console.ReadLine())
        {
            case "1":
                Console.WriteLine("Choissisez le livre à emprunter");
                listerLivres();
                try
                {
                    int num = int.Parse(Console.ReadLine());
                    var livre = listeLivres.OrderBy(livre => livre.Titre).Skip(num).First();
                    if (livre.EstEmprunter)
                    {
                        throw new Exception("Le livre est emprunté ");
                    }
                    utilisateur.empruter(livre);
                    Console.WriteLine("Le livre a bien été emprunté ");
                }

                catch (FormatException)
                {
                    throw new Exception("entrez une valeur valide");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }


                break;

            case "2":
                Console.WriteLine("Choissisez le livre a rendre");
                utilisateur.listerLivres();
                try
                {
                    int num = int.Parse(Console.ReadLine());
                    var livre = utilisateur.Livres.OrderBy(livre => livre.Titre).Skip(num).First();

                    utilisateur.rendre(livre);
                    Console.WriteLine("Le livre a bien été rendu ");
                }

                catch
                {

                    throw new Exception("entrez une valeur valide");
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
while (true)
{
    Console.WriteLine("Appuyer sur une touche pour continuer");
    Console.ReadKey();
    Console.Clear();
    Console.WriteLine("""
        1. Ajouter un livre 
        2. Supprimer un livre 
        3. Ajouter un utilisateur
        4. Ajouter un Utilisateur premium 
        5. Supprimer un livre
        6. Lister les livres 
        7  Lister les utilisateurs 
        8. Choisir utilisateur pour emprunter/ rendre / voir livres empruntés *
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
                Console.WriteLine("Choisisez un utilisteur");
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