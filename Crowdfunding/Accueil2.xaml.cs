﻿using System;
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
using System.Globalization;

namespace Crowdfunding
{
    /// <summary>
    /// Logique d'interaction pour Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {

        MySqlConnection conn = new MySqlConnection("SERVER=127.0.0.1; DATABASE='crowdfunding'; UID=root; PASSWORD=");

        ObservableCollection<Projet> liste = new ObservableCollection<Projet>();
        ObservableCollection<Projet> listeaccueil = new ObservableCollection<Projet>();
        ObservableCollection<Projet> liste_mes_cagnottes = new ObservableCollection<Projet>(); //liste pour la listeview "mes cagnottes"
        ObservableCollection<Projet> liste_mes_investissements = new ObservableCollection<Projet>();//liste pour la listeview "mes investissements"
        ObservableCollection<Projet> liste_projet_suivi = new ObservableCollection<Projet>();//liste pour la listview "mes projets suivis"

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

        //variable : création cagnotte
        public string path1 = "";
        public string nomFichier1 = "";
        public string path2 = "";
        public string nomFichier2 = "";
        public string path3 = "";
        public string nomFichier3 = "";
        public int id_categ = 0;
        

        public Window2(int id)
        {
            this.ID_user_connected = id;
            InitializeComponent();
            affichageListeProjet();
            chargementCheckbox();
            affichageListeProjetUser();

            //les statistiques sur l'acceuil
            Total_investissement();
            somme_investi();

            //les statistiques dans "mes cagnottes"
            stat_pp();


            //afficher la liste des projets investis
            affichage_mes_investissement();

            //afficher la liste des projets suivis dans "mes investissements"
            affichageProjetSuivi();
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
                        listeaccueil.Add(new Projet()
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

                        listeProjet.ItemsSource = listeaccueil;
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
            //ID_user_connected = int.Parse(ID_user_connected);



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
                        liste_mes_cagnottes.Add(new Projet()
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

                        listeProjet2.ItemsSource = liste_mes_cagnottes;
                    }
                    reader.Close();
                }
            }
            conn.Close();
        }


        //Affichage de la liste des projets investis dans fenêtre "mes investissements"
        private void affichage_mes_investissement()
        {
            conn.Open();

            //il faut encore changer l'ID en paramètre - Standardiser

            String sql = "SELECT titreprojet,descriptionProjet, objectifCagnotte, statut, date_fermeture_cagnotte, date_debut_paiement,image_projet, SUM(investisssement.montant_investissement) FROM `projet` inner JOIN investisssement on projet.ID_projet=investisssement.reference_projet inner JOIN users on investisssement.fk_id_user_invest=users.ID_user and users.ID_user="+ ID_user_connected +" GROUP by titreprojet;";


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
                        String imgpath = reader.GetString(6);

                        String img = imgpath.Replace("\\", "#");
                        String[] im = img.Split('#');
                        String photo = im[2]; //MessageBox.Show(photo);

                        //ajout de donnée à afficher dans la liste
                        liste_mes_investissements.Add(new Projet()
                        {
                            titre = reader.GetString(0),
                            description = reader.GetString(1),
                            sommeInvesti = reader.GetFloat(7),
                            objectifCagnotte = reader.GetFloat(2),
                            statut = reader.GetString(3),
                            image = photo,
                            paiement = reader.GetDateTime(5),
                            fermeture = reader.GetDateTime(4)
                        });


                    }
                    reader.Close();
                }

                //trie de la liste obtenue à partir de la base de donnée
                //liste_mes_investissements.Sort(delegate (Projet x, Projet y)
                //{
                //    return x.titre.CompareTo(y.titre);
                //});
                projetInvesti.ItemsSource = liste_mes_investissements;
            }
            conn.Close();
        }

        //Affichage de la liste des projets suivi dans fenêtres "mes investissements"
        private void affichageProjetSuivi()
        {
            //connexion à la base de donnée
            conn = new MySqlConnection("SERVER=127.0.0.1; DATABASE='crowdfunding'; UID=root; PASSWORD=");
            conn.Open();

            //il faut encore changer l'ID en paramètre - Standardiser

            String sql = "SELECT * FROM projet, users, projet_suivi WHERE projet.ID_projet = projet_suivi.id_projet_suivi AND projet_suivi.id_user_suiveur = users.ID_user AND users.ID_user = "+ ID_user_connected;


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
                        String photo = im[2]; //MessageBox.Show(photo);

                        //ajout de donnée à afficher dans la liste
                        liste_projet_suivi.Add(new Projet()
                        {
                            IdProjet = reader.GetInt16(0),
                            titre = reader.GetString(3),
                            description = reader.GetString(4),
                            objectifCagnotte = reader.GetFloat(6),
                            statut = reader.GetString(9),
                            image = photo,
                            paiement = reader.GetDateTime(12),
                            fermeture = reader.GetDateTime(11)
                        });


                    }
                    reader.Close();
                }

                //trie de la liste obtenue à partir de la base de donnée
                //listeSuivi.Sort(delegate (Projet x, Projet y)
                //{
                //    return x.titre.CompareTo(y.titre);
                //});

                projetSuivi.ItemsSource = liste_projet_suivi;

            }
            conn.Close();
        }
        private void Button_Click_Accueil(object sender, RoutedEventArgs e)
        {
            Accueil.BorderBrush = new SolidColorBrush(Colors.White);
            Cagnotte.BorderBrush = new SolidColorBrush(Colors.Transparent);
            Investissement.BorderBrush = new SolidColorBrush(Colors.Transparent);
            boutton_User.BorderBrush = new SolidColorBrush(Colors.Transparent);

            // utilisateur.Foreground = new SolidColorBrush(Colors.White);

            //gestion des fenêtres

            boutton_User.Foreground = new SolidColorBrush(Colors.White);

            listeProjet.Items.Refresh();

            listeProjet2.IsEnabled = false;
            Accueil1.Visibility = Visibility.Visible;
            Cagnottes.Visibility = Visibility.Hidden;
            GridCreation.Visibility = Visibility.Hidden;
            grid_mes_investissements.Visibility = Visibility.Hidden;

        }

        private void Button_Click_cagnotte(object sender, RoutedEventArgs e)
        {
            Accueil.BorderBrush = new SolidColorBrush(Colors.Transparent);
            Cagnotte.BorderBrush = new SolidColorBrush(Colors.White);
            Investissement.BorderBrush = new SolidColorBrush(Colors.Transparent);
            boutton_User.BorderBrush = new SolidColorBrush(Colors.Transparent);

            listeProjet2.IsEnabled = true;

            //listeProjet2.Items.Refresh();
            
            Cagnottes.Visibility = Visibility.Visible;
            GridCreation.Visibility = Visibility.Hidden;
            grid_mes_investissements.Visibility = Visibility.Hidden;
        }
        private void Button_Click_investissement(object sender, RoutedEventArgs e)
        {
            Accueil.BorderBrush = new SolidColorBrush(Colors.Transparent);
            Cagnotte.BorderBrush = new SolidColorBrush(Colors.Transparent);
            Investissement.BorderBrush = new SolidColorBrush(Colors.White);
            boutton_User.BorderBrush = new SolidColorBrush(Colors.Transparent);


            Cagnottes.Visibility = Visibility.Hidden;
            Accueil1.Visibility = Visibility.Hidden;
            grid_mes_investissements.Visibility = Visibility.Visible;


            boutton_User.Foreground = new SolidColorBrush(Colors.White);

            flotanteProjet_investi.Visibility = Visibility.Hidden;


        }

        private void Button_Click_notification(object sender, RoutedEventArgs e)
        {
            Accueil.BorderBrush = new SolidColorBrush(Colors.Transparent);
            Cagnotte.BorderBrush = new SolidColorBrush(Colors.Transparent);
            Investissement.BorderBrush = new SolidColorBrush(Colors.Transparent);
            boutton_User.BorderBrush = new SolidColorBrush(Colors.Transparent); ;
        }

        private void Button_Click_User(object sender, RoutedEventArgs e)
        {

            if (deroulante_Projet.Visibility == Visibility.Hidden)
                deroulante_Projet.Visibility = Visibility.Visible;
            else
                deroulante_Projet.Visibility = Visibility.Hidden;


            Accueil.BorderBrush = new SolidColorBrush(Colors.Transparent);
            Cagnotte.BorderBrush = new SolidColorBrush(Colors.Transparent);
            Investissement.BorderBrush = new SolidColorBrush(Colors.Transparent);
            boutton_User.BorderBrush = new SolidColorBrush(Colors.White);


        }

        private void ButtonClick_creation_cagnotte(object sender, RoutedEventArgs e)
        {
            listeProjet2.Items.Refresh();
            Cagnottes.Visibility = Visibility.Hidden;
            GridCreation.Visibility = Visibility.Visible;
        }

        private void fermer_click(object sender, MouseButtonEventArgs e)
        {

            //flotanteProjet.Visibility = Visibility.Hidden;
            Recherche.Visibility = Visibility.Visible;
            listeProjet2.Items.Refresh();
            listeProjet.IsEnabled = true;
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
            //prendre les valeurs entrées par l'utilisateur qui créé une cagnotte
            string title = titreprojet.Text;
            string titre = title.Replace("'", "\''");
            string desk = descriptionProjet.Text;
            string description = desk.Replace("'", "\''");
            float objectif = float.Parse(objectifCagnotte.Text);
            DateTime date_fin = (DateTime)date_fermeture_cagnotte.SelectedDate;
            string result_fin = date_fin.ToString("yyyy-MM-dd");
            DateTime date_debut = (DateTime)date_debut_dividende.SelectedDate;
            string result_debut = date_debut.ToString("yyyy-MM-dd");

            string src1 = @path1;
            string dest1 = @"dossiers\BP\" + nomFichier1 + "";
            string src2 = @path2;
            string dest2 = @"dossiers\CIN\" + nomFichier2 + "";
            string src3 = @path3;
            string dest3 = @"dossiers\IMG\" + nomFichier3 + "";
            string dest1Req = dest1.Replace("\\", "\\\\");
            string dest2Req = dest2.Replace("\\", "\\\\");
            string dest3Req = dest3.Replace("\\", "\\\\");

            try
            {
                File.Copy(src1, dest1);

            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }
            try
            {
                File.Copy(src2, dest2);
            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }
            try
            {
                File.Copy(src3, dest3);
            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }

            conn.Open();
            //récupérer ID_categ à partir de la catégorie sélectionnée
            string requette1 = "SELECT * FROM `categorie` WHERE nom_categ = '" + comboboxCateg.Text + "' ";
            MySqlCommand cmd1 = new MySqlCommand(requette1, conn);
            MySqlDataReader myReader1;
            myReader1 = cmd1.ExecuteReader();
            while (myReader1.Read())
            {
                id_categ = myReader1.GetInt32(0);
            }

            conn.Close();


            //injection requette
            conn.Open();


            string requette = "INSERT INTO `projet_a_valider` (`ID_projet`, `fk_id_user_projet`, `fk_id_categ_projet`, `titreprojet`, `descriptionProjet`, `sommeCagnotte`, `objectifCagnotte`, `fichierBP`, `pieceidentite`, `statut`, `date_ouverture_cagnotte`, `date_fermeture_cagnotte`, `date_debut_paiement`, `image_projet`) VALUES(NULL, '1', '" + id_categ + "', '" + titre + "', '" + description + "', 0, '" + objectif + "', '" + dest1Req + "', '" + dest2Req + "', 'Ouvert', current_timestamp(), '" + result_fin + "', '" + result_debut + "','" + dest3Req + "')";

            MySqlCommand cmd = new MySqlCommand(requette, conn);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            conn.Close();

            //remise à zero et message de confirmation
            RAZ();

            //cacher la fenêtre création cagnotte et retour sur mes cagnottes
            MessageBox.Show("Le projet " + title + " a été enregistré.");
            GridMainCagnotte.Visibility = Visibility.Visible;
            GridCreation.Visibility = Visibility.Hidden;
            listeProjet2.Items.Refresh();
            Cagnottes.Visibility = Visibility.Visible;
        }

        private void Button_Click_annuler(object sender, RoutedEventArgs e)
        {
            // effacer les champs
            RAZ();

            //cacher la grid création cagnotte
            GridCreation.Visibility = Visibility.Hidden;

            //rendre la listeview enable
            listeProjet2.IsEnabled = true;

            //refresh la liste
            listeProjet2.Items.Refresh();

            //faire apparaitre la grid mes cagnottes
            Cagnottes.Visibility = Visibility.Visible;
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

            Recherche.Visibility = Visibility.Hidden;

            //selectionner l'Item clické
            string curentItem = listeProjet.SelectedItem.ToString();
            int index = listeProjet.SelectedIndex;
            //Affecter les valeurs de l'Item clické dans des variables à l'aide de Getter

            ID_projet = listeaccueil[index].getIdProjet();
            titreSelected = listeaccueil[index].getTitre();
            descriptionSelected = listeaccueil[index].getDescription();
            sommeCagnotteSelected = listeaccueil[index].getSommeCagnotte().ToString();
            objectifCagnotteSelected = listeaccueil[index].getObjectifCagnotte().ToString();
            statutSelected = listeaccueil[index].getStatut();
            imageSelected = listeaccueil[index].getImage();
            ouvertureSelected = listeaccueil[index].getOuverture().ToString();
            fermetureSelected = listeaccueil[index].getFermeture().ToString();


            //Faire apparaitre la fenêtre flottante et Désactiver la listeView pour qu'on ne peut pas la toucher
            flotanteProjet.Visibility = Visibility.Visible;
            Recherche.Visibility = Visibility.Hidden;

            Accueil2.IsEnabled = false;

            //Initialisation du bouton suivi
            //Envoie requête pour voir si le projet est déjà suivi dans la base de donnée
            conn.Open();
            string sql = "SELECT * FROM `projet_suivi` WHERE `id_user_suiveur` = " + ID_user_connected + " AND `id_projet_suivi` =" + ID_projet + "";
            //`datenaissance`='"+ datenaissance.Text +"',

            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.ExecuteNonQuery();

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    bouttonSuivre.Content = "Suivi";
                }
                else bouttonSuivre.Content = "Suivre";
                reader.Close();
            }
            conn.Close();

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
            //string curentItem = listeProjet.SelectedItem.ToString();
            index = listeProjet2.SelectedIndex;
            flottanteProjet flottanteProjet = new flottanteProjet();
            flottanteProjet.Activate();

            flotanteProjet.Visibility = Visibility.Hidden;
            flotanteInvestir.Visibility = Visibility.Visible;


        }

        private void inserer_bp(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "word files (*.docx)|*.docx|pdf files (*.pdf)|*.pdf|text files (*.txt)|*.txt|All files (*.*)|*.*";

            bool? result = fileDialog.ShowDialog();
            if (result == true)
            {
                path1 = fileDialog.FileName;
                nomFichier1 = Path.GetFileName(path1);
                uploadBP_nom.Text = nomFichier1;
            }
        }

        private void inserer_cin(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog2 = new OpenFileDialog();
            fileDialog2.Filter = "images files (*.jpg)|*.jpg|word files (*.docx)|*.docx|pdf files (*.pdf)|*.pdf|text files (*.txt)|*.txt|All files (*.*)|*.*";

            bool? result = fileDialog2.ShowDialog();
            if (result == true)
            {
                path2 = fileDialog2.FileName;
                nomFichier2 = Path.GetFileName(path2);
                uploadCIN_nom.Text = nomFichier2;
            }
        }

        private void photoC_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog3 = new OpenFileDialog();
            fileDialog3.Filter = "images files (*.jpg)|*.jpg|images files (*.png)|*.png|All files (*.*)|*.*";

            bool? result = fileDialog3.ShowDialog();
            if (result == true)
            {
                path3 = fileDialog3.FileName;
                nomFichier3 = Path.GetFileName(path3);
                uploadImage_nom.Text = nomFichier3;
            }
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

        //flottante investir dans accueil 2
        private void Payer_Click(object sender, RoutedEventArgs e)
        {

            sommeInvest = sommeInvesti.Text;
            //int index = listeProjet.SelectedIndex;
            //MessageBox.Show(index.ToString());

            ////Affecter les valeurs de l'Item clické dans des variables à l'aide de Getter
            //ID_projet = listeaccueil[index].getIdProjet();
            //Connexion vers BDD
            conn = new MySqlConnection("SERVER=127.0.0.1; DATABASE='crowdfunding'; UID=root; PASSWORD=");
            conn.Open();
            String sql = "INSERT INTO `investisssement` (`ID_investissement`, `fk_id_user_invest`, `montant_investissement`, `date_investissement`, `fk_ID_paiement_invest`, `reference_projet`) VALUES (NULL, '" + ID_user_connected + "', '" + sommeInvest + "', current_timestamp(), '" + ID_paiement_invest + "', '" + ID_projet + "')";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            flotanteInvestir.Visibility = Visibility.Hidden;
            Recherche.Visibility = Visibility.Visible;
            listeProjet.IsEnabled = true;
            Accueil2.IsEnabled = true;
            conn.Close();

            MessageBox.Show("Investissement fait");
            listeProjet.IsEnabled = true;
            Accueil2.IsEnabled = true;

        }

        //MES CAGNOTTES
        private void selectionner_projet(object sender, SelectionChangedEventArgs e)
        {
            //desactiver la listview
            listeProjet2.IsEnabled = false;
            //Cagnotte.Visibility = Visibility.Hidden;
            grid_vue_projet.Visibility = Visibility.Visible;

            //selectionner l'Item clické
            string curentItem = listeProjet2.SelectedItems.ToString(); //prends la ligne séléctionnée dans listview
            int index = listeProjet2.SelectedIndex;

            //Affecter les valeurs de l'Item clické dans des variables à l'aide de Getter
            ID_projet = liste_mes_cagnottes[index].getIdProjet();
            titreSelected = liste_mes_cagnottes[index].getTitre();
            descriptionSelected = liste_mes_cagnottes[index].getDescription();
            sommeCagnotteSelected = liste_mes_cagnottes[index].getSommeCagnotte().ToString();
            objectifCagnotteSelected = liste_mes_cagnottes[index].getObjectifCagnotte().ToString();
            statutSelected = liste_mes_cagnottes[index].getStatut();
            imageSelected = liste_mes_cagnottes[index].getImage();
            ouvertureSelected = liste_mes_cagnottes[index].getOuverture().ToString();
            fermetureSelected = liste_mes_cagnottes[index].getFermeture().ToString();

            //Afficher les valeurs
            title.Text = titreSelected;
            descrip.Text = descriptionSelected;
            objectif.Text = objectifCagnotteSelected;
            etat.Text = statutSelected;
            ouvert.Text = ouvertureSelected;
            ferme.Text = fermetureSelected;
            //labelTitre2.Content = titreSelected;
            somme.Text = sommeCagnotteSelected;
            int statprojet = 100 * int.Parse(sommeCagnotteSelected) / int.Parse(objectifCagnotteSelected);
            progress.Value = statprojet;
            LabelProgression.Content = statprojet + " % de votre cagnotte ont été atteint";
        }

        //fonction qui calcul le nombre d'investissement fait et la somme totale des investissements
        private void Total_investissement()
        {
            //connexion à la base de donnée
            conn = new MySqlConnection("SERVER=127.0.0.1; DATABASE='crowdfunding'; UID=root; PASSWORD=");
            conn.Open();

            //il faut encore changer l'ID en paramètre - Standardiser
            String sql = "SELECT COUNT(id_investissement),SUM(montant_investissement) from investisssement";

            //creat command
            MySqlCommand cmd = new MySqlCommand();

            //Etablir la connexion de la commande
            cmd.Connection = conn;
            cmd.CommandText = sql;

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();

                    int a = reader.GetInt16(0);
                    nombre_invest.Text = a.ToString();
                    total_invest.Text = reader.GetFloat(1).ToString();
                }

            }
            conn.Close();
        }


        //fonction qui affiche le nombre de projet enregistré
        private void somme_investi()
        {
            //connexion à la base de donnée
            conn = new MySqlConnection("SERVER=127.0.0.1; DATABASE='crowdfunding'; UID=root; PASSWORD=");
            conn.Open();

            //il faut encore changer l'ID en paramètre - Standardiser
            String sql = "SELECT COUNT(id_projet) from projet";

            //creat command
            MySqlCommand cmd = new MySqlCommand();

            //Etablir la connexion de la commande
            cmd.Connection = conn;
            cmd.CommandText = sql;

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    nombre_projet.Text = reader.GetInt16(0).ToString();
                }
            }
            conn.Close();
        }


        //Section porteur de projet : fonction qui affiche le nombre de projet enregistré et la somme collectée
        private void stat_pp()
        {
            //connexion à la base de donnée
            conn = new MySqlConnection("SERVER=127.0.0.1; DATABASE='crowdfunding'; UID=root; PASSWORD=");
            conn.Open();

            //il faut encore changer l'ID en paramètre - Standardiser
            String sql = "SELECT COUNT(ID_projet), SUM(sommeCagnotte) FROM projet WHERE fk_id_user_projet=" + ID_user_connected;
            
            //creat command
            MySqlCommand cmd = new MySqlCommand();

            //Etablir la connexion de la commande
            cmd.Connection = conn;
            cmd.CommandText = sql;

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    
                    if(reader.GetInt16(0) == 0)
                    {
                        //nb_projet_pp.Text = "0";
                        //somme_collecte.Text = "0";
                    }
                    else
                    {
                        //nb_projet_pp.Text = reader.GetInt16(0).ToString();
                        //somme_collecte.Text = reader.GetFloat(1).ToString();
                    }                    
                }
            }         
            conn.Close();
        }


        private void modification_description(object sender, TextChangedEventArgs e)
        {
         

        }


        //MON PROFIL

        private void MonProfil_click(object sender, MouseButtonEventArgs e)
        {

            if(Accueil1.IsEnabled == true)
                Accueil1.IsEnabled = false;
            else
                Accueil1.IsEnabled = true;
            

            
            if (Profil.Visibility == Visibility.Hidden)
                Profil.Visibility = Visibility.Visible;
            else
                Profil.Visibility = Visibility.Hidden;

            User utilisateur = new User();
            //ID_user_connected = int.Parse(ID_connected.Text);
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
                        utilisateur.datenaissance = reader.GetDateTime(5).Date;
                        utilisateur.ville = reader.GetString(6);
                        utilisateur.pays = reader.GetString(7);

                        nom.Text = utilisateur.nom;
                        prenom.Text = utilisateur.prenom;
                        email.Text = utilisateur.email;
                        //motdepasse.Text = utilisateur.motdepasse;
                        adresse.Text = utilisateur.adresse;
                        datenaissance.SelectedDate = utilisateur.datenaissance.Date;
                        ville.Text = utilisateur.ville;
                        pays.Text = utilisateur.pays;

                        dateok.Text = utilisateur.datenaissance.ToString("yyyy-MM-dd");
                        
                        
                    }
                }

                conn.Close();
            }
        }

        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            conn.Open();
            string sql = "UPDATE `users` SET `nom`='"+ nom.Text +"',`prenom`='"+ prenom.Text + "',`email`='" + email.Text + "',`adresse`='" + adresse.Text +"', `ville`='"+ ville.Text +"',`pays`='"+ pays.Text +"' WHERE ID_user = "+ ID_user_connected + "";
            

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            Profil.Visibility = Visibility.Hidden;
            MainStack.IsEnabled = true;

        }
        private void annuler_mofificationprofil_click(object sender, RoutedEventArgs e)
        {
                Accueil1.IsEnabled = true;
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
            conn.Open();
            string sql = "SELECT * FROM `projet_suivi` WHERE `id_user_suiveur` = " + ID_user_connected + " AND `id_projet_suivi` =" + ID_projet + "";
            
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.ExecuteNonQuery();

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    bouttonSuivre.Content = "Suivi";
                }
                else
                {
                    MySqlConnection conn2 = new MySqlConnection("SERVER=127.0.0.1; DATABASE='crowdfunding'; UID=root; PASSWORD=");
                    conn2.Open();
                    string sql1 = "INSERT INTO `projet_suivi` (`id_user_suiveur`, `id_projet_suivi`, `Date_suivi`) VALUES ('" + ID_user_connected + "', '" + ID_projet + "', current_timestamp())";
                    
                    MySqlCommand cmd_envoie = new MySqlCommand(sql1, conn2);

                    cmd_envoie.ExecuteNonQuery();
                    conn2.Close();
                }
                reader.Close();
            }

            conn.Close();
            
            bouttonSuivre.Content = "Suivi";
        }

        private void modification_titre(object sender, TextChangedEventArgs e)
        {
            //liste_mes_cagnottes[index].titre = title.Text;

            //string requete = " UPDATE `projet` SET `titreprojet` = '" + liste_mes_cagnottes[index].titre + "' WHERE `projet`.`ID_projet` = " + liste_mes_cagnottes[index].IdProjet;
            ////connexion à la base de donnée
            //conn = new MySqlConnection("SERVER=127.0.0.1; DATABASE='crowdfunding'; UID=root; PASSWORD=");
            
            //conn.Open();

            ////creat command
            //MySqlCommand cmd = new MySqlCommand();

            ////Etablir la connexion de la commande
            //cmd.Connection = conn;
            //cmd.CommandText = requete;

            //try
            //{
            //    cmd.ExecuteNonQuery();
            //}
            //catch (Exception excep)
            //{
            //    MessageBox.Show(excep.ToString());
            //}
            //conn.Close();
        }
        //private void modification_description(object sender, TextChangedEventArgs e)
        //{
        //    //liste_mes_cagnottes[index].description = descrip.Text;

        //    //string requete = " UPDATE `projet` SET `descriptionProjet` = '" + liste_mes_cagnottes[index].description + "' WHERE `projet`.`ID_projet` = " + liste_mes_cagnottes[index].IdProjet;


        //    ////connexion à la base de donnée
        //    //conn = new MySqlConnection("SERVER=127.0.0.1; DATABASE='crowdfunding'; UID=root; PASSWORD=");
        //    //conn.Open();

        //    //MySqlCommand cmd = new MySqlCommand();
        //    //cmd.Connection = conn;
        //    //cmd.CommandText = requete;

        //    ////Exécution
        //    //try
        //    //{
        //    //    cmd.ExecuteNonQuery();
        //    //}
        //    //catch (Exception excep)
        //    //{
        //    //    MessageBox.Show(excep.ToString());
        //    //}
        //    //conn.Close();
        //}


        private void progress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

       

        private void valider_changement_cagnotte(object sender, RoutedEventArgs e)
        {
            string title_changed = title.Text;
            string descrip_changed = descrip.Text;
            liste_mes_cagnottes[index].description = descrip.Text;

            string requete = "UPDATE `projet` SET `titreprojet`='"+ title_changed +"',`descriptionProjet`='"+ descrip_changed + "' WHERE `projet`.`ID_projet` = " +(int)(liste_mes_cagnottes[index].IdProjet)+"";
            conn = new MySqlConnection("SERVER=127.0.0.1; DATABASE='crowdfunding'; UID=root; PASSWORD=");
            conn.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = requete;

            //Exécution
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.ToString());
            }
            conn.Close();

            listeProjet2.Items.Refresh();

            //Faire apparaitre la fenêtre listeView et Désactiver la flottante
            grid_vue_projet.Visibility = Visibility.Hidden;
            listeProjet2.IsEnabled = true;
        }

        private void annuler_changement_cagnotte(object sender, RoutedEventArgs e)
        {
            listeProjet2.Items.Refresh();

            //Faire apparaitre la fenêtre listeView et Désactiver la flottante
            grid_vue_projet.Visibility = Visibility.Hidden;
            listeProjet2.IsEnabled = true;
        }

        //afficher les détails du projet investi lors du click dans "mes investissements"
        private void afficher_investi(object sender, SelectionChangedEventArgs e)
        {
            //selectionner l'Item clické dans les projets investis
            flotanteProjet_investi.Visibility = Visibility.Visible;
            grid_mes_investissements.IsEnabled = false;

            //selectionner l'Item clické
            string curentItem = projetInvesti.SelectedItem.ToString();
            int index = projetInvesti.SelectedIndex;
            //Affecter les valeurs de l'Item clické dans des variables à l'aide de Getter

            ID_projet = liste_mes_investissements[index].getIdProjet();
            titreSelected = liste_mes_investissements[index].getTitre();
            descriptionSelected = liste_mes_investissements[index].getDescription();
            sommeCagnotteSelected = liste_mes_investissements[index].getSommeCagnotte().ToString();
            objectifCagnotteSelected = liste_mes_investissements[index].getObjectifCagnotte().ToString();
            statutSelected = liste_mes_investissements[index].getStatut();
            imageSelected = liste_mes_investissements[index].getImage();
            ouvertureSelected = liste_mes_investissements[index].getOuverture().ToString();
            fermetureSelected = liste_mes_investissements[index].getFermeture().ToString();

            //Faire apparaitre la fenêtre flottante et Désactiver la listeView pour qu'on ne peut pas la toucher
            flotanteProjet_investi.Visibility = Visibility.Visible;
            //Recherche.Visibility = Visibility.Hidden;

            //grid_mes_investissements.IsEnabled = false;

            //Afficher les valeurs
            labelTitre_investi.Content = titreSelected;
            labelDescription_investi.Content = descriptionSelected;
            labelObjectifCagnotte_investi.Content = objectifCagnotteSelected;
            labelSomme_investi.Content = liste_mes_investissements[index].getSommeInvesti();
            labelStatut_investi.Content = statutSelected;
            //labelSomme_collectee_investi.Content = liste_mes_investissements[index].ge
            //labelOuverture_investi.Content = ouvertureSelected;
            labelFermeture_investi.Content = fermetureSelected;
        }

        //afficher les détails du projet suivi lors du click dans "mes investissements"
        private void afficher_flottante_suivi(object sender, SelectionChangedEventArgs e)
        {

        }

        private void fermer_fenetre_invesi(object sender, MouseButtonEventArgs e)
        {
            flotanteProjet_investi.Visibility = Visibility.Hidden;
            grid_mes_investissements.IsEnabled = true;
        }
    }
}   