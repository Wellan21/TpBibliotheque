using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Biblio
{
    internal class Utilisateur
    {
        protected string nom;
        protected string prenom;
        protected int id;
        protected int NbLivreMax;
        protected HashSet<Livre> livres;

        public Utilisateur(string nom, string prenom, int id)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.id = id;
            this.NbLivreMax = 3;
            this.livres = new HashSet<Livre>();
        }

        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public int Id { get => id; }
        internal HashSet<Livre> Livres { get => livres; }

        public void empruter(Livre livre)
        {
            if (!livre.EstEmprunter)
                if (livres.Count >= NbLivreMax)
                {
                    throw new Exception("Le nombre de livre max a été dépassé");
                }
            livres.Add(livre);
            livre.EstEmprunter = true;

        }
        public void rendre(Livre livre)
        {
            if (livres.Contains(livre))
            {
                throw new Exception("le livre n'est pas emprunter par l'utilisateur");
            }
            livres.Remove(livre);
            livre.EstEmprunter = false;
        }
        public void listerLivres()
        {
           int cpt = 0;
            foreach (var livre in livres.OrderBy(livre => livre.Titre))
            {
                Console.WriteLine($"{cpt}. {livre.Titre} par {livre.Auteur} sorti en {livre.AnneeDePublication}. ISBN:{livre.ISBN}");
                cpt++;
            }
        }
    }
}