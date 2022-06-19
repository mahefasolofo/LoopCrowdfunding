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

namespace Crowdfunding
{
    /// <summary>
    /// Interaction logic for Code.xaml
    /// </summary>
    public partial class Code : Window
    {
        public Code()
        {
            InitializeComponent();
        }

        private void btVerifier_Click(object sender, RoutedEventArgs e)
        {
            //convertir code to String 

            String Code = texboxcode.Text;
            //MessageBox.Show(Code);

            if (Convert.ToBoolean(txbCode.Text == Code))

            {
                Window2 accUser = new Window2();
                accUser.Show();
                //MainWindow.Visibility = Visibility.Hidden;
                this.Hide();
               

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
