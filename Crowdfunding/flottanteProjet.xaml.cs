using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Crowdfunding
{
    /// <summary>
    /// Interaction logic for flottanteProjet.xaml
    /// </summary>
    public partial class flottanteProjet : Window
    {
        //ObservableCollection<Projet> liste = new ObservableCollection<Projet>();
        Window2 accueil2 = new Window2();

        
        public flottanteProjet()
        {
            InitializeComponent();
            affichageFlottante();
        }
        //Extraction des données du projet sélectionné sur la listView | Vue Flottante
        
        public void affichageFlottante()
        {
            
            //Afficher les valeurs
            labelTitre.Content = accueil2.titreSelected;
            labelDescription.Content = accueil2.descriptionSelected;
            labelObjectifCagnotte.Content = accueil2.objectifCagnotteSelected;
            labelStatut.Content = accueil2.statutSelected;
            labelOuverture.Content = accueil2.ouvertureSelected;
            labelFermeture.Content = accueil2.fermetureSelected;


        }

        private void Investir_click(object sender, RoutedEventArgs e)
        {

        }

        private void fermer_click(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
