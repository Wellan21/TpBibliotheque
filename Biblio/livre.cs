using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblio
{
    internal class Livre
    {
        private string titre;
        private string auteur;
        private int anneeDePublication;
        private int isbn;
        private bool estEmprunter; 

        public Livre(string titre, string auteur, int anneeDePublication, int isbn)
        {
            this.titre = titre;
            this.auteur = auteur;
            this.anneeDePublication = anneeDePublication;
            this.isbn = isbn;
            this.estEmprunter = false; 
        }

        public string Titre { get => titre;  }
        public string Auteur { get => auteur;}
        public int AnneeDePublication { get => anneeDePublication;}
        public int ISBN { get => isbn;}
        public bool EstEmprunter { get => estEmprunter; set => estEmprunter = value; }
    }
}
