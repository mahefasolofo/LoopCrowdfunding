using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Data.Common;
using MySql.Data.MySqlClient;


namespace Crowdfunding
{
    /// <summary>
    /// Interaction logic for Inscription.xaml
    /// </summary>
    public partial class Inscription : Window
    {
        MySqlConnection conn;
        public Inscription()
        {
            InitializeComponent();
            conn = new MySqlConnection("SERVER=127.0.0.1; DATABASE='crowdfunding'; UID=root; PASSWORD=");
        }

        private void buttonInscription_Click_1(object sender, RoutedEventArgs e)
        {
            string nom = TextBoxNom.Text;
            string prenom = TextBoxPrenom.Text;
            string email = TextBoxMail.Text;
            string adresse = TextBoxAdresse.Text;
            string ville = TextBoxVille.Text;
            string pays = TextBoxPays.Text;
            DateTime datenaissance = (DateTime)DatePickerNaissance.SelectedDate;
            string result = datenaissance.ToString("yyyy-MM-dd");
            string motdepasse = PasswordBoxMdp.Password.ToString();
            if (PasswordBoxConfirmer.Password != PasswordBoxMdp.Password)
            {

                label.Content = "Ces mots de passe ne correspondaient pas. Réessayer";
                Color color = (Color)ColorConverter.ConvertFromString("#FF0000");

                PasswordBoxConfirmer.Foreground = new System.Windows.Media.SolidColorBrush(color);
            }
            else
            {
                if (String.IsNullOrEmpty(TextBoxNom.Text) | String.IsNullOrEmpty(TextBoxPrenom.Text) | String.IsNullOrEmpty(TextBoxMail.Text) | String.IsNullOrEmpty(TextBoxAdresse.Text) | String.IsNullOrEmpty(TextBoxVille.Text) | String.IsNullOrEmpty(TextBoxPays.Text) | String.IsNullOrEmpty(PasswordBoxMdp.Password) | String.IsNullOrEmpty(PasswordBoxConfirmer.Password))
                {
                    label.Content = "certains champs sont vide";

                }
                else
                {
                    conn.Open();
                    string requette1 = "INSERT INTO users(nom,prenom,email,motdepasse,adresse,ville,pays,datenaissance)" + " VALUES ('" + nom + "','" + prenom + "','" + email + "','" + motdepasse + "','" + adresse + "','" + ville + "','" + pays + "','" + result + "')";
                    MySqlCommand cmd = new MySqlCommand(requette1, conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        label.Content = "Inscription réussit";
                        Color color = (Color)ColorConverter.ConvertFromString("#4dfd31");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    conn.Close();
                }
            }
        }

        private void annuler_inscription_click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
