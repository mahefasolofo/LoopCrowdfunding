using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crowdfunding
{
    internal class Projet
    {
        [System.ComponentModel.Bindable(true)]
        [System.ComponentModel.Browsable(false)]

        public int IdProjet { get; set; }
        public String titre { get; set; }
        public String description { get; set; }
        public float sommeCagnotte { get; set; }
        public float objectifCagnotte { get; set; }
        public String statut { get; set; }
        public String image { get; set; }
        public DateTime ouverture { get; set; }
        public DateTime fermeture { get; set; }
        public DateTime paiement { get; set; }

        public object SelectedItem { get; set; }

        public override string ToString()
        {
            return this.titre + " " + this.description + " " + this.sommeCagnotte + " " + this.objectifCagnotte + " " + this.objectifCagnotte + " " + this.statut;
        }

        //Getter Setter
        public String getTitre()
        {
            return this.titre;
        }
        public String getDescription()
        {
            return this.description;
        }

        public float getSommeCagnotte()
        {
            return this.sommeCagnotte;
        }
        public float getObjectifCagnotte()
        {
            return this.objectifCagnotte;
        }

        public String getStatut()
        {
            return this.statut;
        }
        public String getImage()
        {
            return this.image;
        }
        public DateTime getOuverture()
        {
            return this.ouverture;
        }
        public DateTime getFermeture()
        {
            return this.fermeture;
        }

        public int getIdProjet()
        {
            return this.IdProjet;
        }

    }
}
