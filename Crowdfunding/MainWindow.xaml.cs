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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace Crowdfunding
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MySqlConnection conn;
        ObservableCollection<Projet> liste_acceuil = new ObservableCollection<Projet>();
        Login login = new Login();
        private int ID_paiement_invest = 0;
        private int ID_projet = 0;
        private string sommeInvest = "";
        

        public MainWindow()
        {
            InitializeComponent();
            acceuilPrincipal();
            
            login.Show();
            login.Visibility = Visibility.Hidden;
            Total_investissement();
            somme_investi();

        }
        private void acceuilPrincipal()
        {
            //connexion à la base de donnée
            conn = new MySqlConnection("SERVER=127.0.0.1; DATABASE='crowdfunding'; UID=root; PASSWORD=");
            conn.Open();
            //il faut encore changer l'ID en paramètre - Standardiser
            String sql = "SELECT * FROM projet;";
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
                        liste_acceuil.Add(new Projet()
                        {
                            IdProjet = reader.GetInt32(0),
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
                acceuil_projet.ItemsSource = liste_acceuil;
            }

           
        }
        

        private void investir(object sender, RoutedEventArgs e)
        {
            //Vers fenêtre veuillez vous connecter
            //Si connexion OK => fenetre investir
            //Window2 acc = new Window2();
            //acc.Show();
        }
        private void Button_Click_Accueil(object sender, RoutedEventArgs e)
        {
            Acceuilecrit.BorderBrush = new SolidColorBrush(Colors.Transparent);
            Guide.BorderBrush = new SolidColorBrush(Colors.Transparent);
            Apropos.BorderBrush = new SolidColorBrush(Colors.White);
            Inscription.BorderBrush = new SolidColorBrush(Colors.Transparent);
            Connexion.BorderBrush = new SolidColorBrush(Colors.Transparent);
            MainGrid.Visibility = Visibility.Visible;
            a_propos_de_nous.Visibility = Visibility.Hidden;
        }

        private void Button_Click_guide(object sender, RoutedEventArgs e)
        {
            Acceuilecrit.BorderBrush = new SolidColorBrush(Colors.Transparent);
            Guide.BorderBrush = new SolidColorBrush(Colors.White);
            Apropos.BorderBrush = new SolidColorBrush(Colors.Transparent);
            Inscription.BorderBrush = new SolidColorBrush(Colors.Transparent);
            Connexion.BorderBrush = new SolidColorBrush(Colors.Transparent);

        }

        private void Button_Click_Apropos(object sender, RoutedEventArgs e)
        {
            Acceuilecrit.BorderBrush = new SolidColorBrush(Colors.Transparent);
            Guide.BorderBrush = new SolidColorBrush(Colors.Transparent);
            Apropos.BorderBrush = new SolidColorBrush(Colors.White);
            Inscription.BorderBrush = new SolidColorBrush(Colors.Transparent);
            Connexion.BorderBrush = new SolidColorBrush(Colors.Transparent);

            //Espace_admin espaceadmin = new Espace_admin();
            //espaceadmin.Show();
            //this.Hide();

            //Connexion.Foreground = new SolidColorBrush(Colors.White);


            a_propos_de_nous.Visibility = Visibility.Visible;
            MainGrid.Visibility = Visibility.Hidden;

        }

        private void Button_Click_inscription(object sender, RoutedEventArgs e)
        {
            Acceuilecrit.BorderBrush = new SolidColorBrush(Colors.Transparent);
            Guide.BorderBrush = new SolidColorBrush(Colors.Transparent);
            Apropos.BorderBrush = new SolidColorBrush(Colors.Transparent);
            Inscription.BorderBrush = new SolidColorBrush(Colors.White);
            Connexion.BorderBrush = new SolidColorBrush(Colors.Transparent);
            Inscription inscription = new Inscription();
            inscription.Show();
            this.Close();
        }

        private void Button_Click_connexion(object sender, RoutedEventArgs e)
        {
            Acceuilecrit.BorderBrush = new SolidColorBrush(Colors.Transparent);
            Guide.BorderBrush = new SolidColorBrush(Colors.Transparent);
            Apropos.BorderBrush = new SolidColorBrush(Colors.Transparent);
            Inscription.BorderBrush = new SolidColorBrush(Colors.Transparent);
            Connexion.BorderBrush = new SolidColorBrush(Colors.White);
            ////afficher la deuxième accueil : accueil connecté

            login.Visibility = Visibility.Visible;
            this.Close();
            

        }

        private void ecobutton_Click(object sender, RoutedEventArgs e)
        {
            ecobutton.Background = new SolidColorBrush(Colors.MediumSeaGreen);
            ecobutton.Foreground = new SolidColorBrush(Colors.White);
            ecobutton.Background.Opacity = 100;

            agributton.Background = new SolidColorBrush(Colors.Transparent);
            agributton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            Techbutton.Background = new SolidColorBrush(Colors.Transparent);
            Techbutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            Immobutton.Background = new SolidColorBrush(Colors.Transparent);
            Immobutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            SanteButton.Background = new SolidColorBrush(Colors.Transparent);
            SanteButton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            SolidaireButton.Background = new SolidColorBrush(Colors.Transparent);
            SolidaireButton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            Autrebutton.Background = new SolidColorBrush(Colors.Transparent);
            Autrebutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            tousbutton.Background = new SolidColorBrush(Colors.Transparent);
            tousbutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            //recherche
            rechercheCategorie(5);
        }

        private void agributton_Click(object sender, RoutedEventArgs e)
        {
            agributton.Background = new SolidColorBrush(Colors.MediumSeaGreen);
            agributton.Foreground = new SolidColorBrush(Colors.White);
            agributton.Background.Opacity = 100;

            ecobutton.Background = new SolidColorBrush(Colors.Transparent);
            ecobutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            Techbutton.Background = new SolidColorBrush(Colors.Transparent);
            Techbutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            Immobutton.Background = new SolidColorBrush(Colors.Transparent);
            Immobutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            SanteButton.Background = new SolidColorBrush(Colors.Transparent);
            SanteButton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            SolidaireButton.Background = new SolidColorBrush(Colors.Transparent);
            SolidaireButton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            Autrebutton.Background = new SolidColorBrush(Colors.Transparent);
            Autrebutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            tousbutton.Background = new SolidColorBrush(Colors.Transparent);
            tousbutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            //recherche
            rechercheCategorie(2);
        }

        private void Techbutton_Click(object sender, RoutedEventArgs e)
        {
            Techbutton.Background = new SolidColorBrush(Colors.MediumSeaGreen);
            Techbutton.Foreground = new SolidColorBrush(Colors.White);
            Techbutton.Background.Opacity = 100;

            agributton.Background = new SolidColorBrush(Colors.Transparent);
            agributton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            ecobutton.Background = new SolidColorBrush(Colors.Transparent);
            ecobutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            Immobutton.Background = new SolidColorBrush(Colors.Transparent);
            Immobutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            SanteButton.Background = new SolidColorBrush(Colors.Transparent);
            SanteButton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            SolidaireButton.Background = new SolidColorBrush(Colors.Transparent);
            SolidaireButton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            Autrebutton.Background = new SolidColorBrush(Colors.Transparent);
            Autrebutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            tousbutton.Background = new SolidColorBrush(Colors.Transparent);
            tousbutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            //recherche
            rechercheCategorie(3);
        }

        private void Immobutton_Click(object sender, RoutedEventArgs e)
        {
            Immobutton.Background = new SolidColorBrush(Colors.MediumSeaGreen);
            Immobutton.Foreground = new SolidColorBrush(Colors.White);
            Immobutton.Background.Opacity = 100;

            agributton.Background = new SolidColorBrush(Colors.Transparent);
            agributton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            Techbutton.Background = new SolidColorBrush(Colors.Transparent);
            Techbutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            ecobutton.Background = new SolidColorBrush(Colors.Transparent);
            ecobutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            SanteButton.Background = new SolidColorBrush(Colors.Transparent);
            SanteButton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            SolidaireButton.Background = new SolidColorBrush(Colors.Transparent);
            SolidaireButton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            Autrebutton.Background = new SolidColorBrush(Colors.Transparent);
            Autrebutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            tousbutton.Background = new SolidColorBrush(Colors.Transparent);
            tousbutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            //recherche
            rechercheCategorie(1);
        }

        private void SanteButton_Click(object sender, RoutedEventArgs e)
        {
            SanteButton.Background = new SolidColorBrush(Colors.MediumSeaGreen);
            SanteButton.Foreground = new SolidColorBrush(Colors.White);
            SanteButton.Background.Opacity = 100;

            agributton.Background = new SolidColorBrush(Colors.Transparent);
            agributton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            Techbutton.Background = new SolidColorBrush(Colors.Transparent);
            Techbutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            Immobutton.Background = new SolidColorBrush(Colors.Transparent);
            Immobutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            ecobutton.Background = new SolidColorBrush(Colors.Transparent);
            ecobutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            SolidaireButton.Background = new SolidColorBrush(Colors.Transparent);
            SolidaireButton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            Autrebutton.Background = new SolidColorBrush(Colors.Transparent);
            Autrebutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            tousbutton.Background = new SolidColorBrush(Colors.Transparent);
            tousbutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            //recherche
            rechercheCategorie(6);
        }

        private void SolidaireButton_Click(object sender, RoutedEventArgs e)
        {
            SolidaireButton.Background = new SolidColorBrush(Colors.MediumSeaGreen);
            SolidaireButton.Foreground = new SolidColorBrush(Colors.White);
            SolidaireButton.Background.Opacity = 100;

            agributton.Background = new SolidColorBrush(Colors.Transparent);
            agributton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            Techbutton.Background = new SolidColorBrush(Colors.Transparent);
            Techbutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            Immobutton.Background = new SolidColorBrush(Colors.Transparent);
            Immobutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            SanteButton.Background = new SolidColorBrush(Colors.Transparent);
            SanteButton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            ecobutton.Background = new SolidColorBrush(Colors.Transparent);
            ecobutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            Autrebutton.Background = new SolidColorBrush(Colors.Transparent);
            Autrebutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            tousbutton.Background = new SolidColorBrush(Colors.Transparent);
            tousbutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            //recherche
            rechercheCategorie(4);
        }

        private void Autrebutton_Click(object sender, RoutedEventArgs e)
        {
            Autrebutton.Background = new SolidColorBrush(Colors.MediumSeaGreen);
            Autrebutton.Foreground = new SolidColorBrush(Colors.White);
            Autrebutton.Background.Opacity = 100;

            agributton.Background = new SolidColorBrush(Colors.Transparent);
            agributton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            Techbutton.Background = new SolidColorBrush(Colors.Transparent);
            Techbutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            Immobutton.Background = new SolidColorBrush(Colors.Transparent);
            Immobutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            SanteButton.Background = new SolidColorBrush(Colors.Transparent);
            SanteButton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            SolidaireButton.Background = new SolidColorBrush(Colors.Transparent);
            SolidaireButton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            ecobutton.Background = new SolidColorBrush(Colors.Transparent);
            ecobutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            tousbutton.Background = new SolidColorBrush(Colors.Transparent);
            tousbutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            //recherche
            rechercheCategorie(10);
        }

        private void tousbutton_Click(object sender, RoutedEventArgs e)
        {
            tousbutton.Background = new SolidColorBrush(Colors.MediumSeaGreen);
            tousbutton.Foreground = new SolidColorBrush(Colors.White);
            tousbutton.Background.Opacity = 100;

            agributton.Background = new SolidColorBrush(Colors.Transparent);
            agributton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            Techbutton.Background = new SolidColorBrush(Colors.Transparent);
            Techbutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            Immobutton.Background = new SolidColorBrush(Colors.Transparent);
            Immobutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            SanteButton.Background = new SolidColorBrush(Colors.Transparent);
            SanteButton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            SolidaireButton.Background = new SolidColorBrush(Colors.Transparent);
            SolidaireButton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            ecobutton.Background = new SolidColorBrush(Colors.Transparent);
            ecobutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            Autrebutton.Background = new SolidColorBrush(Colors.Transparent);
            Autrebutton.Foreground = new SolidColorBrush(Colors.DarkSlateGray);

            liste_acceuil.Clear();
            acceuilPrincipal();
        }

        private void acceuil_projet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //changement vues
            flotanteProjet.Visibility = Visibility.Visible;
            accueil.IsEnabled = false;

            //selectionner l'Item clické
            string curentItem = acceuil_projet.SelectedItem.ToString();
            int index = acceuil_projet.SelectedIndex;
            //Affecter les valeurs de l'Item clické dans des variables à l'aide de Getter
            string titreSelected = liste_acceuil[index].getTitre();
            string descriptionSelected = liste_acceuil[index].getDescription();
            string sommeCagnotteSelected = liste_acceuil[index].getSommeCagnotte().ToString();
            string objectifCagnotteSelected = liste_acceuil[index].getObjectifCagnotte().ToString();
            string statutSelected = liste_acceuil[index].getStatut();
            string imageSelected = liste_acceuil[index].getImage();
            string ouvertureSelected = liste_acceuil[index].getOuverture().ToString();
            string fermetureSelected = liste_acceuil[index].getFermeture().ToString();
            ID_projet = liste_acceuil[index].getIdProjet();

            
            //Afficher les valeurs
            labelTitre.Content = titreSelected;
            labelDescription.Content = descriptionSelected;
            labelObjectifCagnotte.Content = objectifCagnotteSelected;
            labelStatut.Content = statutSelected;
            labelOuverture.Content = ouvertureSelected;
            labelFermeture.Content = fermetureSelected;
        }

        private void Investir_click(object sender, RoutedEventArgs e)
        {
            login.Visibility = Visibility.Visible;
            this.Close();
        }

        private void fermer_click(object sender, MouseButtonEventArgs e)
        {
            flotanteProjet.Visibility = Visibility.Hidden;
            accueil.IsEnabled = true;
        }

        //fonction qui recherche par catégorie
        private void rechercheCategorie(int idcateg)
        {
            //réinitialiser la liste
            liste_acceuil.Clear();

            //connexion à la base de donnée
            conn = new MySqlConnection("SERVER=127.0.0.1; DATABASE='crowdfunding'; UID=root; PASSWORD=");
            conn.Open();

            //il faut encore changer l'ID en paramètre - Standardiser
            String sql = "SELECT * FROM projet WHERE projet.fk_id_categ_projet = " + idcateg;

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
                        liste_acceuil.Add(new Projet()
                        {
                            IdProjet = reader.GetInt32(0),
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
                acceuil_projet.ItemsSource = liste_acceuil;
            }
        }

        //fonction qui calcul le nombre d'investissement fait et la somme tatole des investissements
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

        }

        private void contacter_nous(object sender, RoutedEventArgs e)
        {
            if (gridContacte.Visibility == Visibility.Hidden)
            {
                gridContacte.Visibility = Visibility.Visible;
            }
            else
            {
                gridContacte.Visibility = Visibility.Hidden;
            }
        }
    }
}
