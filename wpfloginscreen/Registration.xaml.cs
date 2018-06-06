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
using System.Windows.Navigation;
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

        private void Create_user(object sender, RoutedEventArgs e)
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
                regPassword.Text = usernameReversed;
           
                using (var db = new webshopHostEntities())
                {
                    Inventory userInventory = new Inventory();
                    
                    var user = new User
                    {
                        Username = regUsername.Text,
                        Password = regPassword.Text,
                        Credit = 50,
                        Inventory = userInventory
                    };

                    db.Users.Add(user);
                    db.SaveChanges();
                }
                MessageBox.Show("Account aangemaakt!", "Systeem melding");
            }
        }

        private void To_Login_Click(object sender, RoutedEventArgs e)
        {
            LoginScreen loginPage = new LoginScreen();
            loginPage.Show();
            Close();
        }

    }
}
