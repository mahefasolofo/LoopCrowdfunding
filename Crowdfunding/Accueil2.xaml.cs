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
        MySqlConnection conn = new MySqlConnection("SERVER=127.0.0.1; DATABASE='crowdfunding'; UID=root; PASSWORD=");

        ObservableCollection<Projet> liste = new ObservableCollection<Projet>();


        private int ID_paiement_invest = 0;
        private string sommeInvest = "";
        public int ID_user_connected;

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
            ID_user_connected = int.Parse(textbox_ID.Text);


            String sql = "SELECT * FROM `projet` WHERE fk_id_user_projet = " + ID_user_connected + "";

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
            if (deroulante_Projet.Visibility == Visibility.Hidden)
                deroulante_Projet.Visibility = Visibility.Visible;
            else
                deroulante_Projet.Visibility = Visibility.Hidden;
             
        }

        private void ButtonClick_creation_cagnotte(object sender, RoutedEventArgs e)
        {
            Cagnottes.Visibility = Visibility.Hidden;
            GridCreation.Visibility = Visibility.Visible;
        }




        private void fermer_click(object sender, MouseButtonEventArgs e)
        {
            flotanteProjet.Visibility = Visibility.Hidden;
            GridMainCagnotte.IsEnabled = true;
            Accueil2.IsEnabled = true;
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
            flotanteProjet.Visibility = Visibility.Visible;
            Accueil2.IsEnabled = false;



            //selectionner l'Item clické
            string curentItem = listeProjet.SelectedItem.ToString();
            int index = listeProjet.SelectedIndex;
            //Affecter les valeurs de l'Item clické dans des variables à l'aide de Getter
            string titreSelected = liste[index].getTitre();
            string descriptionSelected = liste[index].getDescription();
            string sommeCagnotteSelected = liste[index].getSommeCagnotte().ToString();
            string objectifCagnotteSelected = liste[index].getObjectifCagnotte().ToString();
            string statutSelected = liste[index].getStatut();
            string imageSelected = liste[index].getImage();
            string ouvertureSelected = liste[index].getOuverture().ToString();
            string fermetureSelected = liste[index].getFermeture().ToString();
            ID_projet = liste[index].getIdProjet();

            //Faire apparaitre la fenêtre flottante et Désactiver la listeView pour qu'on ne peut pas la toucher
            flotanteProjet.Visibility = Visibility.Visible;
            Recherche.Visibility = Visibility.Hidden;
            Accueil2.IsEnabled = false;
            //Afficher les valeurs
            labelTitre.Content = titreSelected;
            labelDescription.Content = descriptionSelected;
            labelObjectifCagnotte.Content = objectifCagnotteSelected;
            labelStatut.Content = statutSelected;
            labelOuverture.Content = ouvertureSelected;
            labelFermeture.Content = fermetureSelected;



        }


        //INVESTIR

        private void Investir_click(object sender, RoutedEventArgs e)
        {
            flotanteProjet.Visibility = Visibility.Hidden;
            flotanteInvestir.Visibility = Visibility.Visible;

        }

        private void back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            flotanteProjet.Visibility = Visibility.Visible;
            flotanteInvestir.Visibility = Visibility.Hidden;
        }

        private void fermer_click2(object sender, MouseButtonEventArgs e)
        {
            flotanteProjet.Visibility = Visibility.Hidden;
            flotanteInvestir.Visibility = Visibility.Hidden;
            GridMainCagnotte.IsEnabled = true;
            Accueil2.IsEnabled = true;
        }

        //Vérification RadioButton moyen de payement 
        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            ID_paiement_invest = int.Parse((string)rb.Content);
        }


        private void Payer_Click(object sender, RoutedEventArgs e)
        {
            ID_user_connected = int.Parse(textbox_ID.Text);
            sommeInvest = sommeInvesti.Text;

            //Connexion vers BDD
            conn = new MySqlConnection("SERVER=127.0.0.1; DATABASE='crowdfunding'; UID=root; PASSWORD=");
            conn.Open();
            String sql = "INSERT INTO `investisssement` (`ID_investissement`, `fk_id_user_invest`, `montant_investissement`, `date_investissement`, `fk_ID_paiement_invest`, `reference_projet`) VALUES (NULL, '" + ID_user_connected + "', '" + sommeInvest + "', current_timestamp(), '" + ID_paiement_invest + "', '" + ID_projet + "')";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            flotanteInvestir.Visibility = Visibility.Hidden;
            Recherche.Visibility = Visibility.Visible;
            listeProjet.IsEnabled = true;
            conn.Close();

            MessageBox.Show("Investissement fait");

        }

        //MES CAGNOTTES
        private void selectionner_projet(object sender, SelectionChangedEventArgs e)
        {

        }



        //MON PROFIL

        private void MonProfil_click(object sender, MouseButtonEventArgs e)
        {
            MainStack.IsEnabled = false;
            if (Profil.Visibility == Visibility.Hidden)
                Profil.Visibility = Visibility.Visible;
            else
                Profil.Visibility = Visibility.Hidden;

            User utilisateur = new User();
            ID_user_connected = int.Parse(textbox_ID.Text);
            conn.Open();
            string sql = "SELECT `nom`, `prenom`, `email`, `motdepasse`, `adresse`, `datenaissance`, `ville`, `pays` FROM `users` WHERE `ID_user`=" + ID_user_connected + "";

            MySqlCommand cmd = new MySqlCommand();

            //Etablir la connexion de la commande
            cmd.Connection = conn;
            cmd.CommandText = sql;

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        utilisateur.nom = reader.GetString(0);
                        utilisateur.prenom = reader.GetString(1);
                        utilisateur.email = reader.GetString(2);
                        utilisateur.motdepasse = reader.GetString(3);
                        utilisateur.adresse = reader.GetString(4);
                        //string date = reader.GetString(5);
                        utilisateur.ville = reader.GetString(6);
                        utilisateur.pays = reader.GetString(7);


                        nom.Text = utilisateur.nom;
                        prenom.Text = utilisateur.prenom;
                        email.Text = utilisateur.email;
                        motdepasse.Text = utilisateur.motdepasse;
                        adresse.Text = utilisateur.adresse;
                        //datenaissance.Text = date;
                        ville.Text = utilisateur.ville;
                        pays.Text = utilisateur.pays;

                    }
                }

                conn.Close();
            }
        }

        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            ID_user_connected = int.Parse(textbox_ID.Text);
            conn.Open();
            string sql = "UPDATE `users` SET `nom`='"+ nom.Text +"',`prenom`='"+ prenom.Text +"',`email`='"+ email.Text +"',`motdepasse`='"+ motdepasse.Text +"',`adresse`='"+ adresse.Text +"', `ville`='"+ ville.Text +"',`pays`='"+ pays.Text +"' WHERE ID_user = "+ ID_user_connected + "";
            //`datenaissance`='"+ datenaissance.Text +"',

            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.ExecuteNonQuery();

            conn.Close();
            Profil.Visibility = Visibility.Hidden;
        }
        private void annuler_mofificationprofil_click(object sender, RoutedEventArgs e)
        {
                MainStack.IsEnabled = true;
                Profil.Visibility = Visibility.Hidden;
        }

        private void deconnexion_click(object sender, MouseButtonEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Close();
            main.Show();
        }

        private void suivre_click(object sender, RoutedEventArgs e)
        {
            ID_user_connected = int.Parse(textbox_ID.Text);

            conn.Open();
            string sql = "INSERT INTO `projet_suivi` (`id_user_suiveur`, `id_projet_suivi`, `Date_suivi`) VALUES ('"+ ID_user_connected + "', '"+ ID_projet +"', current_timestamp())";
            //`datenaissance`='"+ datenaissance.Text +"',

            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        
    }

}   