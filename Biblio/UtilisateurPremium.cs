using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblio
{
    internal class UtilisateurPremium : Utilisateur

    {
        public UtilisateurPremium(string nom, string prenom, int id) : base(nom, prenom, id)
        {
            this.NbLivreMax = 5;
        }
    }
}
