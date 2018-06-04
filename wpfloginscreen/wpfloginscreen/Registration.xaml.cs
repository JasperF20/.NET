using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace wpfloginscreen
{
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        public static string ReverseIt(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private void generate_password(object sender, RoutedEventArgs e)
        {
            //TODO: deze methode maakt een nieuwe user aan in de database, nog niet af!
            // get waarde van username textfield
            // zet die string achterstevoren
            // laat het wachtwoord zien in het wachtwoord textfield
            // combinatie van username en password naar sql database uploaden
            // laat berichtje zien dat account is aangemaakt

            if(string.IsNullOrWhiteSpace(regUsername.Text))
            {
                MessageBox.Show("Geen username ingevuld", "Systeem melding");
            } else
            {
                var usernameReversed = ReverseIt(regUsername.Text);
                regPassword.AppendText(usernameReversed);
                MessageBox.Show("Account aangemaakt!", "Systeem melding");
            }
            /*
            SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\sqle2012; Initial Catalog=LoginDB; Integrated Security=True;");
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                String query = "SELECT COUNT(1) FROM tblUser WHERE Username=@Username AND Password=@Password";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Username", regUsername.Text);
                sqlCmd.Parameters.AddWithValue("@Password", regPassword.Password);

                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if (count == 1)
                {
                    MainWindow dashboard = new MainWindow();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username or password is incorrect.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
            */
        }

        private void To_Login_Click(object sender, RoutedEventArgs e)
        {
            //loginPage.Show();
           // Close();
        }

    }
}
