using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblio
{
    interface IUtilisateur
    {

        public string Prenom { get; set; }
        string Nom { get; set; }
        void empruter(Livre livre);
        void rendre(Livre livre);
        public void listerLivres();
        internal HashSet<Livre> Livres { get; }

    }
}
