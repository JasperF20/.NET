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

        public void AddUser()
        {
            using (var db = new webshopHostEntities())
            {
                Inventory userInventory = new Inventory();
                // als die nog niet bestaat -> voeg toe
                if (!db.Users.Any(u => u.Username == regUsername.Text))
                {
                    var user = new User
                    {
                        Username = regUsername.Text,
                        Password = regPassword.Text,
                        Credit = 50,
                        Inventory = userInventory
                    };

                    db.Users.Add(user);
                    db.SaveChanges();
                    MessageBox.Show("Account aangemaakt!", "Systeem melding");
                }
                else
                {
                    MessageBox.Show("Gebruikersnaam bestaat al!", "Systeem melding");
                }
            }
        }

        private void Create_user(object sender, RoutedEventArgs e)
        {
           
            if(string.IsNullOrWhiteSpace(regUsername.Text))
            {
                MessageBox.Show("Geen username ingevuld", "Systeem melding");
            } else
            {
                var usernameReversed = ReverseIt(regUsername.Text);
                regPassword.Text = usernameReversed;
                AddUser();
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
