using MySql.Data.MySqlClient;
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

namespace Crowdfunding
{
    /// <summary>
    /// Logique d'interaction pour Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        MySqlConnection conn  = new MySqlConnection("SERVER=127.0.0.1; DATABASE='crowdfunding'; UID=root; PASSWORD=");

        ObservableCollection<Projet> liste = new ObservableCollection<Projet>();
        
        private int ID_paiement_invest = 0;
        
        
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

        public Window2()
        {
            InitializeComponent();
            affichageListeProjet();
            chargementCheckbox();
        }
        //Affichage de tous les listes de projets
        private void affichageListeProjet()
        {
            //connexion à la base de donnée
            //conn = new MySqlConnection("SERVER=127.0.0.1; DATABASE='crowdfunding'; UID=root; PASSWORD=");
            conn.Open();


            String sql = "SELECT * FROM `projet` ";

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
                        liste.Add(new Projet()
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

                        listeProjet.ItemsSource = liste;


                    }
                    reader.Close();
                }
            }
            conn.Close();
        }

        private void affichageListeProjetUser()
        {
            //connexion à la base de donnée
            //conn = new MySqlConnection("SERVER=127.0.0.1; DATABASE='crowdfunding'; UID=root; PASSWORD=");
            conn.Open();


            String sql = "SELECT * FROM `projet` WHERE fk_id_user_projet = 1";

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
                        liste.Add(new Projet()
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

                        listeProjet2.ItemsSource = liste;


                    }
                    reader.Close();
                }
            }
            conn.Close();
        }

        //à effacer
        private void listeProjet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //selectionner l'Item clické dans accueil2
            flottanteProjet flottanteProjet = new flottanteProjet();
            flottanteProjet.Activate();

            index = listeProjet.SelectedIndex;
            //Affecter les valeurs de l'Item clické dans des variables à l'aide de Getter
            ID_projet = liste[index].getIdProjet();
            titreSelected = liste[index].getTitre();
            descriptionSelected = liste[index].getDescription();
            sommeCagnotteSelected = liste[index].getSommeCagnotte().ToString();
            objectifCagnotteSelected = liste[index].getObjectifCagnotte().ToString();
            statutSelected = liste[index].getStatut();
            imageSelected = liste[index].getImage();
            ouvertureSelected = liste[index].getOuverture().ToString();
            fermetureSelected = liste[index].getFermeture().ToString();
            


            

        }


        private void Button_Click_Accueil(object sender, RoutedEventArgs e)
        {
            
            Accueil1.Visibility = Visibility.Visible;
            Cagnottes.Visibility = Visibility.Hidden;
            GridCreation.Visibility = Visibility.Hidden;
        }

        private void Button_Click_cagnotte(object sender, RoutedEventArgs e)
        {
            affichageListeProjetUser(); affichageListeProjetUser();
            Accueil1.Visibility = Visibility.Hidden;
            Cagnottes.Visibility = Visibility.Visible;
            GridCreation.Visibility = Visibility.Hidden;
        }
        private void Button_Click_investissement(object sender, RoutedEventArgs e)
        {
            //GridCursor.Margin = new Thickness(405, 20, 0, 0);
        }

        private void Button_Click_notification(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_User(object sender, RoutedEventArgs e)
        {
            //GridCursor.Margin = new Thickness(1135, 20, 0, 0);
        }

        private void ButtonClick_creation_cagnotte(object sender, RoutedEventArgs e)
        {
            Cagnottes.Visibility = Visibility.Hidden;
            GridCreation.Visibility = Visibility.Visible;
        }




        private void fermer_click(object sender, MouseButtonEventArgs e)
        {
            //flotanteProjet.Visibility = Visibility.Hidden;
            Recherche.Visibility = Visibility.Visible;
            listeProjet.IsEnabled = true;
        }

        //private void Investir_click(object sender, RoutedEventArgs e)
        ////{
        ////    flotanteProjet.Visibility = Visibility.Hidden;
        ////    flotanteInvestir.Visibility = Visibility.Visible;
        //}


        //private void fermer_click2(object sender, MouseButtonEventArgs e)
        //{
        //    flotanteInvestir.Visibility = Visibility.Hidden;
        //    listeProjet.IsEnabled = true;
        //    Recherche.Visibility = Visibility.Visible;
        //}

        //private void back_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    flotanteProjet.Visibility = Visibility.Visible;
        //    flotanteInvestir.Visibility = Visibility.Hidden;
        //}

        //private void Payer_Click(object sender, RoutedEventArgs e)
        //{

        //    sommeInvest = sommeInvesti.Text;

        //    //Connexion vers BDD
        //    conn = new MySqlConnection("SERVER=127.0.0.1; DATABASE='crowdfunding'; UID=root; PASSWORD=");
        //    conn.Open();
        //    String sql = "INSERT INTO `investisssement` (`ID_investissement`, `fk_id_user_invest`, `montant_investissement`, `date_investissement`, `fk_ID_paiement_invest`, `reference_projet`) VALUES (NULL, '1', '" + sommeInvest + "', current_timestamp(), '" + ID_paiement_invest.ToString() + "', '" + ID_projet.ToString() + "')";
        //    MySqlCommand cmd = new MySqlCommand(sql, conn);
        //    cmd.ExecuteNonQuery();

        //    flotanteInvestir.Visibility = Visibility.Hidden;
        //    Recherche.Visibility = Visibility.Visible;
        //    listeProjet.IsEnabled = true;
        //    conn.Close();

        //    MessageBox.Show("Investissement fait");

        //}

        //Vérification RadioButton moyen de payement 
        //A mettre dans la fenêtre investir
        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            ID_paiement_invest = int.Parse((string)rb.Content);
        }
        private void chargementCheckbox()
        {
            conn.Open();
            string requete = " SELECT  * from `categorie`";
            MySqlCommand cmd = new MySqlCommand(requete, conn);

            MySqlDataReader myReader;
            myReader = cmd.ExecuteReader();

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    comboboxCateg.Items.Add(myReader.GetString(1));
                }
            }



            myReader.Close();

            conn.Close();
        }

        

        private void Button_Click_valider(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_annuler(object sender, RoutedEventArgs e)
        {
            RAZ();
        }
        public void RAZ()
        {
            titreprojet.Text = "";
            descriptionProjet.Text = "";
            objectifCagnotte.Text = "";
            date_fermeture_cagnotte.Text = null;
            date_debut_dividende.Text = null;
            uploadBP_nom.Text = "";
            uploadCIN_nom.Text = "";
            uploadImage_nom.Text = "";
            comboboxCateg.Text = "";



        }
        //afficher la fenêtre flottante projet à partir accueil projet
        private void listeProjet_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            //selectionner l'Item clické dans accueil2
            flottanteProjet flottanteProjet = new flottanteProjet();
            flottanteProjet.Show();


            //Affecter les valeurs de l'Item clické dans des variables à l'aide de Getter
            ID_projet = liste[index].getIdProjet();
            titreSelected = liste[index].getTitre();
            descriptionSelected = liste[index].getDescription();
            sommeCagnotteSelected = liste[index].getSommeCagnotte().ToString();
            objectifCagnotteSelected = liste[index].getObjectifCagnotte().ToString();
            statutSelected = liste[index].getStatut();
            imageSelected = liste[index].getImage();
            ouvertureSelected = liste[index].getOuverture().ToString();
            fermetureSelected = liste[index].getFermeture().ToString();

        }

        //afficher la fenêtre flottante projet à partir porteur de projet : à changer /!\
        private void selectionner_projet(object sender, SelectionChangedEventArgs e)
        {
            //string curentItem = listeProjet.SelectedItem.ToString();
            index = listeProjet.SelectedIndex;
            flottanteProjet flottanteProjet = new flottanteProjet();
            flottanteProjet.Activate();


            //Affecter les valeurs de l'Item clické dans des variables à l'aide de Getter
            ID_projet = liste[index].getIdProjet();
            titreSelected = liste[index].getTitre();
            descriptionSelected = liste[index].getDescription();
            sommeCagnotteSelected = liste[index].getSommeCagnotte().ToString();
            objectifCagnotteSelected = liste[index].getObjectifCagnotte().ToString();
            statutSelected = liste[index].getStatut();
            imageSelected = liste[index].getImage();
            ouvertureSelected = liste[index].getOuverture().ToString();
            fermetureSelected = liste[index].getFermeture().ToString();

        }
    }
}
