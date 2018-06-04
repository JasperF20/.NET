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
                            Username = "Admin",
                            Password = "Admin"
                        };

                        db.Users.Add(user);
                        db.SaveChanges();

                        var query = from b in db.Users
                                    orderby b.Username
                                    select b;

                        Console.WriteLine(" All student in the database: ");

                        foreach (var item in query)
                        {
                            Console.WriteLine(item.Username);
                        }

                        Console.WriteLine("Press any key to exit...");
                        Console.ReadKey();
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




