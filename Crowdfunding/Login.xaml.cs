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
using MySql.Data.MySqlClient;
using System.Net;
using System.Net.Mail;

namespace Crowdfunding
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private string email, password, sql;
        private Connection conn = new Connection();
        private MySqlCommand command;
        Code cd = new Code();
        public int ID_user { get; set; }
        private void annuler_click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            MainWindow main = new MainWindow();
            main.Show();

        }

        public Login()
        {
            InitializeComponent();
        }

        private void btConnecter_Click(object sender, RoutedEventArgs e)
        {
            ////Générer OTP email
            //Random rand = new Random();
            //int otp = rand.Next(1000, 10000);
            ////string codeverif = otp.ToString();
            ////MessageBox.Show(otp.ToString());

            //cd.texboxcode.Text = otp.ToString();
            //try
            //{
            //    MailMessage msg = new MailMessage();
            //    msg.From = new MailAddress("loop.crowdfunding@gmail.com");
            //    msg.To.Add(textBoxEmail.Text);
            //    msg.Subject = "OTP Code";
            //    msg.Body = otp.ToString();

            //    SmtpClient smt = new SmtpClient();
            //    smt.Host = "smtp.gmail.com";
            //    System.Net.NetworkCredential ntcd = new NetworkCredential();
            //    ntcd.UserName = "loop.crowdfunding@gmail.com";
            //    ntcd.Password = "xqkrtrokrarwmcom";
            //    smt.Credentials = ntcd;
            //    smt.EnableSsl = true;
            //    smt.Port = 587;
            //    smt.Send(msg);

            //    //MessageBox.Show("votre email est envoyé");
            //    label.Content = "votre email est envoyé";
            //    Color color = (Color)ColorConverter.ConvertFromString("#4dfd31");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            //login relation base de donnée
            email = textBoxEmail.Text;
            password = textBoxMdp.Password;
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("email ou mots de passe est vide");
            }
            else
            {
                sql = "SELECT * FROM users WHERE email = '" + email + "' AND motdepasse = '" + password + "'";
                if (conn.OpenConnection() == true)
                {
                    try
                    {
                        command = new MySqlCommand(sql, conn.get_connection());
                        object a = command.ExecuteScalar();
                        if (a == null)
                        {
                            label.Content = "Invalide email ou mots de passe";
                            Color color = (Color)ColorConverter.ConvertFromString("#FF0000");
                        }
                        else
                        {

                            ID_user = int.Parse(a.ToString());
                            Code cd = new Code();
                            cd.ID_connected.Text = ID_user.ToString();
                            
                            cd.Show();
                            this.Close();
                           
                            //client clt = new client();
                            //clt.Show();
                            //this.Close();
                        }
                    }
                    catch (MySqlException x)
                    {
                        MessageBox.Show("" + x);
                    }

                }
            }
            textBoxEmail.Text = "";
            textBoxMdp.Password = "";
            conn.close_conn();
        }
    }
}
