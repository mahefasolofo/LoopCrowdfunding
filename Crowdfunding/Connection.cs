using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;

namespace Crowdfunding
{
    internal class Connection
    {
        private MySqlConnection conn;

        public Connection()
        {
            Initialize();
        }
        private void Initialize()
        {
           
                conn = new MySqlConnection("SERVER = 127.0.0.1; DATABASE =crowdfunding; UID = root; PASSWORD =");
         
        }
        public bool OpenConnection()
        {
            try
            {
                conn.Open();
                return true;

            }
            catch (MySqlException e)
            {
                switch (e.Number)
                {
                    case 0:
                        MessageBox.Show("ne peut pas connecter au server");
                        break;
                    case 1:
                        MessageBox.Show("email/mots de passe n'est  pas correct");
                        break;
                }
                return false;

            }
        }
        public void close_conn()
        {
            this.conn.Close();
        }
        public MySqlConnection get_connection()
        {
            return this.conn;
        }
    }
}
