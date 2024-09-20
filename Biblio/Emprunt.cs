using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblio
{
    internal class Emprunt
    {
        public Livre Livre;
        public Utilisateur Utilisateur;
        public DateTime date = DateTime.Now.Date;

        public Emprunt(Livre livre, Utilisateur utilisateur)
        {
            this.Livre = livre;
            this.Utilisateur = utilisateur;
        }
    }
}
