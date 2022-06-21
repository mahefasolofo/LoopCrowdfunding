using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.IO;
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
using Path = System.IO.Path;

namespace Crowdfunding
{
    /// <summary>
    /// Logique d'interaction pour Window3.xaml
    /// </summary>
    
    public partial class Window3 : Window
    {
        MySqlConnection conn;
        public string path1 = "";
        public string nomFichier1 = "";
        public string path2 = "";
        public string nomFichier2 = "";
        public string path3 = "";
        public string nomFichier3 = "";
        public int id_categ = 0;
        string curentItem;
        int index;

        ObservableCollection<Projet> liste = new ObservableCollection<Projet>();

        

        public Window3()
        {
            InitializeComponent();
            conn = new MySqlConnection("SERVER=127.0.0.1; DATABASE='crowdfunding'; UID=root; PASSWORD=");

            chargementCheckbox();
            //affichageListeProjet();
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

        private void ButtonClick_creation_cagnotte(object sender, RoutedEventArgs e)
        {
            //GridMainCagnotte.Visibility = Visibility.Hidden;
            //GridCreation.Visibility = Visibility.Visible;
        }

        private void Button_Click_User(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Accueil(object sender, RoutedEventArgs e)
        {
            //Window2 accueil2 = new Window2();
            //accueil2.Show();
            //this.Hide();
        }

        private void Button_Click_cagnotte(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_investissement(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_notification(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_valider(object sender, RoutedEventArgs e)
        {
            //prendre les valeurs entrées par l'utilisateur
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

            string requette = "INSERT INTO `projet` (`ID_projet`, `fk_id_user_projet`, `fk_id_categ_projet`, `titreprojet`, `descriptionProjet`, `sommeCagnotte`, `objectifCagnotte`, `fichierBP`, `pieceidentite`, `statut`, `date_ouverture_cagnotte`, `date_fermeture_cagnotte`, `date_debut_paiement`, `image_projet`) VALUES(NULL, '1', '" + id_categ + "', '" + titre + "', '" + description + "', NULL, '" + objectif + "', '" + dest1Req + "', '" + dest2Req + "', 'Ouvert', current_timestamp(), '" + result_fin + "', '" + result_debut + "','" + dest3Req + "')";

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
            //cacher la fenêtre création cagnotte et retour sue mes cagnottes
            MessageBox.Show("Le projet " + title + " a été enregistré.");
            //GridMainCagnotte.Visibility = Visibility.Visible;
            //GridCreation.Visibility = Visibility.Hidden;
        }

        private void uploadBP_Click(object sender, RoutedEventArgs e)
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

        private void uploadCIN_Click(object sender, RoutedEventArgs e)
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

        private void uploadImage_Click(object sender, RoutedEventArgs e)
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

        private void Button_Click_annuler(object sender, RoutedEventArgs e)
        {
            RAZ();
            //cacher la fenêtre création cagnotte et retour sue mes cagnottes
            //GridMainCagnotte.Visibility = Visibility.Visible;
            GridCreation.Visibility = Visibility.Hidden;

        }
        //private void affichageListeProjet()
        //{
        //    //connexion à la base de donnée
        //    conn = new MySqlConnection("SERVER=127.0.0.1; DATABASE='crowdfunding'; UID=root; PASSWORD=");
        //    conn.Open();


        //    String sql = "SELECT * FROM `projet` ";

        //    //creat command
        //    MySqlCommand cmd = new MySqlCommand();

        //    //Etablir la connexion de la commande
        //    cmd.Connection = conn;
        //    cmd.CommandText = sql;

        //    using (DbDataReader reader = cmd.ExecuteReader())
        //    {
        //        if (reader.HasRows)
        //        {
        //            while (reader.Read())
        //            {   //lecture de l'image et conversion du nom            
        //                String imgpath = reader.GetString(13);

        //                String img = imgpath.Replace("\\", "#");
        //                String[] im = img.Split('#');
        //                String photo = im[2]; //MessageBox.Show(photo);

        //                //ajout de donnée à afficher dans la liste
        //                liste.Add(new Projet()
        //                {
        //                    IdProjet = reader.GetInt16(0),
        //                    titre = reader.GetString(3),
        //                    description = reader.GetString(4),
        //                    sommeCagnotte = reader.GetFloat(5),
        //                    objectifCagnotte = reader.GetFloat(6),
        //                    statut = reader.GetString(9),
        //                    image = photo,
        //                    ouverture = reader.GetDateTime(10),
        //                    fermeture = reader.GetDateTime(11)
        //                });

        //                                    }
        //            reader.Close();
        //        }
        //    }
        //    listeProjet.ItemsSource = liste;
        //}

        //private void selectionner_projet(object sender, SelectionChangedEventArgs e)
        //{
        //    //selectionner l'Item clické
        //    curentItem = liste.SelectedItem.ToString();
        //    index = listeProjet.SelectedIndex;

        //    //Affecter les valeurs de l'Item clické dans des variables à l'aide de Getter
        //    string titreSelected = liste[index].getTitre();
        //    string descriptionSelected = liste[index].getDescription();
        //    string sommeCagnotteSelected = liste[index].getSommeCagnotte().ToString();
        //    string objectifCagnotteSelected = liste[index].getObjectifCagnotte().ToString();
        //    string statutSelected = liste[index].getStatut();
        //    string imageSelected = liste[index].getImage();
        //    string ouvertureSelected = liste[index].getOuverture().ToString();
        //    string fermetureSelected = liste[index].getFermeture().ToString();

        //    //Faire apparaitre la fenêtre flottante et Désactiver la listeView pour qu'on ne peut pas la toucher
        //    grid_vue_projet.Visibility = Visibility.Visible;
        //    listeProjet.IsEnabled = false;

        //    //Afficher les valeurs
        //    title.Text = titreSelected;
        //    descrip.Text = descriptionSelected;
        //    objectif.Text = objectifCagnotteSelected;
        //    etat.Text = statutSelected;
        //    ouvert.Text = ouvertureSelected;
        //    ferme.Text = fermetureSelected;
        //    //labelTitre2.Content = titreSelected;
        //    somme.Text = sommeCagnotteSelected;

        //}

        //private void retour_cagnotte(object sender, RoutedEventArgs e)
        //{
        //    //réactualiser la listeview
        //    listeProjet.Items.Refresh();

        //    //Faire apparaitre la fenêtre listeView et Désactiver la flottante
        //    grid_vue_projet.Visibility = Visibility.Hidden;
        //    listeProjet.IsEnabled = true;
        //}
        private void modification_titre(object sender, RoutedEventArgs e)
        {
            liste[index].titre = title.Text;

            string requete = " UPDATE `projet` SET `titreprojet` = '" + liste[index].titre + "' WHERE `projet`.`ID_projet` = " + liste[index].IdProjet;


            //connexion à la base de donnée
            conn = new MySqlConnection("SERVER=127.0.0.1; DATABASE='crowdfunding'; UID=root; PASSWORD=");
            conn.Open();

            //creat command
            MySqlCommand cmd = new MySqlCommand();

            //Etablir la connexion de la commande
            cmd.Connection = conn;
            cmd.CommandText = requete;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.ToString());
            }
            conn.Close();
        }

        private void modification_description(object sender, RoutedEventArgs e)
        {
            liste[index].description = descrip.Text;

            string requete = " UPDATE `projet` SET `descriptionProjet` = '" + liste[index].description + "' WHERE `projet`.`ID_projet` = " + liste[index].IdProjet;


            //connexion à la base de donnée
            conn = new MySqlConnection("SERVER=127.0.0.1; DATABASE='crowdfunding'; UID=root; PASSWORD=");
            conn.Open();

            //creat command
            MySqlCommand cmd = new MySqlCommand();

            //Etablir la connexion de la commande
            cmd.Connection = conn;
            cmd.CommandText = requete;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.ToString());
            }
            conn.Close();


        }

        private void retour_cagnotte(object sender, RoutedEventArgs e)
        {

        }
        
    }
}
