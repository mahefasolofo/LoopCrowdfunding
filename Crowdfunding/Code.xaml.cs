using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Code.xaml
    /// </summary>
    public partial class Code : Window
    {
        MySqlConnection conn = new MySqlConnection("SERVER=127.0.0.1; DATABASE='crowdfunding'; UID=root; PASSWORD=");

        public Code()
        {
            
            InitializeComponent();
            
        }

        private void btVerifier_Click(object sender, RoutedEventArgs e)
        {
            //convertir code to String 

            String Code = texboxcode.Text;
            

            if (Convert.ToBoolean(txbCode.Text == Code))

            {
                

                Window2 accUser = new Window2();
                accUser.Show();
                accUser.textbox_ID.Text = ID_connected.Text;
               
                

                //Récupération du prénom de l'utilisateur pour le mettre dans la page d'accueil2
                conn.Open();
                string sql = "SELECT `nom`,`prenom` FROM `users` WHERE `ID_user` = " + int.Parse(ID_connected.Text) + " ";
                MySqlCommand cmd = new MySqlCommand();

                //Etablir la connexion de la commande
                cmd.Connection = conn;
                cmd.CommandText = sql;

                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    accUser.User.Content = ""+ reader.GetString(0) +" "+ reader.GetString(1)+"";
                }

                conn.Close();

                
                this.Close();
                
                
               

            }

            else

            {

                //MessageBox.Show("votre code n'est pas valide");
                label.Content = "votre code n'est pas valide";
                Color color = (Color)ColorConverter.ConvertFromString("#FF0000");

            }
        }
    }
}
