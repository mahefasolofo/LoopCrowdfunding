using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crowdfunding
{
    internal class User
    {
        public string nom { get; set; }
        public string prenom { get; set; }
        public string email { get; set; }
        public string motdepasse { get; set; }
        public string adresse { get; set; }
        public DateTime datenaissance { get; set; }
        public string ville { get; set; }
        public string pays { get; set; }
        
        //Getter Setter
        public string getNom()
        {
            return this.nom;
        }
        public string getPrenom()
        {
            return this.prenom;
        }
        public string getEmail()
        {
            return this.email;
        }
        public string getMotdepasse()
        {
            return this.motdepasse;
        }
        public string getAdresse()
        {
            return this.adresse;
        }
        public DateTime getDatedenaissance()
        {
            return this.datenaissance;
        }
        public string getVille()
        {
            return this.ville;
        }
        public string getpays()
        {
            return this.pays;
        }
            
    }
}
