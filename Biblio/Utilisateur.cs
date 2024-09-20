using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Biblio
{
    internal class Utilisateur : IUtilisateur
    {
        protected string nom;
        protected string prenom;
        protected int id;
        protected int NbLivreMax;
        protected HashSet<Emprunt> emprunts;
        
        public Utilisateur(string nom, string prenom, int id)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.id = id;
            this.NbLivreMax = 3;
            this.emprunts = new HashSet<Emprunt>();
        }

        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public int Id { get => id; }
        public HashSet<Emprunt> Emprunts { get => emprunts; }

        public void empruter(Livre livre)
        {
            if (!livre.EstEmprunter)
                if (emprunts.Count >= NbLivreMax)
                {
                    throw new Exception("Le nombre de livre max a été dépassé");
                }
            emprunts.Add(new Emprunt(livre,this));
            livre.EstEmprunter = true;

        }
        public void rendre(Emprunt emprunt)
        {
           
            if (!emprunts.Contains(emprunt))
            {
                throw new Exception("le livre n'est pas emprunter par l'utilisateur");
            }
            if (DateTime.Now.Subtract(emprunt.date).Days > 15)
            {
                this.penalite(); 
            }
            emprunts.Remove(emprunt); 
            emprunt.Livre.EstEmprunter = false;
        }
        public void listerLivres()
        {
           int cpt = 0;
            foreach (var emp in emprunts.OrderBy(emp => emp.Livre.Titre))
            {
                Console.WriteLine($"{cpt}. {emp.Livre.Titre} par {emp.Livre.Auteur} sorti en {emp.Livre.AnneeDePublication}. ISBN:{emp.Livre.ISBN}");
                cpt++;
            }
        }
        public void penalite()
        {
            throw new NotImplementedException();  
        }
    }
}