using System;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Windows;
using System.Linq;
using System.Data.Entity.Validation;

// download sql database: https://www.microsoft.com/en-US/download/details.aspx?id=29062
// kies voor SQLEXPRADV_x64_ENU.exe
// tutorial: https://docs.microsoft.com/en-us/sql/relational-databases/lesson-1-connecting-to-the-database-engine?view=sql-server-2017

namespace wpfloginscreen
{
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        Registration registrationPage = new Registration();
        MainWindow webshopPage = new MainWindow();

        public int CreateUserID()
        {
            int x = 10;
            return x;
        }


        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Password))
            {
                MessageBox.Show("Verkeerde inloggegevens!");
            }
            else
            {
                try
                {
                    using (var db = new webshopHostEntities())
                    {
                        //hardcoded nieuwe user aanmaken om te testen
                        // deze moet opgehaald worden uit invoergegevens
                        var user = new User
                        {
                            User_ID = 1,
                            Username = txtUsername.Text,
                            Password = txtPassword.Password,
                            Credit = 50
                        };

                        db.Users.Add(user);
                        db.SaveChanges();
                        MessageBox.Show("Account created & login succesfull!");

                        /*
                        var query = from u in db.Users
                                    orderby u.Username
                                    select u;
                        */

                        Close();
                        webshopPage.Show();

                    }
                }
                catch (Exception exception)
                {
                 
                }



            
            }
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            registrationPage.Show();
            Close();
        }

    }
}




