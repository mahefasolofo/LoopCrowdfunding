using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
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
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Win32;
using Path = System.IO.Path;

namespace Crowdfunding
{
    /// <summary>
    /// Interaction logic for Espace_admin.xaml
    /// </summary>
    public partial class Espace_admin : Window
    {
        MySqlConnection conn = new MySqlConnection("SERVER=127.0.0.1; DATABASE='crowdfunding'; UID=root; PASSWORD=");

        ObservableCollection<Projet> liste_a_valider = new ObservableCollection<Projet>();


        private int ID_paiement_invest = 0;
        private string sommeInvest = "";

        public int index { get; set; }
        public int ID_projet { get; set; }
        public string titreSelected { get; set; }
        public string descriptionSelected { get; set; }
        public string sommeCagnotteSelected { get; set; }
        public string objectifCagnotteSelected { get; set; }
        public string statutSelected { get; set; }
        public string imageSelected { get; set; }
        public string ouvertureSelected { get; set; }
        public string fermetureSelected { get; set; }

        public Espace_admin()
        {
            InitializeComponent();
            affichageListeProjet();
        }

        //Affichage de tous les listes de projets
        private void affichageListeProjet()
        {
            //connexion à la base de donnée
            //conn = new MySqlConnection("SERVER=127.0.0.1; DATABASE='crowdfunding'; UID=root; PASSWORD=");
            conn.Open();

            String sql = "SELECT * FROM `projet_a_valider` ";

            //creat command
            MySqlCommand cmd = new MySqlCommand();

            //Etablir la connexion de la commande
            cmd.Connection = conn;
            cmd.CommandText = sql;

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {   //lecture de l'image et conversion du nom            
                        String imgpath = reader.GetString(13);

                        String img = imgpath.Replace("\\", "#");
                        String[] im = img.Split('#');
                        String photo = im[2];

                        //ajout de donnée à afficher dans la liste
                        liste_a_valider.Add(new Projet()
                        {

                            IdProjet = reader.GetInt32(0),
                            titre = reader.GetString(3),
                            description = reader.GetString(4),
                            sommeCagnotte = reader.GetFloat(5),
                            objectifCagnotte = reader.GetFloat(6),
                            statut = reader.GetString(9),
                            image = photo,
                            ouverture = reader.GetDateTime(10),
                            fermeture = reader.GetDateTime(11)
                        });
                    }
                    reader.Close();
                }
            }

            MessageBox.Show(liste_a_valider.ToString());
            listeProjet.ItemsSource = liste_a_valider;
            conn.Close();
        }
    }
}
